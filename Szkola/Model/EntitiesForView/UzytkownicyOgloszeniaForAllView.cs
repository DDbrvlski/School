using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Szkola.Model.EntitiesForView
{
    public class UzytkownicyOgloszeniaForAllView
    {
        #region Properties
        public int Id { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public string Klasa { get; set; }
        public string Status { get; set; }
        public bool IsSelected { get; set; }
        #endregion
    }
}
