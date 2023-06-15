using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Szkola.Model.EntitiesForView
{
    public class ObecnosciUczniaForAllView
    {
        #region Properties
        public DateTime DataTemp { get; set; }
        public string Data
        {
            get { return DataTemp.ToString("dd.MM.yyyy"); }
        }
        public string Lekcja { get; set; }
        public string GodzinaLekcyjna { get; set; }
        #endregion
    }
}
