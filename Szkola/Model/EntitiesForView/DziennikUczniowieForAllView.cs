using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Szkola.Model.EntitiesForView
{
    public class DziennikUczniowieForAllView
    {
        #region Properties
        public int IdUcznia { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public string Klasa { get; set; }
        public double Srednia { get; set; }
        #endregion
    }
}
