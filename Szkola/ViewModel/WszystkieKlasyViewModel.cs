using Firma.Helper;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Szkola.Helpers.Messenger;
using Szkola.Model.BusinessLogic;
using Szkola.Model.EntitiesForView;
using Szkola.ViewModel.Abstract;

namespace Szkola.ViewModel
{
    public class WszystkieKlasyViewModel : WszystkieViewModel<KlasyForAllView>
    {
        #region Konstruktor
        public WszystkieKlasyViewModel() : base("Klasy")
        {
            Messenger.Default.Register<string>(this, getMessage);
        }
        #endregion
        #region Properties
        #region Pola
        private string messageInfo = "";
        private KlasyForAllView _WybranaKlasa;
        public KlasyForAllView WybranaKlasa
        {
            get { return _WybranaKlasa; }
            set
            {
                if (value != _WybranaKlasa)
                {
                    _WybranaKlasa = value;
                    if (messageInfo != "")
                    {
                        Messenger.Default.Send(new Message<KlasyForAllView>(messageInfo, WybranaKlasa));
                        OnRequestClose();
                    }
                    base.OnPropertyChanged(() => WybranaKlasa);
                }
            }
        }
        #endregion
        #region Listy
        private ObservableCollection<KlasyUczniowieWiecejForAllView> _UczniowieZKlasyList;
        public ObservableCollection<KlasyUczniowieWiecejForAllView> UczniowieZKlasyList
        {
            get
            {
                return _UczniowieZKlasyList;
            }
            set
            {
                _UczniowieZKlasyList = value;
                OnPropertyChanged(() => UczniowieZKlasyList);
            }
        }
        #endregion
        #endregion
        #region Komendy
        private BaseCommand _ShowStudensCommand;
        public ICommand ShowStudensCommand
        {
            get
            {
                if (_ShowStudensCommand == null) { }
                {
                    _ShowStudensCommand = new BaseCommand(() => showStudents());
                }
                return _ShowStudensCommand;
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
            List = new ObservableCollection<KlasyForAllView>
                (
                    from Kl in SzkolaEntities.Klasa
                    where Kl.CzyAktywny == true
                    select new KlasyForAllView
                    {
                        IdKlasy = Kl.IdKlasa,
                        Klasa = Kl.NazwaKlasy,
                        Nauczyciel = Kl.Uzytkownik.Imie + " " + Kl.Uzytkownik.Nazwisko,
                        IloscUczniow = (int)SzkolaEntities.Uzytkownik.Where(u => u.IdKlasy == Kl.IdKlasa && u.IdStatusu == 1).Count(),
                        NumerSali = Kl.SalaLekcyjna.NumerSaliLekcyjnej
                    }
                );
        }
        private void showStudents()
        {
            if (WybranaKlasa != null)
            {
                UczniowieZKlasyList = new KlasaLogic(SzkolaEntities).GetUczniowieZWybranejKlasyList(WybranaKlasa.IdKlasy);
            }
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
            if (SortField == "Klasa")
            {
                List = new ObservableCollection<KlasyForAllView>(List.OrderBy(item => item.Klasa));
            }
            if (SortField == "Nauczyciel")
            {
                List = new ObservableCollection<KlasyForAllView>(List.OrderBy(item => item.Nauczyciel));
            }
            if (SortField == "LiczbaUczniow")
            {
                List = new ObservableCollection<KlasyForAllView>(List.OrderBy(item => item.IloscUczniow));
            }
            if (SortField == "NumerSali")
            {
                List = new ObservableCollection<KlasyForAllView>(List.OrderBy(item => item.NumerSali));
            }
        }
        public override List<string> GetComboboxSortList()
        {
            return new List<string>
            {
                "Klasa",
                "Nauczyciel",
                "LiczbaUczniow",
                "NumerSali"
            };
        }
        public override void Find()
        {
            if (FindField == "Nauczyciel")
            {
                List = new ObservableCollection<KlasyForAllView>(List.Where(item => item.Nauczyciel != null && item.Nauczyciel.StartsWith(FindTextBox)));
            }
            if (FindField == "Klasa")
            {
                List = new ObservableCollection<KlasyForAllView>(List.Where(item => item.Klasa != null && item.Klasa.StartsWith(FindTextBox)));
            }
        }
        public override List<string> GetComboboxFindList()
        {
            return new List<string>
            {
                "Nauczyciel",
                "Klasa"
            };
        }
        #endregion
        
        
    }
}
