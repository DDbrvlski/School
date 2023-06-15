using Firma.Helper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Szkola.Model.BusinessLogic;
using Szkola.Model.Entities;
using Szkola.Model.EntitiesForView;
using Szkola.ViewModel.Abstract;

namespace Szkola.ViewModel
{
    public class DodajNieobecnoscViewModel : DodajViewModel<Nieobecnosci>
    {
        #region Konstruktor
        public DodajNieobecnoscViewModel() : base("Dodaj nieobecność")
        {
            Item = new Nieobecnosci();
        }
        #endregion
        #region Properties
        #region Pola
        private int _WybraneIdPrzedmiotu;
        public int WybraneIdPrzedmiotu
        {
            get
            {
                return _WybraneIdPrzedmiotu;
            }
            set
            {
                if (value != _WybraneIdPrzedmiotu)
                {
                    _WybraneIdPrzedmiotu = value;
                    GodzinyLekcyjneComboboxItems = new DziennikObecnosciLogic(Db).GetGodzinyLekcyjneDlaLekcji(DataNieobecnosci, WybraneIdKlasy, WybraneIdPrzedmiotu);
                    base.OnPropertyChanged(() => WybraneIdPrzedmiotu);
                }
            }
        }
        private int _WybraneIdKlasy;
        public int WybraneIdKlasy
        {
            get
            {
                return _WybraneIdKlasy;
            }
            set
            {
                if (value != _WybraneIdKlasy)
                {
                    _WybraneIdKlasy = value;
                    base.OnPropertyChanged(() => WybraneIdKlasy);
                }
            }
        }
        private int _WybraneIdGodzinyLekcyjnej;
        public int WybraneIdGodzinyLekcyjnej
        {
            get
            {
                return _WybraneIdGodzinyLekcyjnej;
            }
            set
            {
                if (value != _WybraneIdGodzinyLekcyjnej)
                {
                    _WybraneIdGodzinyLekcyjnej = value;
                    base.OnPropertyChanged(() => WybraneIdGodzinyLekcyjnej);
                }
            }
        }
        private DateTime _DataNieobecnosci = DateTime.Now;
        public DateTime DataNieobecnosci
        {
            get
            {
                return _DataNieobecnosci;
            }
            set
            {
                if (value != _DataNieobecnosci)
                {
                    _DataNieobecnosci = value;
                    PrzedmiotyComboboxItems = new DziennikObecnosciLogic(Db).GetPrzedmiotyDlaDnia(DataNieobecnosci, WybraneIdKlasy);
                    base.OnPropertyChanged(() => DataNieobecnosci);
                }
            }
        }
        #endregion
        #region Listy
        private ObservableCollection<ListaObecnosciForAllView> _UczniowieList;
        public ObservableCollection<ListaObecnosciForAllView> UczniowieList
        {
            get
            {
                return _UczniowieList;
            }
            set
            {
                _UczniowieList = value;
                OnPropertyChanged(() => UczniowieList);
            }
        }
        private ObservableCollection<KeyAndValue> _PrzedmiotyComboboxItems;
        public ObservableCollection<KeyAndValue> PrzedmiotyComboboxItems
        {
            get
            {
                return _PrzedmiotyComboboxItems;
            }
            set
            {
                _PrzedmiotyComboboxItems = value;
                OnPropertyChanged(() => PrzedmiotyComboboxItems);
            }
        }
        private ObservableCollection<KeyAndValue> _GodzinyLekcyjneComboboxItems;
        public ObservableCollection<KeyAndValue> GodzinyLekcyjneComboboxItems
        {
            get
            {
                return _GodzinyLekcyjneComboboxItems;
            }
            set
            {
                _GodzinyLekcyjneComboboxItems = value;
                OnPropertyChanged(() => GodzinyLekcyjneComboboxItems);
            }
        }
        #endregion
        #region Combobox
        public IQueryable<KeyAndValue> KlasyComboboxItems
        {
            get
            {
                return new PodstawoweComboboxyLogic(Db).GetAktywneKlasy();
            }
        }
        #endregion
        #endregion
        #region Komendy
        private BaseCommand _ShowListaObecnosciCommand;
        public ICommand ShowListaObecnosciCommand
        {
            get
            {
                if (_ShowListaObecnosciCommand == null) { }
                {
                    _ShowListaObecnosciCommand = new BaseCommand(() => Load());
                }
                return _ShowListaObecnosciCommand;
            }
        }
        #endregion
        #region Helpers
        public override void Save()
        {
            foreach (var element in UczniowieList)
            {
                if (element.IsSelected)
                {
                    Item.CzyAktywny = true;
                    Item.IdUzytkownika = element.IdUcznia;
                    Item.IdLekcji = new DziennikObecnosciLogic(Db).GetIdLekcji(DataNieobecnosci, WybraneIdKlasy, WybraneIdPrzedmiotu, WybraneIdGodzinyLekcyjnej);
                    Item.DataNieobecnosci = DataNieobecnosci;
                    Db.Nieobecnosci.AddObject(Item);
                    Item = new Nieobecnosci();
                }
            }
            Db.SaveChanges();
        }
        public void Load()
        {
            if (WybraneIdPrzedmiotu == 0)
            {
                PrzedmiotyComboboxItems = new DziennikObecnosciLogic(Db).GetPrzedmiotyDlaDnia(DataNieobecnosci, WybraneIdKlasy);
            }
            UczniowieList = new DziennikObecnosciLogic(Db).GetListaObecnosci(WybraneIdKlasy);
        }
        #endregion

    }
}
