using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Szkola.Model.EntitiesForView
{
    public class TydzienForAllView<T>
    {
        #region Properties
        public string Godzina { get; set; }
        public T Pon { get; set; }
        public T Wt { get; set; }
        public T Sr { get; set; }
        public T Czw { get; set; }
        public T Pt { get; set; }
        #endregion
    }
}
