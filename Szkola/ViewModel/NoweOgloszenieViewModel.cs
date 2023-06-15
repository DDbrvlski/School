using Firma.Helper;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using Szkola.Model.Entities;
using Szkola.Model.EntitiesForView;
using Szkola.Model.Validators;
using Szkola.ViewModel.Abstract;

namespace Szkola.ViewModel
{
    public class NoweOgloszenieViewModel : DodajViewModel<Ogloszenia>, IDataErrorInfo
    {
        #region Konstruktor
        public NoweOgloszenieViewModel() : base("Ogloszenie")
        {
            Item = new Ogloszenia();
            Item2 = new OdbiorcyOgloszenia();
            Item3 = new Uzytkownik();
        }
        #endregion
        #region Properties
        #region Pola
        private bool wcisnietoPrzycisk = false;
        public OdbiorcyOgloszenia Item2 { get; set; }
        public Uzytkownik Item3 { get; set; }
        public string TytulOgloszenia
        {
            get
            {
                return Item.TytulOgloszenia;
            }
            set
            {
                if (value != Item.TytulOgloszenia)
                {
                    Item.TytulOgloszenia = value;
                    base.OnPropertyChanged(() => TytulOgloszenia);
                }
            }
        }
        public bool CzyWazne
        {
            get
            {
                return Item.CzyWazne;
            }
            set
            {
                if (value != Item.CzyWazne)
                {
                    Item.CzyWazne = value;
                    base.OnPropertyChanged(() => CzyWazne);
                }
            }
        }
        public string TrescOgloszenia
        {
            get
            {
                return Item.TrescOgloszenia;
            }
            set
            {
                if (value != Item.TrescOgloszenia)
                {
                    Item.TrescOgloszenia = value;
                    base.OnPropertyChanged(() => TrescOgloszenia);
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
                    Item.DataWyslania = DataWyslania;
                }
            }
        }
        #endregion
        #region Listy
        private ObservableCollection<UzytkownicyOgloszeniaForAllView> _UzytkownicyOgloszeniaList;
        public ObservableCollection<UzytkownicyOgloszeniaForAllView> UzytkownicyOgloszeniaList
        {
            get
            {
                if (_UzytkownicyOgloszeniaList == null) GetUzytkownicyOgloszeniaList();
                return _UzytkownicyOgloszeniaList;
            }
            set
            {
                _UzytkownicyOgloszeniaList = value;
                OnPropertyChanged(() => UzytkownicyOgloszeniaList);
            }
        }
        private ObservableCollection<UzytkownicyOgloszeniaForAllView> _WybraniUzytkownicyList;
        public ObservableCollection<UzytkownicyOgloszeniaForAllView> WybraniUzytkownicyList
        {
            get
            {
                return _WybraniUzytkownicyList;
            }
            set
            {
                _WybraniUzytkownicyList = value;
                OnPropertyChanged(() => WybraniUzytkownicyList);
            }
        }
        #endregion
        #endregion
        #region Komendy
        private BaseCommand _ZaznaczOdznaczCommand;
        public ICommand ZaznaczOdznaczCommand
        {
            get
            {
                if (_ZaznaczOdznaczCommand == null) { }
                {
                    _ZaznaczOdznaczCommand = new BaseCommand(() => CheckAll());
                }
                return _ZaznaczOdznaczCommand;
            }
        }
        #endregion
        #region Helpers
        public void GetSelectedUsersList()
        {
            WybraniUzytkownicyList = new ObservableCollection<UzytkownicyOgloszeniaForAllView>();
            foreach (var item in UzytkownicyOgloszeniaList)
            {
                if (item.IsSelected == true)
                {
                    WybraniUzytkownicyList.Add(item);
                }
            }
        }
        public void SendMessageToUsers()
        {
            foreach (var item in WybraniUzytkownicyList)
            {
                Item2.CzyAktywny = true;
                Item2.IdUzytkownika = item.Id;
                Item2.IdOgloszenia = Item.IdOgloszenie;
                Db.OdbiorcyOgloszenia.AddObject(Item2);
                Item2 = new OdbiorcyOgloszenia();
            }
        }
        public void GetUzytkownicyOgloszeniaList()
        {
            UzytkownicyOgloszeniaList = new ObservableCollection<UzytkownicyOgloszeniaForAllView>(
                    from Uzytkownik in Db.Uzytkownik
                    where Uzytkownik.CzyAktywny == true
                    select new UzytkownicyOgloszeniaForAllView
                    {
                        Id = Uzytkownik.IdUzytkownik,
                        Imie = Uzytkownik.Imie,
                        Nazwisko = Uzytkownik.Nazwisko,
                        Klasa = Uzytkownik.Klasa1.NazwaKlasy,
                        Status = Uzytkownik.Status.NazwaStatusuKonta
                    }
                );
        }
        public override void Save()
        {
            GetSelectedUsersList();
            SendMessageToUsers();
            Item.CzyAktywny = true;
            Item.DataWyslania = DataWyslania;
            Db.Ogloszenia.AddObject(Item);
            Db.SaveChanges();
        }
        public void CheckAll()
        {
            if (wcisnietoPrzycisk == false)
            {
                wcisnietoPrzycisk = true;
                foreach (var item in UzytkownicyOgloszeniaList)
                {
                    item.IsSelected = true;
                }
            }
            else
            {
                wcisnietoPrzycisk = false;
                foreach (var item in UzytkownicyOgloszeniaList)
                {
                    item.IsSelected = false;
                }
            }
            //wymuszenie odświeżenia zakładki
            if (!wcisnietoPrzycisk)
            {
                GetUzytkownicyOgloszeniaList();
            }
            else
            {
                GetSelectedUsersList();
                UzytkownicyOgloszeniaList = WybraniUzytkownicyList;
            }
        }
        #endregion
        #region Validation
        public string Error
        {
            get
            {
                return null;
            }
        }
        public string this[string name]
        {
            get
            {
                string komunikat = null;
                if (name == "TytulOgloszenia")
                {
                    komunikat = StringValidator.SprawdzTytulOgloszeniaMaOdpowiedniaDlugosc(TytulOgloszenia);
                    Wiadomosc = komunikat;
                }
                if (name == "TrescOgloszenia")
                {
                    komunikat = StringValidator.SprawdzCzyZaczynaSieOdDuzej(TrescOgloszenia);
                    Wiadomosc = komunikat;
                }
                return komunikat;
            }
        }
        //To jest funkcja ktora sprawdza poprawnosc danych przed zapisem
        //Jezeli funkcja zwroci True to znaczy ze wszystkie dane sa poprawne i mozna zapisac rekord
        //Jezeli funkcja zwroci False to znaczy ze jest blad w danych i rekord sie nie zapisze
        public override bool IsValid()
        {
            if (this["TytulOgloszenia"] == null && this["TrescOgloszenia"] == null)
            {
                return true;
            }
            return false;
        }
        #endregion
    }
}
