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
    //Klasa zawiera funkcje logiki dla Planu Lekcji
    public class PlanLekcjiLogic : DatabaseClass
    {
        #region Konstruktor
        public PlanLekcjiLogic(SzkolaEntities szkolaEntities) : base(szkolaEntities) { }
        #endregion
        //Funkcja zwraca plan lekcji
        public ObservableCollection<TydzienForAllView<LekcjaForAllView>> GetPlanLekcji(int WybraneIdKlasy)
        {
            //Pobranie wszystkich lekcji z danej klasy
            ObservableCollection<LekcjaDaneDB> LekcjeWKlasie = new ObservableCollection<LekcjaDaneDB>
                (
                    from lekcja in SzkolaEntities.Lekcja
                    where lekcja.CzyAktywny == true && lekcja.IdKlasy == WybraneIdKlasy
                    select new LekcjaDaneDB
                    {
                        IdDniaTygodnia = lekcja.IdDniaTygodnia,
                        IdGodziny = lekcja.IdGodziny,
                        IdPrzedmiotu = lekcja.IdPrzedmiotu
                    }
                );
            //Utworzenie planu lekcji
            ObservableCollection<TydzienForAllView<LekcjaForAllView>> Plan = new ObservableCollection<TydzienForAllView<LekcjaForAllView>>();
            //Pobranie nazw godzin
            var listaGodzin = SzkolaEntities.Godzina.Select(o => o.NazwaGodziny).ToList();
            int temp = 0;
            for (int i = 1; i <= 9; i++)
            {
                //Utworzenie elementu do dodania do planu lekcji
                TydzienForAllView<LekcjaForAllView> PlanTemp = new TydzienForAllView<LekcjaForAllView>();

                for (int j = 1; j < 6; j++)
                {
                    //Utworzenie lekcji w celu dodania do PlanTemp
                    LekcjaForAllView LekcjaTemp = new LekcjaForAllView();
                    LekcjaTemp.WybraneIdKlasy = WybraneIdKlasy;
                    //Przypisanie do lekcji odpowiednich id dla dnia i godziny
                    LekcjaTemp.WybraneIdDnia = j;
                    LekcjaTemp.WybraneIdGodziny = i;
                    //Sprawdzenie czy w danym dniu o danej godzinie jest ustawiona jakaś lekcja
                    var czyId = LekcjeWKlasie.Where(x => x.IdDniaTygodnia == j && x.IdGodziny == i);
                    if (czyId.Any())
                    {
                        //Przypisanie id przedmiotu z danego dnia i godziny do lekcji
                        LekcjaTemp.WybraneIdPrzedmiotu = LekcjeWKlasie.First(x => x.IdDniaTygodnia == j && x.IdGodziny == i).IdPrzedmiotu;
                    }
                    if (j-1 == 0)
                    {
                        //Dodanie nazwy godziny do zmiennej godzina w PlanTemp
                        PlanTemp.Godzina = listaGodzin[temp++];
                    }
                    //Przypisywanie lekcji do danych dni
                    if (j == 1)
                    {
                        PlanTemp.Pon = LekcjaTemp;
                    }
                    if (j == 2)
                    {
                        PlanTemp.Wt = LekcjaTemp;
                    }
                    if (j == 3)
                    {
                        PlanTemp.Sr = LekcjaTemp;
                    }
                    if (j == 4)
                    {
                        PlanTemp.Czw = LekcjaTemp;
                    }
                    if (j == 5)
                    {
                        PlanTemp.Pt = LekcjaTemp;
                    }
                }
                Plan.Add(PlanTemp);

            }
            return Plan;
        }
        //Funkcja służy do dodawania/edycji przedmiotu w planie dla danej klasy
        public void EdytujTabeleLekcja(int WybraneIdPrzedmiotu, int WybraneIdDnia, int WybraneIdKlasy, int WybraneIdGodziny)
        {
            Lekcja lekcja = new Lekcja();
            //Sprawdzanie czy edytujemy lekcje i przypisanie tej lekcji do zmiennej lekcja
            var lekcjaCheck = SzkolaEntities.Lekcja.Where(x => x.CzyAktywny==true && x.IdPrzedmiotu != 0 && x.IdKlasy == WybraneIdKlasy && x.IdDniaTygodnia == WybraneIdDnia && x.IdGodziny == WybraneIdGodziny);
            if (lekcjaCheck.Any())
            {
                int id = lekcjaCheck.FirstOrDefault().IdLekcja;
                lekcja = SzkolaEntities.Lekcja.First(x => x.IdLekcja == id);
                lekcja.IdPrzedmiotu = WybraneIdPrzedmiotu;
            }
            else
            {
                lekcja.CzyAktywny = true;
                lekcja.IdDniaTygodnia = WybraneIdDnia;
                lekcja.IdGodziny = WybraneIdGodziny;
                lekcja.IdKlasy = WybraneIdKlasy;
                lekcja.IdPrzedmiotu = WybraneIdPrzedmiotu;
                SzkolaEntities.Lekcja.AddObject(lekcja);
            }
            SzkolaEntities.SaveChanges();
        }
    }
}
