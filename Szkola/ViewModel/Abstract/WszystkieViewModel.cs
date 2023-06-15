using Firma.Helper;
using GalaSoft.MvvmLight.Messaging;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Szkola.Model.Entities;

namespace Szkola.ViewModel.Abstract
{
    public abstract class WszystkieViewModel<T>:WorkspaceViewModel
    {
        #region Fields
        private readonly SzkolaEntities szkolaEntities;
        public SzkolaEntities SzkolaEntities
        {
            get { return szkolaEntities; }
        }

        private BaseCommand _AddCommand;
        public ICommand AddCommand
        {
            get
            {
                if (_AddCommand == null)
                {
                    _AddCommand = new BaseCommand(() => add()); //pusta wywoluje load
                }
                return _AddCommand;
            }
        }

        private BaseCommand _DeleteCommand;
        public ICommand DeleteCommand
        {
            get
            {
                if (_DeleteCommand == null)
                {
                    _DeleteCommand = new BaseCommand(() => Delete()); //pusta wywoluje load
                }
                return _DeleteCommand;
            }
        }

        private BaseCommand _LoadCommand;
        public ICommand LoadCommand
        {
            get
            {
                if (_LoadCommand == null)
                {
                    _LoadCommand = new BaseCommand(() => Load());
                }
                return _LoadCommand;
            }
        }
        private ObservableCollection<T> _List;
        public ObservableCollection<T> List
        {
            get
            {
                if (_List == null) 
                    Load();
                return _List;
            }
            set
            {
                _List = value;
                OnPropertyChanged(() => List);
            }
        }
        #endregion
        #region Kontruktor
        public WszystkieViewModel(string displayName)
        {
            base.DisplayName = displayName;
            this.szkolaEntities = new SzkolaEntities();
        }
        #endregion

        #region Sort
        //To jest komenda która zostanie podpięta pod przycisk sortuj
        //wywoła ona funkcje Sort()
        private BaseCommand _SortCommand;
        public ICommand SortCommand
        {
            get
            {
                if (_SortCommand == null)
                {
                    _SortCommand = new BaseCommand(() => Sort());
                }
                return _SortCommand;
            }
        }
        public abstract void Sort();
        //To jest pole do przechowywania wyboru tego po czym będziemy sortować
        public string SortField { get; set; }
        //To jest propertis który wypełnia opcje wyboru w comboboxie na bazie funkcji GetComboboxSortList()
        public List<string> SortComboboxItems
        {
            get
            {
                return GetComboboxSortList();
            }
        }
        public abstract List<string> GetComboboxSortList();
        #endregion

        #region Filtrowanie
        //To jest komenda która zostanie podpięta pod przycisk Filtruj/Szukaj
        //Wywoła ona funkcje Find()
        private BaseCommand _FindCommand; //_ oznacza ze bedzie propertis
        public ICommand FindCommand
        {
            get
            {
                if (_FindCommand == null)
                {
                    _FindCommand = new BaseCommand(() => Find());
                }
                return _FindCommand;
            }
        }
        public abstract void Find();

        //To jest właściwość w której zostanie zapisany wybór pola po którym będziemy wyszukiwać
        public string FindField { get; set; }

        //To jest właściwość w której zostanie zapisany początek frazy wyszukującej i która zostanie podpięta pod textbox
        public string FindTextBox { get; set; }

        public List<string> FindComboboxItems
        {
            get
            {
                return GetComboboxFindList();
            }
        }
        public abstract List<string> GetComboboxFindList();
        #endregion

        #region Helpers
        public abstract void Load();
        public abstract void Delete();
        public void add()
        {
            Messenger.Default.Send(DisplayName + "Add");
        }
        #endregion
    }
}
