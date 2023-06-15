using Firma.Helper;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Szkola.Model.BusinessLogic;
using Szkola.Model.Entities;
using Szkola.Model.EntitiesForView;
using Szkola.Model.Validators;
using Szkola.ViewModel.Abstract;

namespace Szkola.ViewModel
{
    public class NowyUzytkownikViewModel : DodajViewModel<Uzytkownik>, IDataErrorInfo
    {
        #region Konstruktor
        public NowyUzytkownikViewModel() : base("Uzytkownik")
        {
            Item = new Uzytkownik();
            Item2 = new Adres();
        }
        #endregion
        #region Properties
        #region Pola
        public Adres Item2 { get; set; }
        #region Dane osobowe
        public string Imie
        {
            get
            {
                return Item.Imie;
            }
            set
            {
                if (value != Item.Imie)
                {
                    Item.Imie = value;
                    base.OnPropertyChanged(() => Imie);
                }
            }
        }
        public string Nazwisko
        {
            get
            {
                return Item.Nazwisko;
            }
            set
            {
                if (value != Item.Nazwisko)
                {
                    Item.Nazwisko = value;
                    base.OnPropertyChanged(() => Nazwisko);
                }
            }
        }
        public int? IdPlec
        {
            get
            {
                return Item.IdPlec;
            }
            set
            {
                if (value != Item.IdPlec)
                {
                    Item.IdPlec = value;
                    base.OnPropertyChanged(() => IdPlec);
                }
            }
        }
        public string Telefon
        {
            get
            {
                return Item.Telefon;
            }
            set
            {
                if (value != Item.Telefon)
                {
                    Item.Telefon = value;
                    base.OnPropertyChanged(() => Telefon);
                }
            }
        }
        public string Pesel
        {
            get
            {
                return Item.Pesel;
            }
            set
            {
                if (value != Item.Pesel)
                {
                    Item.Pesel = value;
                    base.OnPropertyChanged(() => Pesel);
                }
            }
        }
        private DateTime _DataUrodzenia = DateTime.Now;
        public DateTime DataUrodzenia
        {
            get
            {
                return _DataUrodzenia;
            }
            set
            {
                if (value != _DataUrodzenia)
                {
                    _DataUrodzenia = value;
                    base.OnPropertyChanged(() => DataUrodzenia);
                    Item.DataUrodzenia = DataUrodzenia;
                }
            }
        }
        #endregion
        #region Dane kontaktowe
        public string Ulica
        {
            set
            {
                if (value != Item2.Ulica)
                {
                    Item2.Ulica = value;
                    base.OnPropertyChanged(() => Item2.Ulica);
                }
            }
        }
        public string Miasto
        {
            get
            {
                return Item2.Miasto;
            }
            set
            {
                if (value != Item2.Miasto)
                {
                    Item2.Miasto = value;
                    base.OnPropertyChanged(() => Item2.Miasto);
                }
            }
        }
        public int IdKraju
        {
            get
            {
                return Item2.IdKraju;
            }
            set
            {
                if (value != Item2.IdKraju)
                {
                    Item2.IdKraju = value;
                    base.OnPropertyChanged(() => Item2.IdKraju);
                }
            }
        }
        public string NrLokalu
        {
            get
            {
                return Item2.NumerLokalu;
            }
            set
            {
                if (value != Item2.NumerLokalu)
                {
                    Item2.NumerLokalu = value;
                    base.OnPropertyChanged(() => Item2.NumerLokalu);
                }
            }
        }
        public string NrDomu
        {
            get
            {
                return Item2.NumerDomu;
            }
            set
            {
                if (value != Item2.NumerDomu)
                {
                    Item2.NumerDomu = value;
                    base.OnPropertyChanged(() => Item2.NumerDomu);
                }
            }
        }
        public string KodPocztowy
        {
            get
            {
                return Item2.KodPocztowy;
            }
            set
            {
                if (value != Item2.KodPocztowy)
                {
                    Item2.KodPocztowy = value;
                    base.OnPropertyChanged(() => Item2.KodPocztowy);
                }
            }
        }
        #endregion
        #region Dane dodatkowe
        public int? IdKlasa
        {
            get
            {
                return Item.IdKlasy;
            }
            set
            {
                if (value != Item.IdKlasy)
                {
                    Item.IdKlasy = value;
                    base.OnPropertyChanged(() => IdKlasa);
                }
            }
        }
        public int IdStatusu
        {
            get
            {
                return Item.IdStatusu;
            }
            set
            {
                if (value != Item.IdStatusu)
                {
                    Item.IdStatusu = value;
                    base.OnPropertyChanged(() => IdStatusu);
                }
            }
        }
        public int? IdPrzedmiotu
        {
            get
            {
                return Item.IdPrzedmiotu;
            }
            set
            {
                if (value != Item.IdPrzedmiotu)
                {
                    Item.IdPrzedmiotu = value;
                    base.OnPropertyChanged(() => IdPrzedmiotu);
                }
            }
        }
        #endregion
        #endregion
        #region Comboboxy
        public IQueryable<KeyAndValue> PlcieComboboxItems
        {
            get
            {
                return new PodstawoweComboboxyLogic(Db).GetAktywnePlcie();
            }
        }
        public IQueryable<KeyAndValue> KrajeComboboxItems
        {
            get
            {
                return new PodstawoweComboboxyLogic(Db).GetAktywneKraje();
            }
        }
        public IQueryable<KeyAndValue> KlasyComboboxItems
        {
            get
            {
                return new PodstawoweComboboxyLogic(Db).GetAktywneKlasy();
            }
        }
        public IQueryable<KeyAndValue> StatusyComboboxItems
        {
            get
            {
                return new PodstawoweComboboxyLogic(Db).GetAktywneStatusy();
            }
        }
        #endregion
        #endregion
        #region Helpers
        public override void Save()
        {
            Item.CzyAktywny = true;
            Item2.CzyAktywny = true;
            Item.IdAdresu = Item2.IdAdresu;
            Db.Adres.AddObject(Item2);
            Db.Uzytkownik.AddObject(Item);
            Db.SaveChanges();
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
                if (name == "Imie")
                {
                    komunikat = StringValidator.SprawdzCzyZaczynaSieOdDuzej(Imie);
                    Wiadomosc = komunikat;
                }
                if (name == "Nazwisko")
                {
                    komunikat = StringValidator.SprawdzCzyZaczynaSieOdDuzej(Nazwisko);
                    Wiadomosc = komunikat;
                }
                if (name == "Telefon")
                {
                    komunikat = StringValidator.SprawdzCzyDlugoscTelefonuJestPoprawna(Telefon);
                    Wiadomosc = komunikat;
                }
                if (name == "Pesel")
                {
                    komunikat = StringValidator.SprawdzCzyDlugoscPeseluJestPoprawna(Pesel);
                    Wiadomosc = komunikat;
                }
                if (name == "DataUrodzenia")
                {
                    komunikat = DateValidator.SprawdzCzyWiekJestPoprawny(DataUrodzenia);
                    Wiadomosc = komunikat;
                }
                if (name == "KodPocztowy")
                {
                    komunikat = StringValidator.SprawdzCzyKodPocztowyJestPoprawny(KodPocztowy);
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
            if (this["Imie"] == null && this["Telefon"] == null && this["Pesel"] == null && this["DataUrodzenia"] == null)
            {
                return true;
            }
            return false;
        }
        #endregion
    }
}
