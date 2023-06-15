using Firma.Helper;
using Firma.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Szkola.ViewModel
{
    public class WorkspaceViewModel : BaseViewModel
    {
        #region Pola i Komendy
        //każda zakładka ma minimum nazwę i zamknij
        public string DisplayName { get; set; } //w tym propertisie będzie przechowywana nazwa zakładki
        private BaseCommand _CloseCommand; //to jest komenda do obsługi zamykania okna
        public ICommand CloseCommand
        {
            get
            {
                if (_CloseCommand == null)
                {
                    _CloseCommand = new BaseCommand(() => this.OnRequestClose()); //ta komenda wywola funkcje zamykajaca zakladke
                }
                return _CloseCommand;
            }
        }
        #endregion
        #region RequestClose [event] 
        public event EventHandler RequestClose;
        protected void OnRequestClose()
        {
            EventHandler handler = this.RequestClose;
            if (handler != null) handler(this, EventArgs.Empty);
        }
        #endregion
    }
}
