using Firma.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Szkola.ViewModel
{
    public class CommandViewModel : BaseViewModel
    {
        #region Wlasciwosci
        public string DisplayName { get; set; }
        public string DisplayImage { get; set; }
        public ICommand Command { get; set; } 
        #endregion
        #region Konstruktor
        public CommandViewModel(string displayName, string displayImage, ICommand command)
        {
            if (command == null)
            {
                throw new ArgumentNullException("Command");
            }
            this.DisplayName = displayName;
            this.DisplayImage = displayImage;
            this.Command = command;
        }
        #endregion
    }
}
