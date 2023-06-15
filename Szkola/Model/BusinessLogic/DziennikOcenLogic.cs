using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Objects.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Szkola.Model.Entities;
using Szkola.Model.EntitiesForView;

namespace Szkola.Model.BusinessLogic
{
    //Klasa zawiera funkcje logiki dla ocen (DziennikOcen i DodajOcene)
    public class DziennikOcenLogic : DatabaseClass
    {
        #region Konstruktor
        public DziennikOcenLogic(SzkolaEntities szkolaEntities) : base(szkolaEntities) { }
        #endregion
        //Funkcja zwraca listę uczniów wraz ze średnią
        public ObservableCollection<DziennikUczniowieForAllView> GetUczniowieISredniaZKlasyList(int WybraneIdKlasy, int WybraneIdPrzedmiotu)
        {
            //Jeżeli wybrano tylko klasę
            if(WybraneIdKlasy != 0 && WybraneIdPrzedmiotu == 0)
            {
                return new ObservableCollection<DziennikUczniowieForAllView>(
                    from Uczen in SzkolaEntities.Uzytkownik
                    where Uczen.CzyAktywny == true && Uczen.IdStatusu == 1 && Uczen.IdKlasy == WybraneIdKlasy
                    join ocena in SzkolaEntities.Oceny on Uczen.IdUzytkownik equals ocena.IdUcznia into ocenyUcznia
                    let srednia = ocenyUcznia.Any() ? ocenyUcznia.Average(o => o.NazwyOcen.WartoscOceny) : 0
                    select new DziennikUczniowieForAllView
                    {
                        IdUcznia = Uczen.IdUzytkownik,
                        Imie = Uczen.Imie,
                        Nazwisko = Uczen.Nazwisko,
                        Klasa = Uczen.Klasa1.NazwaKlasy,
                        Srednia = Math.Round((double)srednia, 2)
                    }
                );
            }
            //jeżeli wybrano klasę i przedmiot
            else if (WybraneIdKlasy != 0 && WybraneIdPrzedmiotu != 0)
            {
                return new ObservableCollection<DziennikUczniowieForAllView>(
                    from Uczen in SzkolaEntities.Uzytkownik
                    where Uczen.CzyAktywny == true && Uczen.IdStatusu == 1 && Uczen.IdKlasy == WybraneIdKlasy
                    join ocena in SzkolaEntities.Oceny on Uczen.IdUzytkownik equals ocena.IdUcznia into ocenyUcznia
                    let srednia = ocenyUcznia.Any() ? ocenyUcznia.Where(o => o.IdPrzedmiotu == WybraneIdPrzedmiotu).Average(o => o.NazwyOcen.WartoscOceny) : 0
                    select new DziennikUczniowieForAllView
                    {
                        IdUcznia = Uczen.IdUzytkownik,
                        Imie = Uczen.Imie,
                        Nazwisko = Uczen.Nazwisko,
                        Klasa = Uczen.Klasa1.NazwaKlasy,
                        Srednia = Math.Round((double)(srednia ?? 0), 2)
                    }
                );
            }
            //Jeżeli nie wybrano nic
            else
            {
                return new ObservableCollection<DziennikUczniowieForAllView>(
                    from Uczen in SzkolaEntities.Uzytkownik
                    where Uczen.CzyAktywny == true && Uczen.IdStatusu == 1
                    join ocena in SzkolaEntities.Oceny on Uczen.IdUzytkownik equals ocena.IdUcznia into ocenyUcznia
                    let srednia = ocenyUcznia.Any() ? ocenyUcznia.Average(o => o.NazwyOcen.WartoscOceny) : 0
                    select new DziennikUczniowieForAllView
                    {
                        IdUcznia = Uczen.IdUzytkownik,
                        Imie = Uczen.Imie,
                        Nazwisko = Uczen.Nazwisko,
                        Klasa = Uczen.Klasa1.NazwaKlasy,
                        Srednia = Math.Round((double)srednia, 2)
                    }
                );
            }

            
        }
        //Funkcja zwraca listę uczniów z klasy
        public ObservableCollection<KlasyUczniowieForAllView> GetUczniowieZKlasyList(int WybraneIdKlasy)
        {
            return new ObservableCollection<KlasyUczniowieForAllView>(
                    from Uczen in SzkolaEntities.Uzytkownik
                    where Uczen.CzyAktywny == true && Uczen.IdStatusu == 1 && Uczen.IdKlasy == WybraneIdKlasy
                    select new KlasyUczniowieForAllView
                    {
                        Id = Uczen.IdUzytkownik,
                        Imie = Uczen.Imie,
                        Nazwisko = Uczen.Nazwisko,
                        Pesel = Uczen.Pesel
                    }
                );
        }
        //Funkcja zwraca listę ocen ucznia
        public ObservableCollection<DziennikUczenOcenyForAllView> GetAktywneOcenyUcznia(int WybraneIdUcznia, int WybraneIdPrzedmiotu)
        {
            return new ObservableCollection<DziennikUczenOcenyForAllView>(
                    from oceny in SzkolaEntities.Oceny
                    where oceny.CzyAktywny == true && oceny.IdUcznia == WybraneIdUcznia && oceny.IdPrzedmiotu == WybraneIdPrzedmiotu
                    select new DziennikUczenOcenyForAllView
                    {
                        FormaSprawdzeniaWiedzy = oceny.FormySprawdzaniaWiedzy.NazwaFormySprawdzaniaWiedzy,
                        Ocena = oceny.NazwyOcen.NazwaOceny + " (" + SqlFunctions.StringConvert((double)oceny.NazwyOcen.WartoscOceny).Trim() + ")"
                    }
                );
        }
    }
}

