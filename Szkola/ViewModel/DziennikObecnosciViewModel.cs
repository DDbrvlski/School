using Firma.Helper;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Szkola.Helpers.Messenger;
using Szkola.Model.BusinessLogic;
using Szkola.Model.Entities;
using Szkola.Model.EntitiesForView;
using Szkola.ViewModel.Abstract;

namespace Szkola.ViewModel
{
    public class DziennikObecnosciViewModel : WszystkieViewModel<DziennikObecnosciForAllView>
    {
        #region Konstruktor
        public DziennikObecnosciViewModel() : base("Dziennik obecności")
        {
            Messenger.Default.Register<Message<UzytkownicyForAllView>>(this, getWybranyStudent);
            Messenger.Default.Register<Message<KlasyForAllView>>(this, getWybranaKlasa);
        }
        #endregion
        #region Properties
        #region Pola
        private DateTime _DataNieobecnosciWKlasieOd = DateTime.Now;
        public DateTime DataNieobecnosciWKlasieOd
        {
            get
            {
                return _DataNieobecnosciWKlasieOd;
            }
            set
            {
                if (value != _DataNieobecnosciWKlasieOd)
                {
                    _DataNieobecnosciWKlasieOd = value;
                    base.OnPropertyChanged(() => DataNieobecnosciWKlasieOd);
                }
            }
        }
        private DateTime _DataNieobecnosciWKlasieDo = DateTime.Now;
        public DateTime DataNieobecnosciWKlasieDo
        {
            get
            {
                return _DataNieobecnosciWKlasieDo;
            }
            set
            {
                if (value != _DataNieobecnosciWKlasieDo)
                {
                    _DataNieobecnosciWKlasieDo = value;
                    base.OnPropertyChanged(() => DataNieobecnosciWKlasieDo);
                }
            }
        }
        private DateTime _DataNieobecnosciUczniaOd = DateTime.Now;
        public DateTime DataNieobecnosciUczniaOd
        {
            get
            {
                return _DataNieobecnosciUczniaOd;
            }
            set
            {
                if (value != _DataNieobecnosciUczniaOd)
                {
                    _DataNieobecnosciUczniaOd = value;
                    base.OnPropertyChanged(() => DataNieobecnosciUczniaOd);
                }
            }
        }
        private DateTime _DataNieobecnosciUczniaDo = DateTime.Now;
        public DateTime DataNieobecnosciUczniaDo
        {
            get
            {
                return _DataNieobecnosciUczniaDo;
            }
            set
            {
                if (value != _DataNieobecnosciUczniaDo)
                {
                    _DataNieobecnosciUczniaDo = value;
                    base.OnPropertyChanged(() => DataNieobecnosciUczniaDo);
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
        private int _WybraneIdUcznia;
        public int WybraneIdUcznia
        {
            get
            {
                return _WybraneIdUcznia;
            }
            set
            {
                if (value != _WybraneIdUcznia)
                {
                    _WybraneIdUcznia = value;
                    base.OnPropertyChanged(() => WybraneIdUcznia);
                }
            }
        }
        private int _WybraneIdPrzedmiotuUcznia;
        public int WybraneIdPrzedmiotuUcznia
        {
            get
            {
                return _WybraneIdPrzedmiotuUcznia;
            }
            set
            {
                if (value != _WybraneIdPrzedmiotuUcznia)
                {
                    _WybraneIdPrzedmiotuUcznia = value;
                    base.OnPropertyChanged(() => WybraneIdPrzedmiotuUcznia);
                }
            }
        }
        #endregion
        #region Listy
        private DziennikObecnosciForAllView _WybranyUczen;
        public DziennikObecnosciForAllView WybranyUczen
        {
            get
            {
                return _WybranyUczen;
            }
            set
            {
                if (value != _WybranyUczen)
                {
                    _WybranyUczen = value;
                    base.OnPropertyChanged(() => WybranyUczen);
                }
            }
        }
        private ObservableCollection<ObecnosciUczniaForAllView> _NieobecnosciUczniaList;
        public ObservableCollection<ObecnosciUczniaForAllView> NieobecnosciUczniaList
        {
            get
            {
                return _NieobecnosciUczniaList;
            }
            set
            {
                _NieobecnosciUczniaList = value;
                OnPropertyChanged(() => NieobecnosciUczniaList);
            }
        }
        #endregion
        #region Comboboxy
        public IQueryable<KeyAndValue> KlasyComboboxItems
        {
            get
            {
                return new PodstawoweComboboxyLogic(SzkolaEntities).GetAktywneKlasy();
            }
        }
        public IQueryable<KeyAndValue> PrzedmiotyComboboxItems
        {
            get
            {
                return new PodstawoweComboboxyLogic(SzkolaEntities).GetAktywnePrzedmioty();
            }
        }
        public IQueryable<KeyAndValue> UczniowieComboboxItems
        {
            get
            {
                return new PodstawoweComboboxyLogic(SzkolaEntities).GetAktywniUczniowie();
            }
        }
        #endregion
        #endregion
        #region Komendy
        private BaseCommand _ShowNieobecnosciCommand;
        public ICommand ShowNieobecnosciCommand
        {
            get
            {
                if (_ShowNieobecnosciCommand == null) { }
                {
                    _ShowNieobecnosciCommand = new BaseCommand(() => LoadNieobecnosci());
                }
                return _ShowNieobecnosciCommand;
            }
        }
        private BaseCommand _ShowNieobecnosciUczniaCommand;
        public ICommand ShowNieobecnosciUczniaCommand
        {
            get
            {
                if (_ShowNieobecnosciUczniaCommand == null) { }
                {
                    _ShowNieobecnosciUczniaCommand = new BaseCommand(() => LoadNieobecnosciUcznia());
                }
                return _ShowNieobecnosciUczniaCommand;
            }
        }
        private BaseCommand _ShowUczniowieCommand;
        public ICommand ShowUczniowieCommand
        {
            get
            {
                if (_ShowUczniowieCommand == null) { }
                {
                    _ShowUczniowieCommand = new BaseCommand(() => showStudents());
                }
                return _ShowUczniowieCommand;
            }
        }
        private BaseCommand _ShowKlasyCommand;
        public ICommand ShowKlasyCommand
        {
            get
            {
                if (_ShowKlasyCommand == null) { }
                {
                    _ShowKlasyCommand = new BaseCommand(() => showKlasy());
                }
                return _ShowKlasyCommand;
            }
        }
        #endregion
        #region Helpers
        public override void Delete()
        {
            throw new NotImplementedException();
        }
        public override void Load()
        {
            List = new DziennikObecnosciLogic(SzkolaEntities).GetNieobecnosciWKlasie(DateTime.Today, DateTime.Today, 0, 0);
        }
        public void LoadNieobecnosci()
        {
            List = new DziennikObecnosciLogic(SzkolaEntities).GetNieobecnosciWKlasie(DataNieobecnosciWKlasieOd, DataNieobecnosciWKlasieDo, WybraneIdKlasy, WybraneIdPrzedmiotu);
        }
        public void LoadNieobecnosciUcznia()
        {
            if (WybraneIdUcznia == 0)
            {
                WybraneIdUcznia = WybranyUczen.IdUcznia;
                if (WybraneIdPrzedmiotuUcznia == 0 && WybraneIdPrzedmiotu != 0)
                {
                    WybraneIdPrzedmiotuUcznia = WybraneIdPrzedmiotu;
                }
                DataNieobecnosciUczniaOd = DataNieobecnosciWKlasieOd;
                DataNieobecnosciUczniaDo = DataNieobecnosciWKlasieDo;
            }
            else
            {
                if (WybraneIdPrzedmiotuUcznia == 0 && WybraneIdPrzedmiotu != 0)
                {
                    WybraneIdPrzedmiotuUcznia = WybraneIdPrzedmiotu;
                }
                DataNieobecnosciUczniaOd = DataNieobecnosciWKlasieOd;
                DataNieobecnosciUczniaDo = DataNieobecnosciWKlasieDo;
            }
            NieobecnosciUczniaList = new DziennikObecnosciLogic(SzkolaEntities).GetNieobecnosciUcznia(DataNieobecnosciUczniaOd, DataNieobecnosciUczniaDo, WybraneIdUcznia, WybraneIdPrzedmiotuUcznia);
        }
        private void showStudents()
        {
            Messenger.Default.Send("StudenciObecnosci");
        }
        private void getWybranyStudent(Message<UzytkownicyForAllView> wiadomosc)
        {
            if (wiadomosc.messageInfo == "StudenciObecnosc") WybraneIdUcznia = wiadomosc.element.IdUzytkownika;
        }
        private void showKlasy()
        {
            Messenger.Default.Send("KlasyObecnosci");
        }
        private void getWybranaKlasa(Message<KlasyForAllView> wiadomosc)
        {
            if (wiadomosc.messageInfo == "KlasyObecnosc") WybraneIdKlasy = wiadomosc.element.IdKlasy;
        }
        #endregion
        #region Find & Sort
        public override void Sort()
        {
            if (SortField == "Nazwisko")
            {
                List = new ObservableCollection<DziennikObecnosciForAllView>(List.OrderBy(item => item.Nazwisko));
            }
            if (SortField == "Imie")
            {
                List = new ObservableCollection<DziennikObecnosciForAllView>(List.OrderBy(item => item.Imie));
            }
            if (SortField == "Liczba nieobecności")
            {
                List = new ObservableCollection<DziennikObecnosciForAllView>(List.OrderBy(item => item.LiczbaNieobecnosci));
            }
        }
        public override List<string> GetComboboxSortList()
        {
            return new List<string>
            {
                "Imie",
                "Nazwisko",
                "Liczba nieobecności"
            };
        }
        public override void Find()
        {
            if (FindField == "Imie")
            {
                List = new ObservableCollection<DziennikObecnosciForAllView>(List.Where(item => item.Imie != null && item.Imie.StartsWith(FindTextBox)));
            }
            if (FindField == "Nazwisko")
            {
                List = new ObservableCollection<DziennikObecnosciForAllView>(List.Where(item => item.Nazwisko != null && item.Nazwisko.StartsWith(FindTextBox)));
            }
        }
        public override List<string> GetComboboxFindList()
        {
            return new List<string>
            {
                "Imie",
                "Nazwisko"
            };
        }
        #endregion

    }
}
