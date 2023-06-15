using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Szkola.Model.EntitiesForView
{
    public class KlasyForAllView
    {
        #region Properties
        public int IdKlasy { get; set; }
        public string Klasa { get; set; }
        public string Nauczyciel { get; set; }
        public int IloscUczniow { get; set; }
        public string NumerSali { get; set; }
        #endregion
    }
}
