using Firma.Helper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Szkola.Model.BusinessLogic;
using Szkola.Model.Entities;
using Szkola.Model.EntitiesForView;
using Szkola.ViewModel.Abstract;

namespace Szkola.ViewModel
{
    public class DodajOceneViewModel : DodajViewModel<Oceny>
    {
        #region Konstruktor
        public DodajOceneViewModel() : base("Dodaj ocene")
        {
            Item = new Oceny();
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
        private int _WybraneIdFormy;
        public int WybraneIdFormy
        {
            get
            {
                return _WybraneIdFormy;
            }
            set
            {
                if (value != _WybraneIdFormy)
                {
                    _WybraneIdFormy = value;
                    base.OnPropertyChanged(() => WybraneIdFormy);
                }
            }
        }
        private DateTime _DataWyslania = DateTime.Now;
        public DateTime DataWyslania
        {
            get
            {
                return _DataWyslania;
            }
            set
            {
                if (value != _DataWyslania)
                {
                    _DataWyslania = value;
                    base.OnPropertyChanged(() => DataWyslania);
                }
            }
        }
        #endregion
        #region Listy
        private ObservableCollection<KlasyUczniowieForAllView> _UczniowieList;
        public ObservableCollection<KlasyUczniowieForAllView> UczniowieList
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
        #endregion
        #region Combobox
        public IQueryable<KeyAndValue> KlasyComboboxItems
        {
            get
            {
                return new PodstawoweComboboxyLogic(Db).GetAktywneKlasy();
            }
        }
        public IQueryable<KeyAndValue> FormySprawdzaniaWiedzyComboboxItems
        {
            get
            {
                return new PodstawoweComboboxyLogic(Db).GetAktywneFormySprawdzaniaWiedzy();
            }
        }
        public IQueryable<KeyAndValue> PrzedmiotyComboboxItems
        {
            get
            {
                return new PodstawoweComboboxyLogic(Db).GetAktywnePrzedmioty();
            }
        }
        #endregion
        #endregion
        #region Komendy
        private BaseCommand _ShowAllUczniowieCommand;
        public ICommand ShowAllUczniowieCommand
        {
            get
            {
                if (_ShowAllUczniowieCommand == null) { }
                {
                    _ShowAllUczniowieCommand = new BaseCommand(() => Load());
                }
                return _ShowAllUczniowieCommand;
            }
        }
        #endregion
        #region Helpers
        public override void Save()
        {
            foreach (var element in UczniowieList)
            {
                if (element.WybraneIdOceny != 0)
                {
                    Item.CzyAktywny = true;
                    Item.IdFormySprawdzeniaWiedzy = WybraneIdFormy;
                    Item.IdPrzedmiotu = WybraneIdPrzedmiotu;
                    Item.IdNazwyOceny = element.WybraneIdOceny;
                    Item.DataDodaniaOceny = DataWyslania;
                    Item.IdUcznia = element.Id;
                    Db.Oceny.AddObject(Item);
                    Item = new Oceny();
                }
            }
            Db.SaveChanges();
        }
        public void Load()
        {
            UczniowieList = new DziennikOcenLogic(Db).GetUczniowieZKlasyList(WybraneIdKlasy);
        }
        #endregion
        
    }
}
