using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Szkola.Model.Entities;
using Szkola.Model.EntitiesForView;

namespace Szkola.Model.BusinessLogic
{
    //Klasa zawiera funkcje logiki dla Klasy
    public class KlasaLogic : DatabaseClass
    {
        #region Konstruktor
        public KlasaLogic(SzkolaEntities szkolaEntities) : base(szkolaEntities) { }
        #endregion
        #region FunkcjeBiznesowe
        //Funkcja zwraca listę nauczycieli
        public IQueryable<KeyAndValue> GetAktywniWychowawcy()
        {
            return
                (
                    from uzytkownik in SzkolaEntities.Uzytkownik
                    where uzytkownik.CzyAktywny == true && uzytkownik.IdStatusu == 2
                    select new KeyAndValue
                    {
                        Key = uzytkownik.IdUzytkownik,
                        Value = uzytkownik.Imie + " " + uzytkownik.Nazwisko
                    }
                ).ToList().AsQueryable();
        }
        //Funkcja która zwraca id wychowawcy z klasy która została wybrana
        //i ustawia tego wychowawce jako wybrany element z listy
        //jezeli klasa nie została wybrana to id jest ustawiane jako -1 czyli bez wychowawcy
        public int GetAktywnyWychowawcaZWybranejKlasy(int WybraneIdKlasy)
        {
            
                var wychowawca = 
                (
                    from klasa in SzkolaEntities.Klasa
                    select new
                    {
                        klasa.Uzytkownik.IdUzytkownik,
                        klasa.Uzytkownik.IdStatusu,
                        klasa.Uzytkownik.IdKlasy,
                        klasa.IdKlasa
                    }
                ).FirstOrDefault(p => p.IdStatusu == 2 && p.IdKlasa == WybraneIdKlasy);

            int id;

            if (wychowawca == null) id = -1;
            else id = wychowawca.IdUzytkownik;

            return id;
        }
        //Funkcja zwraca id sali lekcyjnej dla wybranej klasy
        public int GetAktywnaSalaLekcyjnaZWybranejKlasy(int WybraneIdKlasy)
        {

            var sala =
            (
                from klasa in SzkolaEntities.Klasa
                select new
                {
                    klasa.IdSaliLekcyjnej,
                    klasa.IdKlasa
                }
            ).FirstOrDefault(p => p.IdKlasa == WybraneIdKlasy);

            int id;

            if (sala == null) id = -1;
            else id = sala.IdSaliLekcyjnej;

            return id;
        }
        //Funkcja zwraca listę uczniów bez klasy
        public ObservableCollection<KlasyUczniowieForAllView> GetPozostaliUczniowieList()
        {
            return new ObservableCollection<KlasyUczniowieForAllView>(
                    from Uczen in SzkolaEntities.Uzytkownik
                    where Uczen.CzyAktywny == true && Uczen.IdStatusu == 1 && Uczen.IdKlasy == null
                    select new KlasyUczniowieForAllView
                    {
                        Imie = Uczen.Imie,
                        Nazwisko = Uczen.Nazwisko,
                        Pesel = Uczen.Pesel
                    }
                );
        }
        //Funkcja zwraca listę uczniów z wybranej klasy
        public ObservableCollection<KlasyUczniowieForAllView> GetUczniowieZKlasyList(int WybraneIdKlasy)
        {
            return new ObservableCollection<KlasyUczniowieForAllView>(
                    from Uczen in SzkolaEntities.Uzytkownik
                    where Uczen.CzyAktywny == true && Uczen.IdStatusu == 1 && Uczen.IdKlasy == WybraneIdKlasy
                    select new KlasyUczniowieForAllView
                    {
                        Imie = Uczen.Imie,
                        Nazwisko = Uczen.Nazwisko,
                        Pesel = Uczen.Pesel
                    }
                );
        }
        //Funkcja zwraca listę uczniów z wybranej klasy wraz z ich średnią
        public ObservableCollection<KlasyUczniowieWiecejForAllView> GetUczniowieZWybranejKlasyList(int WybraneIdKlasy)
        {
            return new ObservableCollection<KlasyUczniowieWiecejForAllView>(
                    from Uczen in SzkolaEntities.Uzytkownik
                    where Uczen.CzyAktywny == true && Uczen.IdStatusu == 1 && Uczen.IdKlasy == WybraneIdKlasy
                    join ocena in SzkolaEntities.Oceny on Uczen.IdUzytkownik equals ocena.IdUcznia into ocenyUcznia
                    let srednia = ocenyUcznia.Any() ? ocenyUcznia.Average(o => o.NazwyOcen.WartoscOceny) : 0
                    select new KlasyUczniowieWiecejForAllView
                    {
                        Imie = Uczen.Imie,
                        Nazwisko = Uczen.Nazwisko,
                        Pesel = Uczen.Pesel,
                        SredniaOcen = Math.Round((double)srednia, 2)
                    }
                );
        }
        #endregion

    }
}
