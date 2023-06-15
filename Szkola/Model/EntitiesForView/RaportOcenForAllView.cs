using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Szkola.Model.EntitiesForView
{
    public class RaportOcenForAllView
    {
        #region Properties
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public string Klasa { get; set; }
        public string Plec { get; set; }
        public string Pesel { get; set; }
        public double SredniaOcen { get; set; }
        #endregion
    }
}
