using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Szkola.Helpers.Messenger;
using Szkola.Model.BusinessLogic;
using Szkola.Model.Entities;
using Szkola.Model.EntitiesForView;
using Szkola.ViewModel.Abstract;

namespace Szkola.ViewModel
{
    public class WszyscyUzytkownicyViewModel : WszystkieViewModel<UzytkownicyForAllView>
    {
        #region Konstruktor
        public WszyscyUzytkownicyViewModel() : base("Uzytkownicy")
        {
            Messenger.Default.Register<string>(this, getMessage);
        }
        #endregion
        #region Properties
        #region Pola
        private string messageInfo = "";
        private UzytkownicyForAllView _WybranyStudent;
        public UzytkownicyForAllView WybranyStudent
        {
            get
            {
                return _WybranyStudent;
            }
            //uruchamia się wtedy gdy wybiorę kontrahenta i wtedy trzeba go wyslac do okna NowaFakturaViewModel
            //i zamknac okno z kontrahentami
            set
            {
                if (_WybranyStudent != value)
                {
                    _WybranyStudent = value;
                    if (messageInfo != "")
                    {
                        Messenger.Default.Send(new Message<UzytkownicyForAllView>(messageInfo, WybranyStudent));
                        OnRequestClose();
                    }
                }
            }
        }
        private Uzytkownik _WybranyUzytkownik;
        public Uzytkownik WybranyUzytkownik
        {
            get { return _WybranyUzytkownik; }
            set
            {
                if (value != _WybranyUzytkownik)
                {
                    _WybranyUzytkownik = value;
                    base.OnPropertyChanged(() => WybranyUzytkownik);
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
        private int _WybraneIdPlci;
        public int WybraneIdPlci
        {
            get
            {
                return _WybraneIdPlci;
            }
            set
            {
                if (value != _WybraneIdPlci)
                {
                    _WybraneIdPlci = value;
                    base.OnPropertyChanged(() => WybraneIdPlci);
                }


            }
        }
        private int _WybraneIdStatusu;
        public int WybraneIdStatusu
        {
            get
            {
                return _WybraneIdStatusu;
            }
            set
            {
                if (value != _WybraneIdStatusu)
                {
                    _WybraneIdStatusu = value;
                    base.OnPropertyChanged(() => WybraneIdStatusu);
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
        public IQueryable<KeyAndValue> StatusyComboboxItems
        {
            get
            {
                return new PodstawoweComboboxyLogic(SzkolaEntities).GetAktywneStatusy();
            }
        }
        public IQueryable<KeyAndValue> PlcieComboboxItems
        {
            get
            {
                return new PodstawoweComboboxyLogic(SzkolaEntities).GetAktywnePlcie();
            }
        }
        #endregion
        #endregion
        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<UzytkownicyForAllView>
                (
                    from Uzytkownik in SzkolaEntities.Uzytkownik
                    where Uzytkownik.CzyAktywny == true
                    select new UzytkownicyForAllView
                    {
                        IdUzytkownika = Uzytkownik.IdUzytkownik,
                        Imie = Uzytkownik.Imie,
                        Nazwisko = Uzytkownik.Nazwisko,
                        Klasa = Uzytkownik.Klasa1.NazwaKlasy,
                        Plec = Uzytkownik.Plec.NazwaPlci,
                        Status = Uzytkownik.Status.NazwaStatusuKonta,
                        Pesel = Uzytkownik.Pesel,
                        Telefon = Uzytkownik.Telefon
                    }
                );
        }
        public override void Delete()
        {
            SzkolaEntities.Uzytkownik.First(x => x.IdUzytkownik == WybranyStudent.IdUzytkownika).CzyAktywny = false;
            SzkolaEntities.SaveChanges();
            Load();
        }
        private void getMessage(string message)
        {
            if (message != null)
            {
                messageInfo = message;
            }
        }
        #endregion
        #region Find & Sort
        public override void Sort()
        {
            if (SortField == "Imie")
            {
                List = new ObservableCollection<UzytkownicyForAllView>(List.OrderBy(item => item.Imie));
            }
            if (SortField == "Nazwisko")
            {
                List = new ObservableCollection<UzytkownicyForAllView>(List.OrderBy(item => item.Nazwisko));
            }
            if (SortField == "Klasa")
            {
                List = new ObservableCollection<UzytkownicyForAllView>(List.OrderBy(item => item.Klasa));
            }
            if (SortField == "Plec")
            {
                List = new ObservableCollection<UzytkownicyForAllView>(List.OrderBy(item => item.Plec));
            }
            if (SortField == "Status")
            {
                List = new ObservableCollection<UzytkownicyForAllView>(List.OrderBy(item => item.Status));
            }
        }
        public override List<string> GetComboboxSortList()
        {
            return new List<string>
            {
                "Imie",
                "Nazwisko",
                "Klasa",
                "Plec",
                "Status"
            };
        }
        public override void Find()
        {
            if (FindField == "Imie")
            {
                List = new ObservableCollection<UzytkownicyForAllView>(List.Where(item => item.Imie != null && item.Imie.StartsWith(FindTextBox)));
            }
            if (FindField == "Nazwisko")
            {
                List = new ObservableCollection<UzytkownicyForAllView>(List.Where(item => item.Nazwisko != null && item.Nazwisko.StartsWith(FindTextBox)));
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
