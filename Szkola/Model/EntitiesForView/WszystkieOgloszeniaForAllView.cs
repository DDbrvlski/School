using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Szkola.Model.EntitiesForView
{
    public class WszystkieOgloszeniaForAllView
    {
        #region Properties
        public string Tytul { get; set; }
        public string Tresc { get; set; }
        public bool CzyWazne { get; set; }
        public DateTime Data { get; set; }
        #endregion
    }
}
