using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Szkola.Model.Entities;
using Szkola.Model.EntitiesForView;

namespace Szkola.Model.BusinessLogic
{
    //Klasa służy do zwracania podstawowych list dla comboboxów
    public class PodstawoweComboboxyLogic : DatabaseClass
    {
        #region Konstruktor
        public PodstawoweComboboxyLogic(SzkolaEntities szkolaEntities) : base(szkolaEntities) {}
        #endregion
        #region FunkcjeBiznesowe
        public IQueryable<KeyAndValue> GetAktywniUzytkownicy()
        {
            return
                (
                    from uzytkownik in SzkolaEntities.Uzytkownik
                    where uzytkownik.CzyAktywny == true
                    select new KeyAndValue
                    {
                        Key = uzytkownik.IdUzytkownik,
                        Value = uzytkownik.Imie + " " + uzytkownik.Nazwisko
                    }
                ).ToList().AsQueryable();
        }
        public IQueryable<KeyAndValue> GetAktywneStatusy()
        {
            return
                (
                    from status in SzkolaEntities.Status
                    where status.CzyAktywny == true
                    select new KeyAndValue
                    {
                        Key = status.IdStatus,
                        Value = status.NazwaStatusuKonta
                    }
                ).ToList().AsQueryable();
        }
        public IQueryable<KeyAndValue> GetAktywneGodzinyLekcyjne()
        {
            return
                (
                    from godzina in SzkolaEntities.Godzina
                    where godzina.CzyAktywny == true
                    select new KeyAndValue
                    {
                        Key = godzina.IdGodziny,
                        Value = godzina.NazwaGodziny
                    }
                ).ToList().AsQueryable();
        }
        public IQueryable<KeyAndValue> GetAktywneKraje()
        {
            return
                (
                    from kraj in SzkolaEntities.Kraje
                    where kraj.CzyAktywny == true
                    select new KeyAndValue
                    {
                        Key = kraj.IdKraju,
                        Value = kraj.NazwaKraju
                    }
                ).ToList().AsQueryable();
        }
        public IQueryable<KeyAndValue> GetAktywnePlcie()
        {
            return
                (
                    from plec in SzkolaEntities.Plec
                    where plec.CzyAktywny == true
                    select new KeyAndValue
                    {
                        Key = plec.IdPlec,
                        Value = plec.NazwaPlci
                    }
                ).ToList().AsQueryable();
        }
        public IQueryable<KeyAndValue> GetAktywneKlasy()
        {
            return
                (
                    from klasa in SzkolaEntities.Klasa
                    where klasa.CzyAktywny == true
                    select new KeyAndValue
                    {
                        Key = klasa.IdKlasa,
                        Value = klasa.NazwaKlasy
                    }
                ).ToList().AsQueryable();
        }
        public IQueryable<KeyAndValue> GetAktywneSaleLekcyjne()
        {
            return
                (
                    from sala in SzkolaEntities.SalaLekcyjna
                    where sala.CzyAktywny == true
                    select new KeyAndValue
                    {
                        Key = sala.IdSalaLekcyjna,
                        Value = sala.NumerSaliLekcyjnej
                    }
                ).ToList().AsQueryable();
        }
        public IQueryable<KeyAndValue> GetAktywneFormySprawdzaniaWiedzy()
        {
            return
                (
                    from forma in SzkolaEntities.FormySprawdzaniaWiedzy
                    where forma.CzyAktywny == true
                    select new KeyAndValue
                    {
                        Key = forma.IdFormySprawdzaniaWiedzy,
                        Value = forma.NazwaFormySprawdzaniaWiedzy
                    }
                ).ToList().AsQueryable();
        }
        public IQueryable<KeyAndValue> GetAktywneNazwyOcen()
        {
            return
                (
                    from nazwa in SzkolaEntities.NazwyOcen
                    where nazwa.CzyAktywny == true
                    select new KeyAndValue
                    {
                        Key = nazwa.IdNazwaOceny,
                        Value = nazwa.NazwaOceny
                    }
                ).ToList().AsQueryable();
        }
        public IQueryable<KeyAndValue> GetAktywnePrzedmioty()
        {
            return
                (
                    from przedmiot in SzkolaEntities.Przedmiot
                    where przedmiot.CzyAktywny == true
                    select new KeyAndValue
                    {
                        Key = przedmiot.IdPrzedmiot,
                        Value = przedmiot.NazwaPrzedmiotu
                    }
                ).ToList().AsQueryable();
        }
        public IQueryable<KeyAndValue> GetAktywniUczniowie()
        {
            return
                (
                    from uzytkownik in SzkolaEntities.Uzytkownik
                    where uzytkownik.CzyAktywny == true && uzytkownik.IdStatusu == 1
                    select new KeyAndValue
                    {
                        Key = uzytkownik.IdUzytkownik,
                        Value = uzytkownik.Imie + " " + uzytkownik.Nazwisko
                    }
                ).ToList().AsQueryable();
        }
        public IQueryable<KeyAndValue> GetAktywneOceny()
        {
            return
                (
                    from ocena in SzkolaEntities.NazwyOcen
                    where ocena.CzyAktywny == true
                    select new KeyAndValue
                    {
                        Key = ocena.IdNazwaOceny,
                        Value = ocena.NazwaOceny
                    }
                ).ToList().AsQueryable();
        }
        #endregion
    }
}
