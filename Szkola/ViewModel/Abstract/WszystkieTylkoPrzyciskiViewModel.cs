using Firma.Helper;
using GalaSoft.MvvmLight.Messaging;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Szkola.Model.Entities;

namespace Szkola.ViewModel.Abstract
{
    public abstract class WszystkieTylkoPrzyciskiViewModel<T> : WorkspaceViewModel
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
        public WszystkieTylkoPrzyciskiViewModel(string displayName)
        {
            base.DisplayName = displayName;
            this.szkolaEntities = new SzkolaEntities();
        }
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
