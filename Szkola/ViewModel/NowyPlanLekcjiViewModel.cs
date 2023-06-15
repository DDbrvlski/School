using Firma.Helper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using Szkola.Model.BusinessLogic;
using Szkola.Model.Entities;
using Szkola.Model.EntitiesForView;
using Szkola.ViewModel.Abstract;

namespace Szkola.ViewModel
{
    public class NowyPlanLekcjiViewModel : DodajViewModel<Lekcja>
    {
        #region Konstruktor
        public NowyPlanLekcjiViewModel() : base("Plan lekcji")
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
        #region Listy
        private ObservableCollection<TydzienForAllView<LekcjaForAllView>> _Plan;
        public ObservableCollection<TydzienForAllView<LekcjaForAllView>> Plan
        {
            get { return _Plan; }
            set
            {
                if (value != _Plan)
                {
                    _Plan = value;
                    base.OnPropertyChanged(() => Plan);
                }

            }
        }
        #endregion
        #region Comboboxy
        public IQueryable<KeyAndValue> KlasyComboboxItems
        {
            get
            {
                return new PodstawoweComboboxyLogic(Db).GetAktywneKlasy();
            }
        }
        public IQueryable<KeyAndValue> PrzedmiotyComboboxItems
        {
            get
            {
                return new PodstawoweComboboxyLogic(Db).GetAktywnePrzedmioty();
            }
        }
        #endregion
        #endregion
        #region Komendy
        private BaseCommand _ShowAllPlanLekcjiCommand;
        public ICommand ShowAllPlanLekcjiCommand
        {
            get
            {
                if (_ShowAllPlanLekcjiCommand == null) { }
                {
                    _ShowAllPlanLekcjiCommand = new BaseCommand(() => Load());
                }
                return _ShowAllPlanLekcjiCommand;
            }
        }
        #endregion
        #region Helpers
        public override void Save()
        {
            foreach (var item in Plan)
            {
                if (item.Pon.WybraneIdPrzedmiotu > 0)
                {
                    new PlanLekcjiLogic(Db).EdytujTabeleLekcja(item.Pon.WybraneIdPrzedmiotu, item.Pon.WybraneIdDnia, WybraneIdKlasy, item.Pon.WybraneIdGodziny);
                }
                if (item.Wt.WybraneIdPrzedmiotu > 0)
                {
                    new PlanLekcjiLogic(Db).EdytujTabeleLekcja(item.Wt.WybraneIdPrzedmiotu, item.Wt.WybraneIdDnia, WybraneIdKlasy, item.Wt.WybraneIdGodziny);
                }
                if (item.Sr.WybraneIdPrzedmiotu > 0)
                {
                    new PlanLekcjiLogic(Db).EdytujTabeleLekcja(item.Sr.WybraneIdPrzedmiotu, item.Sr.WybraneIdDnia, WybraneIdKlasy, item.Sr.WybraneIdGodziny);
                }
                if (item.Czw.WybraneIdPrzedmiotu > 0)
                {
                    new PlanLekcjiLogic(Db).EdytujTabeleLekcja(item.Czw.WybraneIdPrzedmiotu, item.Czw.WybraneIdDnia, WybraneIdKlasy, item.Czw.WybraneIdGodziny);
                }
                if (item.Pt.WybraneIdPrzedmiotu > 0)
                {
                    new PlanLekcjiLogic(Db).EdytujTabeleLekcja(item.Pt.WybraneIdPrzedmiotu, item.Pt.WybraneIdDnia, WybraneIdKlasy, item.Pt.WybraneIdGodziny);
                }

            }
        }
        public void Load()
        {
            Plan = new PlanLekcjiLogic(Db).GetPlanLekcji(WybraneIdKlasy);
        }
        #endregion

    }
}
