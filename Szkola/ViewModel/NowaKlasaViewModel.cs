using Firma.Helper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Szkola.Model.BusinessLogic;
using Szkola.Model.Entities;
using Szkola.Model.EntitiesForView;
using Szkola.Model.Validators;
using Szkola.ViewModel.Abstract;

namespace Szkola.ViewModel
{
    public class NowaKlasaViewModel : DodajViewModel<Klasa>, IDataErrorInfo
    {
        #region Konstruktor
        public NowaKlasaViewModel() : base("Klasa")
        {
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
        private int _WybranyWychowawcaKlasy;
        public int WybranyWychowawcaKlasy
        {
            get
            {
                return _WybranyWychowawcaKlasy;
            }
            set
            {
                if (value != _WybranyWychowawcaKlasy)
                {
                    _WybranyWychowawcaKlasy = value;
                    base.OnPropertyChanged(() => WybranyWychowawcaKlasy);
                }


            }
        }
        private int _WybranaSalaLekcyjna;
        public int WybranaSalaLekcyjna
        {
            get
            {
                return _WybranaSalaLekcyjna;
            }
            set
            {
                if (value != _WybranaSalaLekcyjna)
                {
                    _WybranaSalaLekcyjna = value;
                    base.OnPropertyChanged(() => WybranaSalaLekcyjna);
                }


            }
        }
        private KlasyUczniowieForAllView _WybranyUczen;
        public KlasyUczniowieForAllView WybranyUczen
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
        private KlasyUczniowieForAllView _WybranyUczenZKlasy;
        public KlasyUczniowieForAllView WybranyUczenZKlasy
        {
            get
            {
                return _WybranyUczenZKlasy;
            }
            set
            {
                if (value != _WybranyUczenZKlasy)
                {
                    _WybranyUczenZKlasy = value;
                    base.OnPropertyChanged(() => WybranyUczenZKlasy);
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
                if (_UczniowieList == null) UczniowieList = new KlasaLogic(Db).GetPozostaliUczniowieList();
                return _UczniowieList;
            }
            set
            {
                _UczniowieList = value;
                OnPropertyChanged(() => UczniowieList);
            }
        }
        private ObservableCollection<KlasyUczniowieForAllView> _UczniowieZKlasyList;
        public ObservableCollection<KlasyUczniowieForAllView> UczniowieZKlasyList
        {
            get
            {
                if (_UczniowieZKlasyList == null) UczniowieZKlasyList = new KlasaLogic(Db).GetUczniowieZKlasyList(WybraneIdKlasy);
                return _UczniowieZKlasyList;
            }
            set
            {
                _UczniowieZKlasyList = value;
                OnPropertyChanged(() => UczniowieZKlasyList);
            }
        }
        #endregion
        #region Comboboxy
        public IQueryable<KeyAndValue> KlasyComboboxItems
        {
            get
            {
                return new PodstawoweComboboxyLogic(Db).GetAktywneKlasy();
            }
        }
        public IQueryable<KeyAndValue> SaleLekcyjneComboboxItems
        {
            get
            {
                return new PodstawoweComboboxyLogic(Db).GetAktywneSaleLekcyjne();
            }
        }
        public IQueryable<KeyAndValue> WychowawcyComboboxItems
        {
            get
            {
                return new KlasaLogic(Db).GetAktywniWychowawcy();
            }
        }
        #endregion
        #endregion
        #region Komendy
        private BaseCommand _RemoveUserCommand;
        public ICommand RemoveUserCommand
        {
            get
            {
                if (_RemoveUserCommand == null) { }
                {
                    _RemoveUserCommand = new BaseCommand(() => removeUser());
                }
                return _RemoveUserCommand;
            }
        }
        private BaseCommand _AddUserCommand;
        public ICommand AddUserCommand
        {
            get
            {
                if (_AddUserCommand == null) { }
                {
                    _AddUserCommand = new BaseCommand(() => addUser());
                }
                return _AddUserCommand;
            }
        }
        private BaseCommand _ShowClassUsersCommand;
        public ICommand ShowClassUsersCommand
        {
            get
            {
                if (_ShowClassUsersCommand == null) { }
                {
                    _ShowClassUsersCommand = new BaseCommand(() => showUsers());
                }
                return _ShowClassUsersCommand;
            }
        }
        #endregion
        #region Helpers
        public override void Save()
        {
            var klasa = Db.Klasa.First(x => x.IdKlasa == WybraneIdKlasy);
            var wychowawca = Db.Uzytkownik.First(x => x.IdUzytkownik == WybranyWychowawcaKlasy);

            klasa.IdWychowawcy = WybranyWychowawcaKlasy;
            if (wychowawca.IdKlasy != klasa.IdWychowawcy)
            {
                wychowawca.IdKlasy = null;
            }

            klasa.IdSaliLekcyjnej = WybranaSalaLekcyjna;
            Db.SaveChanges();
        }

        public void showUsers()
        {
            UczniowieZKlasyList = new KlasaLogic(Db).GetUczniowieZKlasyList(WybraneIdKlasy);
            UczniowieList = new KlasaLogic(Db).GetPozostaliUczniowieList();
            WybranyWychowawcaKlasy = new KlasaLogic(Db).GetAktywnyWychowawcaZWybranejKlasy(WybraneIdKlasy);
            WybranaSalaLekcyjna = new KlasaLogic(Db).GetAktywnaSalaLekcyjnaZWybranejKlasy(WybraneIdKlasy);
        }

        public void removeUser()
        {
            if (WybranyUczenZKlasy != null)
            {
                var uczen = Db.Uzytkownik.First(x => x.Pesel == WybranyUczenZKlasy.Pesel);
                uczen.IdKlasy = null;
                Db.SaveChanges();
                showUsers();
            }
        }
        public void addUser()
        {
            if (WybranyUczen != null && WybraneIdKlasy > 0)
            {
                var uczen = Db.Uzytkownik.First(x => x.Pesel == WybranyUczen.Pesel);
                uczen.IdKlasy = WybraneIdKlasy;
                Db.SaveChanges();
                showUsers();
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
                if (name == "WybranyWychowawcaKlasy")
                {
                    komunikat = ComboboxValidator.SprawdzCzyWybranyNauczycielMaJuzKlase(WybranyWychowawcaKlasy, WybraneIdKlasy, Db);
                    Wiadomosc = komunikat;
                }
                if (name == "WybranaSalaLekcyjna")
                {
                    komunikat = ComboboxValidator.SprawdzCzyWybranaSalaMaJuzKlase(WybranaSalaLekcyjna, WybraneIdKlasy, Db);
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
            if (this["WybranyWychowawcaKlasy"] == null && this["WybranaSalaLekcyjna"] == null)
            {
                return true;
            }
            return false;
        }
        #endregion
    }
}
