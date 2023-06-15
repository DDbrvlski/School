using Firma.Helper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Szkola.Model.BusinessLogic;
using Szkola.Model.EntitiesForView;
using Szkola.ViewModel.Abstract;

namespace Szkola.ViewModel
{
    public class WszystkiePlanyLekcjiViewModel : WszystkieViewModel<PlanZajecForAllView>
    {
        #region Konstruktor
        public WszystkiePlanyLekcjiViewModel() : base("Plany lekcji")
        {
        }
        #endregion
        #region Properties
        #region Pola
        private int _WybraneIdKlasy;
        public int WybraneIdKlasy
        {
            get
            {
                return _WybraneIdKlasy;
            }
            set
            {
                if (value != _WybraneIdKlasy)
                {
                    _WybraneIdKlasy = value;
                    base.OnPropertyChanged(() => WybraneIdKlasy);
                }


            }
        }
        #endregion
        #region Comboboxy
        public IQueryable<KeyAndValue> KlasyComboboxItems
        {
            get
            {
                return new PodstawoweComboboxyLogic(SzkolaEntities).GetAktywneKlasy();
            }
        }
        #endregion
        #endregion
        #region Komendy
        private BaseCommand _ShowPlanCommand;
        public ICommand ShowPlanCommand
        {
            get
            {
                if (_ShowPlanCommand == null) { }
                {
                    _ShowPlanCommand = new BaseCommand(() => ShowPlan());
                }
                return _ShowPlanCommand;
            }
        }
        #endregion
        #region Helpers
        public override void Load()
        {
            wypiszPlan();
        }
        public void ShowPlan()
        {
            wypiszPlan(WybraneIdKlasy);
        }
        private void wypiszPlan(int idKlasy = -1)
        {
            var PlanLekcji = from g in SzkolaEntities.Godzina
                             join l in SzkolaEntities.Lekcja on g.IdGodziny equals l.IdGodziny into g_l
                             from l in g_l.DefaultIfEmpty()
                             join d in SzkolaEntities.DniTygodnia on l.IdDniaTygodnia equals d.IdDniTygodnia into l_d
                             from d in l_d.DefaultIfEmpty()
                             join p in SzkolaEntities.Przedmiot on l.IdPrzedmiotu equals p.IdPrzedmiot into l_p
                             from p in l_p.DefaultIfEmpty()
                             orderby g.IdGodziny
                             select new
                             {
                                 Godzina = g.NazwaGodziny,
                                 DzienTygodnia = d.NazwaDniaTygodnia,
                                 Przedmiot = p.NazwaPrzedmiotu,
                                 Klasa = l.IdKlasy
                             };

            List = new ObservableCollection<PlanZajecForAllView>
                            (
                                PlanLekcji.GroupBy(l => l.Godzina)
                                .Select(g => new PlanZajecForAllView
                                {
                                    GodzinyZajec = g.Key,
                                    Pn = g.Where(l => l.DzienTygodnia == "Poniedziałek" && l.Klasa == idKlasy).Select(l => l.Przedmiot).DefaultIfEmpty("").FirstOrDefault(),
                                    Wt = g.Where(l => l.DzienTygodnia == "Wtorek" && l.Klasa == idKlasy).Select(l => l.Przedmiot).DefaultIfEmpty("").FirstOrDefault(),
                                    Sr = g.Where(l => l.DzienTygodnia == "Środa" && l.Klasa == idKlasy).Select(l => l.Przedmiot).DefaultIfEmpty("").FirstOrDefault(),
                                    Czw = g.Where(l => l.DzienTygodnia == "Czwartek" && l.Klasa == idKlasy).Select(l => l.Przedmiot).DefaultIfEmpty("").FirstOrDefault(),
                                    Pt = g.Where(l => l.DzienTygodnia == "Piątek" && l.Klasa == idKlasy).Select(l => l.Przedmiot).DefaultIfEmpty("").FirstOrDefault()
                                })
            );
        }
        public override void Delete()
        {
            throw new NotImplementedException();
        }
        #endregion
        #region Find & Sort
        public override void Sort()
        {
            throw new NotImplementedException();
        }

        public override List<string> GetComboboxSortList()
        {
            throw new NotImplementedException();
        }

        public override void Find()
        {
            throw new NotImplementedException();
        }

        public override List<string> GetComboboxFindList()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
