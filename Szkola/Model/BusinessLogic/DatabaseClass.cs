using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Szkola.Model.Entities;

namespace Szkola.Model.BusinessLogic
{
    //To jest klasa z ktorej beda dziedziczyc wszystkie klasy logiki biznesowej, ktore uzywaja bazy danych.
    public class DatabaseClass
    {
        #region Entities
        public SzkolaEntities SzkolaEntities { get; set; }
        #endregion

        #region Konstruktor
        public DatabaseClass(SzkolaEntities szkolaEntities)
        {
            this.SzkolaEntities = szkolaEntities;
        }
        #endregion
    }
}
