using Firma.Helper;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Szkola.Helpers.Messenger;
using Szkola.Model.BusinessLogic;
using Szkola.Model.Entities;
using Szkola.Model.EntitiesForView;
using Szkola.ViewModel.Abstract;

namespace Szkola.ViewModel
{
    public class DziennikOcenViewModel : WszystkieViewModel<DziennikUczniowieForAllView>
    {
        #region Konstruktor
        public DziennikOcenViewModel() : base("Dziennik ocen")
        {
            Messenger.Default.Register<Message<UzytkownicyForAllView>>(this, getWybranyStudent);
            Messenger.Default.Register<Message<KlasyForAllView>>(this, getWybranaKlasa);
        }
        #endregion
        #region Properties
        #region Pola
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
        private ObservableCollection<DziennikUczenOcenyForAllView> _OcenyUczniaList;
        public ObservableCollection<DziennikUczenOcenyForAllView> OcenyUczniaList
        {
            get
            {
                return _OcenyUczniaList;
            }
            set
            {
                _OcenyUczniaList = value;
                OnPropertyChanged(() => OcenyUczniaList);
            }
        }
        private DziennikUczniowieForAllView _WybranyUczen;
        public DziennikUczniowieForAllView WybranyUczen
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
        #endregion
        #region Comboboxy
        public IQueryable<KeyAndValue> KlasyComboboxItems
        {
            get
            {
                return new PodstawoweComboboxyLogic(SzkolaEntities).GetAktywneKlasy();
            }
        }
        public IQueryable<KeyAndValue> UczniowieComboboxItems
        {
            get
            {
                return new PodstawoweComboboxyLogic(SzkolaEntities).GetAktywniUczniowie();
            }
        }
        public IQueryable<KeyAndValue> PrzedmiotyComboboxItems
        {
            get
            {
                return new PodstawoweComboboxyLogic(SzkolaEntities).GetAktywnePrzedmioty();
            }
        }
        public IQueryable<KeyAndValue> NazwyOcenComboboxItems
        {
            get
            {
                return new PodstawoweComboboxyLogic(SzkolaEntities).GetAktywneNazwyOcen();
            }
        }
        public IQueryable<KeyAndValue> FormySprawdzaniaWiedzyComboboxItems
        {
            get
            {
                return new PodstawoweComboboxyLogic(SzkolaEntities).GetAktywneFormySprawdzaniaWiedzy();
            }
        }
        #endregion
        #endregion
        #region Komendy
        private BaseCommand _ShowDziennikOcenCommand;
        public ICommand ShowDziennikOcenCommand
        {
            get
            {
                if (_ShowDziennikOcenCommand == null) { }
                {
                    _ShowDziennikOcenCommand = new BaseCommand(() => LoadDziennik());
                }
                return _ShowDziennikOcenCommand;
            }
        }
        private BaseCommand _ShowAllOcenyUczniaCommand;
        public ICommand ShowAllOcenyUczniaCommand
        {
            get
            {
                if (_ShowAllOcenyUczniaCommand == null) { }
                {
                    _ShowAllOcenyUczniaCommand = new BaseCommand(() => LoadUczen());
                }
                return _ShowAllOcenyUczniaCommand;
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
        public override void Load()
        {
            List = new DziennikOcenLogic(SzkolaEntities).GetUczniowieISredniaZKlasyList(0, 0);
        }
        public void LoadDziennik()
        {
            List = new DziennikOcenLogic(SzkolaEntities).GetUczniowieISredniaZKlasyList(WybraneIdKlasy, WybraneIdPrzedmiotu);
        }
        public void LoadUczen()
        {
            if (WybranyUczen != null)
            {
                WybraneIdUcznia = WybranyUczen.IdUcznia;
                WybraneIdPrzedmiotuUcznia = WybraneIdPrzedmiotu;
                WybranyUczen = null;
            }
            OcenyUczniaList = new DziennikOcenLogic(SzkolaEntities).GetAktywneOcenyUcznia(WybraneIdUcznia, WybraneIdPrzedmiotuUcznia);
        }
        private void showStudents()
        {
            Messenger.Default.Send("StudenciOceny");
        }
        private void getWybranyStudent(Message<UzytkownicyForAllView> wiadomosc)
        {
            if (wiadomosc.messageInfo == "StudenciOcena") WybraneIdUcznia = wiadomosc.element.IdUzytkownika;
        }
        private void showKlasy()
        {
            Messenger.Default.Send("KlasyOceny");
        }
        private void getWybranaKlasa(Message<KlasyForAllView> wiadomosc)
        {
            if (wiadomosc.messageInfo == "KlasyOcena") WybraneIdKlasy = wiadomosc.element.IdKlasy;
        }
        #endregion

        #region Find & Sort
        public override void Sort()
        {
            if (SortField == "Klasa")
            {
                List = new ObservableCollection<DziennikUczniowieForAllView>(List.OrderBy(item => item.Klasa));
            }
            if (SortField == "Imie")
            {
                List = new ObservableCollection<DziennikUczniowieForAllView>(List.OrderBy(item => item.Imie));
            }
            if (SortField == "SredniaOcen")
            {
                List = new ObservableCollection<DziennikUczniowieForAllView>(List.OrderBy(item => item.Srednia));
            }
            if (SortField == "Nazwisko")
            {
                List = new ObservableCollection<DziennikUczniowieForAllView>(List.OrderBy(item => item.Nazwisko));
            }
        }
        public override List<string> GetComboboxSortList()
        {
            return new List<string>
            {
                "Klasa",
                "Imie",
                "Nazwisko",
                "SredniaOcen"
            };
        }
        public override void Find()
        {
            if (FindField == "Imie")
            {
                List = new ObservableCollection<DziennikUczniowieForAllView>(List.Where(item => item.Imie != null && item.Imie.StartsWith(FindTextBox)));
            }
            if (FindField == "Nazwisko")
            {
                List = new ObservableCollection<DziennikUczniowieForAllView>(List.Where(item => item.Nazwisko != null && item.Nazwisko.StartsWith(FindTextBox)));
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

        public override void Delete()
        {
            throw new NotImplementedException();
        }
        #endregion

        
    }
}
