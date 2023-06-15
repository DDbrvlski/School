using Firma.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Szkola.Model.Entities;

namespace Szkola.ViewModel.Abstract
{
    public abstract class DodajViewModel<T> : WorkspaceViewModel
    {
        #region Fields
        public SzkolaEntities Db { get; set; }
        public string Wiadomosc { get; set; }
        public T Item { get; set; }
        #endregion
        #region Konstruktor
        public DodajViewModel(string displayName)
        {
            base.DisplayName = displayName;//tu ustawiamy nazwę zakładki
            Db = new SzkolaEntities();
        }
        #endregion
        #region Commands
        //to jest komenda która zostanie podpięta (zbindowana) z przyciskiem zapisz i zamknij. Komenda ta wywola
        //fukcje saveAndClose()
        private BaseCommand _AddNewCommand;
        public ICommand AddNewCommand
        {
            get
            {
                if (_AddNewCommand == null) { }
                {
                    _AddNewCommand = new BaseCommand(() => addNew());
                }
                return _AddNewCommand;
            }
        }
        private BaseCommand _CancelCommand;
        public ICommand CancelCommand
        {
            get
            {
                if (_CancelCommand == null) { }
                {
                    _CancelCommand = new BaseCommand(() => cancel());
                }
                return _CancelCommand;
            }
        }
        #endregion
        #region Save (Helpers)
        public abstract void Save();
        private void addNew()
        {
            if (IsValid())
            {
                Save();
                OnRequestClose();
            }
            else
            {
                MessageBox.Show(Wiadomosc);
            }
        }
        private void cancel()
        {
            OnRequestClose();
        }
        #endregion
        #region Validation
        public virtual bool IsValid() { return true; }
        #endregion
    }
}
