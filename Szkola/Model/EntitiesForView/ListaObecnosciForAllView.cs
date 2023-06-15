using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Szkola.Model.EntitiesForView
{
    public class ListaObecnosciForAllView
    {
        #region Properties
        public int IdUcznia { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public string Pesel { get; set; }
        public bool IsSelected { get; set; }
        #endregion
    }
}
