using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Szkola.Model.Validators
{
    public class StringValidator : Validator
    {
        public static string SprawdzCzyZaczynaSieOdDuzej(string wartosc)
        {
            try
            {
                if (!char.IsUpper(wartosc, 0))
                {
                    return "Rozpocznij dużą literą";
                }
                return null;
            }
            catch (Exception) { }
            return null;

        }
        public static string SprawdzTytulOgloszeniaMaOdpowiedniaDlugosc(string wartosc)
        {
            try
            {
                if (wartosc.Count()>20)
                {
                    return "Za długi tytuł";
                }
                return null;
            }
            catch (Exception) { }
            return null;

        }
        public static string SprawdzCzyDlugoscTelefonuJestPoprawna(string wartosc)
        {
            try
            {
                if (wartosc.Count() != 9)
                {
                    return "Niepoprawny numer telefonu";
                }
                return null;
            }
            catch (Exception) { }
            return null;

        }
        public static string SprawdzCzyTelefonJestNumerem(string wartosc)
        {
            try
            {
                if (!wartosc.All(char.IsDigit))
                {
                    return "Niepoprawny numer telefonu";
                }
                return null;
            }
            catch (Exception) { }
            return null;

        }

        public static string SprawdzCzyDlugoscPeseluJestPoprawna(string wartosc)
        {
            try
            {
                if (wartosc.Count() != 11)
                {
                    return "Niepoprawny pesel";
                }
                return null;
            }
            catch (Exception) { }
            return null;

        }
        public static string SprawdzCzyPeselJestNumerem(string wartosc)
        {
            try
            {
                if (!wartosc.All(char.IsDigit))
                {
                    return "Niepoprawny pesel";
                }
                return null;
            }
            catch (Exception) { }
            return null;

        }
        public static string SprawdzCzyKodPocztowyJestPoprawny(string wartosc)
        {
            try
            {
                if (wartosc.Count()>7 || wartosc.Count() < 4)
                {
                    return "Niepoprawny kod pocztowy";
                }
                return null;
            }
            catch (Exception) { }
            return null;

        }
        public static string SprawdzCzyKodPocztowyJestNumerem(string wartosc)
        {
            try
            {
                if (!wartosc.All(char.IsDigit))
                {
                    return "Niepoprawny kod pocztowy";
                }
                return null;
            }
            catch (Exception) { }
            return null;

        }
    }
}
