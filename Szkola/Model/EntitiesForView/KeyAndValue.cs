using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Szkola.Model.EntitiesForView
{
    public class KeyAndValue
    {
        #region Konstruktor
        public KeyAndValue()
        { }
        public KeyAndValue(int Key, string Value)
        {
            this.Key = Key;
            this.Value = Value;
        }
        #endregion
        #region Properties
        public int Key { get; set; }
        public string Value { get; set; }
        #endregion
    }
}
