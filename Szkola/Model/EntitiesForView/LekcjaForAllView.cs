using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Szkola.Model.BusinessLogic;
using Szkola.Model.Entities;

namespace Szkola.Model.EntitiesForView
{
    public class LekcjaForAllView
    {
        #region Properties
        public int WybraneIdPrzedmiotu { get; set; }
        public int WybraneIdDnia { get; set; }
        public int WybraneIdGodziny { get; set; }
        public int WybraneIdKlasy { get; set; }
        #endregion

        #region Combobox
        public IQueryable<KeyAndValue> PrzedmiotyComboboxItems
        {
            get
            {
                return new PodstawoweComboboxyLogic(new SzkolaEntities()).GetAktywnePrzedmioty();
            }
        }
        #endregion
    }
}
