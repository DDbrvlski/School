using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Szkola.Model.Validators
{
    public class DateValidator : Validator
    {
        public static string SprawdzCzyWiekJestPoprawny(DateTime wartosc)
        {
            try
            {
                if ((DateTime.Now.Year - wartosc.Year) < 7 || (DateTime.Now.Year - wartosc.Year) > 99)
                {
                    return "Niepoprawna data";
                }
                return null;
            }
            catch (Exception) { }
            return null;

        }
    }
}
