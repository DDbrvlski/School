using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Szkola.Model.Entities;
using Szkola.Model.EntitiesForView;

namespace Szkola.Model.BusinessLogic
{
    //Klasa która zawiera logikę dla obecnosci (DziennikObecnosci i DodajObecnosc)
    public class DziennikObecnosciLogic : DatabaseClass
    {
        #region Konstruktor        
        public DziennikObecnosciLogic(SzkolaEntities szkolaEntities) : base(szkolaEntities)
        {
        }
        #endregion

        //Funkcja zwraca listę uczniów wraz z liczbą ich nieobecności w wybranym okresie czasu
        public ObservableCollection<DziennikObecnosciForAllView> GetNieobecnosciWKlasie(DateTime dataOd, DateTime dataDo, int WybraneIdKlasy = 0, int WybraneIdPrzedmiotu = 0)
        {
            //Sprawdza czy została wybrana tylko klasa z comboboxa
            if (WybraneIdKlasy != 0 && WybraneIdPrzedmiotu == 0)
            {
                return new ObservableCollection<DziennikObecnosciForAllView>(
                    from obecnosc in SzkolaEntities.Nieobecnosci
                    where obecnosc.CzyAktywny == true && obecnosc.Uzytkownik.IdKlasy == WybraneIdKlasy && obecnosc.DataNieobecnosci >= dataOd && obecnosc.DataNieobecnosci <= dataDo
                    group obecnosc by obecnosc.Uzytkownik into uzytkownik
                    select new DziennikObecnosciForAllView
                    {
                        IdUcznia = uzytkownik.Key.IdUzytkownik,
                        Imie = uzytkownik.Key.Imie,
                        Nazwisko = uzytkownik.Key.Nazwisko,
                        Pesel = uzytkownik.Key.Pesel,
                        Klasa = uzytkownik.Key.Klasa1.NazwaKlasy,
                        LiczbaNieobecnosci = uzytkownik.Count()
                    }
                );
            }
            //Zwraca listę dla wybranej klasy oraz przedmiotu
            else
            {
                return new ObservableCollection<DziennikObecnosciForAllView>(
                    from obecnosc in SzkolaEntities.Nieobecnosci
                    where obecnosc.CzyAktywny == true && obecnosc.Uzytkownik.IdKlasy == WybraneIdKlasy && obecnosc.Lekcja.IdPrzedmiotu == WybraneIdPrzedmiotu && obecnosc.DataNieobecnosci >= dataOd && obecnosc.DataNieobecnosci <= dataDo
                    group obecnosc by obecnosc.Uzytkownik into uzytkownik
                    select new DziennikObecnosciForAllView
                    {
                        IdUcznia = uzytkownik.Key.IdUzytkownik,
                        Imie = uzytkownik.Key.Imie,
                        Nazwisko = uzytkownik.Key.Nazwisko,
                        Pesel = uzytkownik.Key.Pesel,
                        Klasa = uzytkownik.Key.Klasa1.NazwaKlasy,
                        LiczbaNieobecnosci = uzytkownik.Count()
                    }
                );
            }
        }
        //Funkcja zwraca listę nieobecności wybranego ucznia
        public ObservableCollection<ObecnosciUczniaForAllView> GetNieobecnosciUcznia(DateTime dataOd, DateTime dataDo, int WybraneIdUcznia, int WybraneIdPrzedmiotu)
        {
            //Jeżeli wybrano ucznia bez przedmiotu
            if (WybraneIdUcznia != 0 && WybraneIdPrzedmiotu == 0)
            {
                return new ObservableCollection<ObecnosciUczniaForAllView>(
                    from obecnosc in SzkolaEntities.Nieobecnosci
                    where obecnosc.CzyAktywny == true && obecnosc.IdUzytkownika == WybraneIdUcznia && obecnosc.DataNieobecnosci >= dataOd && obecnosc.DataNieobecnosci <= dataDo
                    select new ObecnosciUczniaForAllView
                    {
                        DataTemp = obecnosc.DataNieobecnosci,
                        Lekcja = obecnosc.Lekcja.Przedmiot.NazwaPrzedmiotu,
                        GodzinaLekcyjna = obecnosc.Lekcja.Godzina.NazwaGodziny
                    }
                );
            }
            //Jeżeli wybrano ucznia i przedmiot
            else
            {
                return new ObservableCollection<ObecnosciUczniaForAllView>(
                    from obecnosc in SzkolaEntities.Nieobecnosci
                    where obecnosc.CzyAktywny == true && obecnosc.IdUzytkownika == WybraneIdUcznia && obecnosc.Lekcja.IdPrzedmiotu == WybraneIdPrzedmiotu && obecnosc.DataNieobecnosci >= dataOd && obecnosc.DataNieobecnosci <= dataDo
                    select new ObecnosciUczniaForAllView
                    {
                        DataTemp = obecnosc.DataNieobecnosci,
                        Lekcja = obecnosc.Lekcja.Przedmiot.NazwaPrzedmiotu,
                        GodzinaLekcyjna = obecnosc.Lekcja.Godzina.NazwaGodziny
                    }
                );
            }
        }
        //Funkcja zwraca liste obecności uczniów z danej klasy
        public ObservableCollection<ListaObecnosciForAllView> GetListaObecnosci(int WybraneIdKlasy)
        {
            return new ObservableCollection<ListaObecnosciForAllView>(
                    from uzytkownik in SzkolaEntities.Uzytkownik
                    where uzytkownik.CzyAktywny == true && uzytkownik.IdKlasy == WybraneIdKlasy
                    select new ListaObecnosciForAllView
                    {
                        IdUcznia = uzytkownik.IdUzytkownik,
                        Imie = uzytkownik.Imie,
                        Nazwisko = uzytkownik.Nazwisko,
                        Pesel = uzytkownik.Pesel
                    }
                );
        }
        //Funkcja zwraca listę przedmiotów dla danej klasy oraz występujące w danym dniu w planie lekcji
        public ObservableCollection<KeyAndValue> GetPrzedmiotyDlaDnia(DateTime data, int WybraneIdKlasy)
        {
            int dzien = (int)data.DayOfWeek;
            return new ObservableCollection<KeyAndValue>(
                    from lekcja in SzkolaEntities.Lekcja
                    where lekcja.CzyAktywny == true && lekcja.IdDniaTygodnia == dzien && lekcja.IdKlasy == WybraneIdKlasy
                    select new KeyAndValue
                    {
                        Key = lekcja.IdPrzedmiotu,
                        Value = lekcja.Przedmiot.NazwaPrzedmiotu
                    }
                );
        }
        //Funkcja zwraca listę godzin lekcyjnych dla wybranego przedmiotu z wybranego dnia
        public ObservableCollection<KeyAndValue> GetGodzinyLekcyjneDlaLekcji(DateTime data, int WybraneIdKlasy, int WybraneIdPrzedmiotu)
        {
            int dzien = (int)data.DayOfWeek;
            return new ObservableCollection<KeyAndValue>(
                    from lekcja in SzkolaEntities.Lekcja
                    where lekcja.CzyAktywny == true && lekcja.IdDniaTygodnia == dzien && lekcja.IdKlasy == WybraneIdKlasy && lekcja.IdPrzedmiotu == WybraneIdPrzedmiotu
                    select new KeyAndValue
                    {
                        Key = lekcja.IdGodziny,
                        Value = lekcja.Godzina.NazwaGodziny
                    }
                );
        }
        //Funkcja zwraca id lekcji na podstawie podanych argumentów
        public int GetIdLekcji(DateTime data, int WybraneIdKlasy, int WybraneIdPrzedmiotu, int WybraneIdGodziny)
        {
            int dzien = (int)data.DayOfWeek;
            return SzkolaEntities.Lekcja.
                Where(x => x.IdDniaTygodnia == dzien && x.IdKlasy == WybraneIdKlasy && 
                x.IdPrzedmiotu == WybraneIdPrzedmiotu && x.IdGodziny == WybraneIdGodziny)
                .First().IdLekcja;
        }
    }
}
