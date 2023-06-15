using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Szkola.Model.Entities;
using Szkola.Model.EntitiesForView;

namespace Szkola.Model.BusinessLogic
{
    //Klasa zawiera funkcje logiki dla Raportu Ocen
    public class RaportOcenLogic : DatabaseClass
    {
        public RaportOcenLogic(SzkolaEntities szkolaEntities) : base(szkolaEntities) { }
        //Funkcja zwraca listę z dostępnymi latami klas (1A, 1B, 1C -> 1 rok)
        public ObservableCollection<KeyAndValue> GetRok()
        {
            ObservableCollection<KeyAndValue> lista = new ObservableCollection<KeyAndValue>();
            lista.Add(new KeyAndValue(1, "1"));
            lista.Add(new KeyAndValue(2, "2"));
            lista.Add(new KeyAndValue(3, "3"));
            return lista;
        }
        //Funkcja zwraca listę uczniów z liczbami nieobecności od największej
        public ObservableCollection<KeyAndValue> GetKlasy()
        {
            return new ObservableCollection<KeyAndValue>(
                    from klasa in SzkolaEntities.Klasa
                    where klasa.CzyAktywny == true
                    select new KeyAndValue
                    {
                        Key = klasa.IdKlasa,
                        Value = klasa.NazwaKlasy
                    }
                );
        }
        //Funkcja zwraca listę uczniów z ich średnimi z ocen z danego przedmiotu od największej (Raport)
        public ObservableCollection<RaportOcenForAllView> GetRaportOcen(bool czyKlasa, int WybraneIdGrupy, int WybraneIdPrzedmiotu, int WybraneIdFormySprawdzeniaWiedzy, DateTime dataOd, DateTime dataDo)
        {
            if (czyKlasa)
            {
                return new ObservableCollection<RaportOcenForAllView>(
                    from ocena in SzkolaEntities.Oceny
                    where ocena.CzyAktywny == true && ocena.Uzytkownik.Klasa1.IdKlasa == WybraneIdGrupy && ocena.IdPrzedmiotu == WybraneIdPrzedmiotu && ocena.DataDodaniaOceny >= dataOd && ocena.DataDodaniaOceny <= dataDo && ocena.IdFormySprawdzeniaWiedzy == WybraneIdFormySprawdzeniaWiedzy
                    group ocena by ocena.Uzytkownik into ocenaGroup
                    orderby ocenaGroup.Average(o => o.NazwyOcen.WartoscOceny) descending
                    select new RaportOcenForAllView
                    {
                        Imie = ocenaGroup.Key.Imie,
                        Nazwisko = ocenaGroup.Key.Nazwisko,
                        Klasa = ocenaGroup.Key.Klasa1.NazwaKlasy,
                        Pesel = ocenaGroup.Key.Pesel,
                        Plec = ocenaGroup.Key.Plec.NazwaPlci,
                        SredniaOcen = Math.Round((double)((ocenaGroup.Any() ? ocenaGroup.Average(o => o.NazwyOcen.WartoscOceny) : 0) ?? 0), 2)
                    }
                );
            }
            else
            {
                string idgrupy = WybraneIdGrupy.ToString();
                return new ObservableCollection<RaportOcenForAllView>(
                    from ocena in SzkolaEntities.Oceny
                    where ocena.CzyAktywny == true && ocena.Uzytkownik.Klasa1.NazwaKlasy.Contains(idgrupy) && ocena.IdPrzedmiotu == WybraneIdPrzedmiotu && ocena.DataDodaniaOceny >= dataOd && ocena.DataDodaniaOceny <= dataDo && ocena.IdFormySprawdzeniaWiedzy == WybraneIdFormySprawdzeniaWiedzy
                    group ocena by ocena.Uzytkownik into ocenaGroup
                    orderby ocenaGroup.Average(o => o.NazwyOcen.WartoscOceny) descending
                    select new RaportOcenForAllView
                    {
                        Imie = ocenaGroup.Key.Imie,
                        Nazwisko = ocenaGroup.Key.Nazwisko,
                        Klasa = ocenaGroup.Key.Klasa1.NazwaKlasy,
                        Pesel = ocenaGroup.Key.Pesel,
                        Plec = ocenaGroup.Key.Plec.NazwaPlci,
                        SredniaOcen = Math.Round((double)((ocenaGroup.Any() ? ocenaGroup.Average(o => o.NazwyOcen.WartoscOceny) : 0) ?? 0), 2)
                    }
                );
            }
        }
        //Funkcja zwraca listę średnich z raportu ocen uczniów oraz procent ile ich wystąpiło od największej (Raport)
        public ObservableCollection<RaportOcenProcentowo> GetRaportOcenProcentowo(ObservableCollection<RaportOcenForAllView> Raport)
        {
            var result = Raport
                .GroupBy(o => o.SredniaOcen)
                .Select(g => new {
                Ocena = g.Key,
                Procent = g.Count() * 100.0 / Raport.Count()
            });
            return new ObservableCollection<RaportOcenProcentowo>(result.Select(x => new RaportOcenProcentowo
            {
                Ocena = x.Ocena.ToString(),
                IleProcent = x.Procent.ToString() + "%"
            }));
        }
        //Funkcja zwraca najwyższą ocenę z raportu
        public double NajwyzszaOcena(ObservableCollection<RaportOcenForAllView> Raport)
        {
            if (Raport.Count() != 0)
            {
                return Raport.Max(x => x.SredniaOcen);
            }
            else
            {
                return 0;
            }
        }
        //Funkcja zwraca najniższą ocenę z raportu
        public double NajnizszaOcena(ObservableCollection<RaportOcenForAllView> Raport)
        {
            if (Raport.Count() != 0)
            {
                return Raport.Min(x => x.SredniaOcen);
            }
            else
            {
                return 0;
            }
        }
        //Funkcja zwraca ilość najwyższych ocen z raportu
        public int IleNajwyzszychOcen(ObservableCollection<RaportOcenForAllView> Raport)
        {
            double max = NajwyzszaOcena(Raport);
            if (max != 0)
            {
                return Raport.Where(x => x.SredniaOcen == max).Count();
            }
            else
            {
                return 0;
            }
        }
        //Funkcja zwraca strina z nazwą która płeć ma lepsze wyniki w ocenach
        public string KtoraPlecOtrzymalaLepszeWyniki(ObservableCollection<RaportOcenForAllView> Raport)
        {
            double kobietaSrednia = 0;
            double mezczyznaSrednia = 0;
            var kobieta = Raport.Where(p => p.Plec == "Kobieta");
            var mezczyzna = Raport.Where(p => p.Plec == "Mężczyzna");
            if (kobieta.Any())
            {
                kobietaSrednia = (double)(Raport.Where(p => p.Plec == "Kobieta").DefaultIfEmpty().Average(x => x.SredniaOcen));
            }
            if (mezczyzna.Any())
            {
                mezczyznaSrednia = (double)(Raport.Where(p => p.Plec == "Mężczyzna").DefaultIfEmpty().Average(x => x.SredniaOcen));
            }
            if (kobietaSrednia == 0 && mezczyznaSrednia == 0)
            {
                return "Żadna";
            }
            else if (kobietaSrednia == mezczyznaSrednia)
            {
                return "Obie";
            }
            else if (kobietaSrednia > mezczyznaSrednia)
            {
                return "Kobieta";
            }
            else
            {
                return "Mężczyzna";
            }
        }
        //Funkcja zwraca strina z nazwą która klasa ma lepsze wyniki w ocenach
        public string KtoraKlasaOtrzymalaLepszeWyniki(ObservableCollection<RaportOcenForAllView> Raport)
        {
            if (Raport.Count() != 0)
            {
                return Raport.GroupBy(p => p.Klasa).OrderByDescending(g => g.Average(x => x.SredniaOcen)).First().Key;
            }
            else
            {
                return "Brak";
            }
        }
        //Funkcja zwraca strina z nazwą która klasa ma gorsze wyniki w ocenach
        public string KtoraKlasaOtrzymalaGorszeWyniki(ObservableCollection<RaportOcenForAllView> Raport)
        {
            if (Raport.Count() != 0)
            {
                return Raport.GroupBy(p => p.Klasa).OrderBy(g => g.Average(x => x.SredniaOcen)).First().Key;
            }
            else
            {
                return "Brak";
            }
        }
    }
}
