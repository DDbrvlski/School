using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Szkola.Model.Entities;

namespace Szkola.Model.Validators
{
    public class ComboboxValidator : Validator
    {
        public static string SprawdzCzyWybranyNauczycielMaJuzKlase(int idNauczyciel, int idKlasa , SzkolaEntities szkola)
        {
            try
            {
                var wychowawca = szkola.Klasa.First(x => x.IdWychowawcy == idNauczyciel);
                var klasa = szkola.Klasa.First(x => x.IdKlasa == idKlasa);
                if (wychowawca != null && wychowawca.IdWychowawcy != klasa.IdWychowawcy)
                {
                    return "Podany wychowawca ma już przypisaną klasę, wybierz innego.";
                }
                return null;
            }
            catch (Exception) { }
            return null;

        }
        public static string SprawdzCzyWybranaSalaMaJuzKlase(int idSali, int idKlasa, SzkolaEntities szkola)
        {
            try
            {
                var sala = szkola.Klasa.First(x => x.IdSaliLekcyjnej == idSali);
                var klasa = szkola.Klasa.First(x => x.IdKlasa == idKlasa);
                if (sala != null && sala.IdSaliLekcyjnej != klasa.IdSaliLekcyjnej)
                {
                    return "Podana sala ma już przypisaną klasę, wybierz inną.";
                }
                return null;
            }
            catch (Exception) { }
            return null;

        }
    }
}
