using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Szkola.Model.BusinessLogic;
using Szkola.Model.Entities;

namespace Szkola.Model.EntitiesForView
{
    public class KlasyUczniowieForAllView
    {
        #region Properties
        public int Id { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public string Pesel { get; set; }
        public int WybraneIdOceny { get; set; }
        public bool IsSelected { get; set; }
        #endregion

        #region Combobox
        public IQueryable<KeyAndValue> OcenyComboboxItems
        {
            get
            {
                return new PodstawoweComboboxyLogic(new SzkolaEntities()).GetAktywneOceny();
            }
        }
        #endregion

    }
}
