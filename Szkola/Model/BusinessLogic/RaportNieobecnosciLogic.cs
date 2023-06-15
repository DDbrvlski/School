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
    //Klasa zawiera funkcje logiki dla Raportu Nieobecności
    public class RaportNieobecnosciLogic : DatabaseClass
    {
        public RaportNieobecnosciLogic(SzkolaEntities szkolaEntities) : base(szkolaEntities)
        {
        }
        //Funkcja zwraca listę z dostępnymi latami klas (1A, 1B, 1C -> 1 rok)
        public ObservableCollection<KeyAndValue> GetRok()
        {
            ObservableCollection<KeyAndValue> lista = new ObservableCollection<KeyAndValue>();
            lista.Add(new KeyAndValue(1, "1"));
            lista.Add(new KeyAndValue(2, "2"));
            lista.Add(new KeyAndValue(3, "3"));
            return lista;
        }
        //Funkcja zwraca listę uczniów z liczbami nieobecności od największej (Raport)
        public ObservableCollection<RaportNieobecnosciForAllView> GetRaportNieobecnosci(bool czyKlasa, int WybraneIdGrupy, int WybraneIdPrzedmiotu, DateTime dataOd, DateTime dataDo)
        {
            //czyKlasa sprawdza czy zwracamy listę uczniów dla klasy czy dla roku
            if (czyKlasa)
            {
                return new ObservableCollection<RaportNieobecnosciForAllView>(
                    from obecnosc in SzkolaEntities.Nieobecnosci
                    where obecnosc.CzyAktywny == true && obecnosc.Uzytkownik.Klasa1.IdKlasa == WybraneIdGrupy && obecnosc.Lekcja.IdPrzedmiotu == WybraneIdPrzedmiotu && obecnosc.DataNieobecnosci >= dataOd && obecnosc.DataNieobecnosci <= dataDo
                    group obecnosc by obecnosc.Uzytkownik into obecnoscGroup
                    orderby obecnoscGroup.Count() descending
                    select new RaportNieobecnosciForAllView
                    {
                        Imie = obecnoscGroup.Key.Imie,
                        Nazwisko = obecnoscGroup.Key.Nazwisko,
                        Klasa = obecnoscGroup.Key.Klasa1.NazwaKlasy,
                        Pesel = obecnoscGroup.Key.Pesel,
                        Plec = obecnoscGroup.Key.Plec.NazwaPlci,
                        LiczbaNieobecnosci = obecnoscGroup.Count()
                    }
                );
            }
            else
            {
                string idgrupy = WybraneIdGrupy.ToString();
                return new ObservableCollection<RaportNieobecnosciForAllView>(
                    from obecnosc in SzkolaEntities.Nieobecnosci
                    where obecnosc.CzyAktywny == true && obecnosc.Uzytkownik.Klasa1.NazwaKlasy.Contains(idgrupy) && obecnosc.Lekcja.IdPrzedmiotu == WybraneIdPrzedmiotu && obecnosc.DataNieobecnosci >= dataOd && obecnosc.DataNieobecnosci <= dataDo
                    group obecnosc by obecnosc.Uzytkownik into obecnoscGroup
                    orderby obecnoscGroup.Count() descending
                    select new RaportNieobecnosciForAllView
                    {
                        IdUcznia = obecnoscGroup.Key.IdUzytkownik,
                        Imie = obecnoscGroup.Key.Imie,
                        Nazwisko = obecnoscGroup.Key.Nazwisko,
                        Klasa = obecnoscGroup.Key.Klasa1.NazwaKlasy,
                        Pesel = obecnoscGroup.Key.Pesel,
                        Plec = obecnoscGroup.Key.Plec.NazwaPlci,
                        LiczbaNieobecnosci = obecnoscGroup.Count()
                    }
                );
            }
        }
        //Funkcja zwraca listę uczniów którzy przekroczyli limit nieobecności dla przedmiotu
        public ObservableCollection<RaportNieobecnosciLimitForAllView> GetRaportNieobecnosciLimit(ObservableCollection<RaportNieobecnosciForAllView> Raport, int IdPrzedmiotu)
        {
            string NazwaPrzedmiotu = SzkolaEntities.Przedmiot.First(x => x.IdPrzedmiot == IdPrzedmiotu).NazwaPrzedmiotu;
            return new ObservableCollection<RaportNieobecnosciLimitForAllView>(
                Raport
                .Where(o => o.LiczbaNieobecnosci >= 5)
                .Select(g => new RaportNieobecnosciLimitForAllView
                {
                    IdUcznia = g.IdUcznia,
                    Imie = g.Imie,
                    Nazwisko = g.Nazwisko,
                    Klasa = g.Klasa,
                    NazwaPrzedmiotu = NazwaPrzedmiotu,
                    LiczbaNieobecnosci = g.LiczbaNieobecnosci
                })
            );
        }
        //Funkcja wysyła powiadomienie do ucznia o przekroczeniu ilości nieobecności
        public void WyslijPowiadomienie(ObservableCollection<RaportNieobecnosciLimitForAllView> Raport)
        {
            if (Raport.Count() != 0)
            {
                //Tworzenie ogłoszenia/powiadomienia
                Ogloszenia ogloszenie = new Ogloszenia();
                ogloszenie.DataWyslania = DateTime.Today;
                ogloszenie.TytulOgloszenia = "Limit nieobecności!!!";
                ogloszenie.CzyWazne = true;
                ogloszenie.CzyAktywny = true;
                //Pobieranie przedmiotu z którego wysyłane jest powiadomienie w celu dodania do wiadomości
                string przedmiot = Raport.Select(x => x.NazwaPrzedmiotu).First();
                ogloszenie.TrescOgloszenia = "Przekroczono dozwolony limit nieobecności z przedmiotu " + przedmiot + "!\n\nSkontaktuj się z wychowawcą w celu dalszego postępowania.";
                Ogloszenia ogloszenieTemp = ogloszenie;
                SzkolaEntities.Ogloszenia.AddObject(ogloszenie);
                SzkolaEntities.SaveChanges();
                //Pobieranie id powiadomienia ktore zostalo dodane
                int idOgloszenia = SzkolaEntities.Ogloszenia.First(x => x.DataWyslania == ogloszenieTemp.DataWyslania && x.TytulOgloszenia == ogloszenieTemp.TytulOgloszenia && x.TrescOgloszenia == ogloszenieTemp.TrescOgloszenia).IdOgloszenie;
                //Wysyłanie powiadomienia do wszystkich uczniów którzy przekroczyli limit
                foreach (var item in Raport)
                {
                    OdbiorcyOgloszenia wiadomosc = new OdbiorcyOgloszenia();
                    wiadomosc.IdUzytkownika = item.IdUcznia;
                    wiadomosc.CzyAktywny = true;
                    wiadomosc.IdOgloszenia = idOgloszenia;
                    SzkolaEntities.OdbiorcyOgloszenia.AddObject(wiadomosc);
                }
                SzkolaEntities.SaveChanges();
            }
        }
        //Funkcja zwraca najwieksza liczbe nieobecnosci
        public int NajwiecejNieobecnosci(ObservableCollection<RaportNieobecnosciForAllView> Raport)
        {
            if (Raport.Count() != 0)
            {
                return Raport.Max(x => x.LiczbaNieobecnosci);
            }
            else
            {
                return 0;
            }
        }
        //Funkcja zwraca najmniejsza liczbe nieobecnosci
        public int NajmniejNieobecnosci(ObservableCollection<RaportNieobecnosciForAllView> Raport)
        {
            if (Raport.Count() != 0)
            {
                return Raport.Min(x => x.LiczbaNieobecnosci);
            }
            else
            {
                return 0;
            }
        }
        //Funkcja zwraca liczbe osob ktore przekroczyly limi nieobecnosci
        public int IleOsobPrzekroczyloLimit(ObservableCollection<RaportNieobecnosciForAllView> Raport)
        {
            if (Raport.Count() != 0)
            {
                return Raport.Where(x => x.LiczbaNieobecnosci >= 5).Count();
            }
            else
            {
                return 0;
            }
        }
        //Zwraca stringa z nazwą która płec otrzymała lepszą frekwencję
        public string KtoraPlecOtrzymalaLepszaFrekwencje(ObservableCollection<RaportNieobecnosciForAllView> Raport)
        {
            int kobietaLiczbaNieobecnosci = 0;
            int mezczyznaLiczbaNieobecnosci = 0;
            var kobieta = Raport.Where(p => p.Plec == "Kobieta");
            var mezczyzna = Raport.Where(p => p.Plec == "Mężczyzna");
            if (kobieta.Any())
            {
                kobietaLiczbaNieobecnosci = Raport.Where(p => p.Plec == "Kobieta").Count();
            }
            if (mezczyzna.Any())
            {
                mezczyznaLiczbaNieobecnosci = Raport.Where(p => p.Plec == "Mężczyzna").Count();
            }
            if (kobietaLiczbaNieobecnosci == 0 && mezczyznaLiczbaNieobecnosci == 0)
            {
                return "Żadna";
            }
            else if (kobietaLiczbaNieobecnosci == mezczyznaLiczbaNieobecnosci)
            {
                return "Obie";
            }
            else if (kobietaLiczbaNieobecnosci < mezczyznaLiczbaNieobecnosci)
            {
                return "Kobieta";
            }
            else
            {
                return "Mężczyzna";
            }
        }
        //Zwraca stringa z nazwą klasy która otrzymała lepszą frekwencje
        public string KtoraKlasaOtrzymalaLepszaFrekwencje(ObservableCollection<RaportNieobecnosciForAllView> Raport)
        {
            if (Raport.Count() != 0)
            {
                return Raport.GroupBy(p => p.Klasa).OrderBy(g => g.Count()).First().Key;
            }
            else
            {
                return "Brak";
            }
        }
        //Zwraca stringa z nazwą klasy która otrzymała gorszą frekwencje
        public string KtoraKlasaOtrzymalaGorszaFrekwencje(ObservableCollection<RaportNieobecnosciForAllView> Raport)
        {
            if (Raport.Count() != 0)
            {
                return Raport.GroupBy(p => p.Klasa).OrderByDescending(g => g.Count()).First().Key;
            }
            else
            {
                return "Brak";
            }
        }
    }
}
