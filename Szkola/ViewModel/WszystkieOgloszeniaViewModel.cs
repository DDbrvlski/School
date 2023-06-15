using Firma.Helper;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Szkola.Helpers.Messenger;
using Szkola.Model.BusinessLogic;
using Szkola.Model.EntitiesForView;
using Szkola.ViewModel.Abstract;

namespace Szkola.ViewModel
{
    public class WszystkieOgloszeniaViewModel : WszystkieViewModel<WszystkieOgloszeniaForAllView>
    {
        #region Konstruktor
        public WszystkieOgloszeniaViewModel() : base("Ogloszenia")
        {
            Messenger.Default.Register<Message<UzytkownicyForAllView>>(this, getWybranyStudent);
        }
        #endregion
        #region Properties
        #region Pola
        private bool _CzyWazne;
        public bool CzyWazne
        {
            get
            {
                return _CzyWazne;
            }
            set
            {
                if (value != _CzyWazne)
                {
                    _CzyWazne = value;
                    base.OnPropertyChanged(() => CzyWazne);
                }
            }
        }
        private int _WybraneIdUzytkownika;
        public int WybraneIdUzytkownika
        {
            get
            {
                return _WybraneIdUzytkownika;
            }
            set
            {
                if (value != _WybraneIdUzytkownika)
                {
                    _WybraneIdUzytkownika = value;
                    base.OnPropertyChanged(() => WybraneIdUzytkownika);
                }
            }
        }
        private DateTime? _DataOd = null;
        public DateTime? DataOd
        {
            get
            {
                return _DataOd;
            }
            set
            {
                if (value != _DataOd)
                {
                    _DataOd = value;
                    base.OnPropertyChanged(() => DataOd);
                }
            }
        }
        private DateTime _DataDo = DateTime.Now;
        public DateTime DataDo
        {
            get
            {
                return _DataDo;
            }
            set
            {
                if (value != _DataDo)
                {
                    _DataDo = value;
                    base.OnPropertyChanged(() => DataDo);
                }
            }
        }
        #endregion
        #region Comboboxy
        public IQueryable<KeyAndValue> UzytkownicyComboboxItems
        {
            get
            {
                return new PodstawoweComboboxyLogic(SzkolaEntities).GetAktywniUzytkownicy();
            }
        }
        #endregion
        #endregion
        #region Komendy
        private BaseCommand _ShowAllUsersCommand;
        public ICommand ShowAllUsersCommand
        {
            get
            {
                if (_ShowAllUsersCommand == null) { }
                {
                    _ShowAllUsersCommand = new BaseCommand(() => showUsers());
                }
                return _ShowAllUsersCommand;
            }
        }
        private BaseCommand _ShowOgloszeniaCommand;
        public ICommand ShowOgloszeniaCommand
        {
            get
            {
                if (_ShowOgloszeniaCommand == null) { }
                {
                    _ShowOgloszeniaCommand = new BaseCommand(() => ShowOgloszenia());
                }
                return _ShowOgloszeniaCommand;
            }
        }
        #endregion
        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<WszystkieOgloszeniaForAllView>
                (
                    from ogloszenie in SzkolaEntities.Ogloszenia
                    where ogloszenie.CzyAktywny == true
                    orderby ogloszenie.DataWyslania descending
                    select new WszystkieOgloszeniaForAllView
                    {
                        Tytul = ogloszenie.TytulOgloszenia,
                        Tresc = ogloszenie.TrescOgloszenia,
                        CzyWazne = ogloszenie.CzyWazne,
                        Data = ogloszenie.DataWyslania
                    }
                );
        }
        public void ShowOgloszenia()
        {
            if (WybraneIdUzytkownika > 0)
            {
                if (CzyWazne && DataOd != null)
                {
                        List = new ObservableCollection<WszystkieOgloszeniaForAllView>
                    (
                        from ogloszenie in SzkolaEntities.OdbiorcyOgloszenia
                        where ogloszenie.CzyAktywny == true && ogloszenie.IdUzytkownika == WybraneIdUzytkownika && ogloszenie.Ogloszenia.CzyWazne == true && ogloszenie.Ogloszenia.DataWyslania >= DataOd && ogloszenie.Ogloszenia.DataWyslania <= DataDo
                        group ogloszenie by ogloszenie.Ogloszenia into ogloszenieGroup
                        orderby ogloszenieGroup.Key.DataWyslania descending
                        select new WszystkieOgloszeniaForAllView
                        {
                            Tytul = ogloszenieGroup.Key.TytulOgloszenia,
                            Tresc = ogloszenieGroup.Key.TrescOgloszenia,
                            CzyWazne = ogloszenieGroup.Key.CzyWazne,
                            Data = ogloszenieGroup.Key.DataWyslania
                        }
                    );
                }
                else if (!CzyWazne && DataOd != null)
                {
                    List = new ObservableCollection<WszystkieOgloszeniaForAllView>
                    (
                        from ogloszenie in SzkolaEntities.OdbiorcyOgloszenia
                        where ogloszenie.CzyAktywny == true && ogloszenie.IdUzytkownika == WybraneIdUzytkownika && ogloszenie.Ogloszenia.DataWyslania >= DataOd && ogloszenie.Ogloszenia.DataWyslania <= DataDo
                        group ogloszenie by ogloszenie.Ogloszenia into ogloszenieGroup
                        orderby ogloszenieGroup.Key.DataWyslania descending
                        select new WszystkieOgloszeniaForAllView
                        {
                            Tytul = ogloszenieGroup.Key.TytulOgloszenia,
                            Tresc = ogloszenieGroup.Key.TrescOgloszenia,
                            CzyWazne = ogloszenieGroup.Key.CzyWazne,
                            Data = ogloszenieGroup.Key.DataWyslania
                        }
                    );
                }
                else if (CzyWazne && DataOd == null)
                {
                    List = new ObservableCollection<WszystkieOgloszeniaForAllView>
                    (
                        from ogloszenie in SzkolaEntities.OdbiorcyOgloszenia
                        where ogloszenie.CzyAktywny == true && ogloszenie.IdUzytkownika == WybraneIdUzytkownika && ogloszenie.Ogloszenia.CzyWazne == true
                        group ogloszenie by ogloszenie.Ogloszenia into ogloszenieGroup
                        orderby ogloszenieGroup.Key.DataWyslania descending
                        select new WszystkieOgloszeniaForAllView
                        {
                            Tytul = ogloszenieGroup.Key.TytulOgloszenia,
                            Tresc = ogloszenieGroup.Key.TrescOgloszenia,
                            CzyWazne = ogloszenieGroup.Key.CzyWazne,
                            Data = ogloszenieGroup.Key.DataWyslania
                        }
                    );
                }
                else
                {
                    List = new ObservableCollection<WszystkieOgloszeniaForAllView>
                    (
                        from ogloszenie in SzkolaEntities.OdbiorcyOgloszenia
                        where ogloszenie.CzyAktywny == true && ogloszenie.IdUzytkownika == WybraneIdUzytkownika
                        group ogloszenie by ogloszenie.Ogloszenia into ogloszenieGroup
                        orderby ogloszenieGroup.Key.DataWyslania descending
                        select new WszystkieOgloszeniaForAllView
                        {
                            Tytul = ogloszenieGroup.Key.TytulOgloszenia,
                            Tresc = ogloszenieGroup.Key.TrescOgloszenia,
                            CzyWazne = ogloszenieGroup.Key.CzyWazne,
                            Data = ogloszenieGroup.Key.DataWyslania
                        }
                    );
                }
                
            }
            else
            {
                if (CzyWazne && DataOd != null)
                {
                    List = new ObservableCollection<WszystkieOgloszeniaForAllView>
                (
                    from ogloszenie in SzkolaEntities.OdbiorcyOgloszenia
                    where ogloszenie.CzyAktywny == true && ogloszenie.Ogloszenia.CzyWazne == true && ogloszenie.Ogloszenia.DataWyslania >= DataOd && ogloszenie.Ogloszenia.DataWyslania <= DataDo
                    group ogloszenie by ogloszenie.Ogloszenia into ogloszenieGroup
                    orderby ogloszenieGroup.Key.DataWyslania descending
                    select new WszystkieOgloszeniaForAllView
                    {
                        Tytul = ogloszenieGroup.Key.TytulOgloszenia,
                        Tresc = ogloszenieGroup.Key.TrescOgloszenia,
                        CzyWazne = ogloszenieGroup.Key.CzyWazne,
                        Data = ogloszenieGroup.Key.DataWyslania
                    }
                );
                }
                else if (!CzyWazne && DataOd != null)
                {
                    List = new ObservableCollection<WszystkieOgloszeniaForAllView>
                    (
                        from ogloszenie in SzkolaEntities.OdbiorcyOgloszenia
                        where ogloszenie.CzyAktywny == true && ogloszenie.Ogloszenia.DataWyslania >= DataOd && ogloszenie.Ogloszenia.DataWyslania <= DataDo
                        group ogloszenie by ogloszenie.Ogloszenia into ogloszenieGroup
                        orderby ogloszenieGroup.Key.DataWyslania descending
                        select new WszystkieOgloszeniaForAllView
                        {
                            Tytul = ogloszenieGroup.Key.TytulOgloszenia,
                            Tresc = ogloszenieGroup.Key.TrescOgloszenia,
                            CzyWazne = ogloszenieGroup.Key.CzyWazne,
                            Data = ogloszenieGroup.Key.DataWyslania
                        }
                    );
                }
                else if (CzyWazne && DataOd == null)
                {
                    List = new ObservableCollection<WszystkieOgloszeniaForAllView>
                    (
                        from ogloszenie in SzkolaEntities.OdbiorcyOgloszenia
                        where ogloszenie.CzyAktywny == true && ogloszenie.Ogloszenia.CzyWazne == true
                        group ogloszenie by ogloszenie.Ogloszenia into ogloszenieGroup
                        orderby ogloszenieGroup.Key.DataWyslania descending
                        select new WszystkieOgloszeniaForAllView
                        {
                            Tytul = ogloszenieGroup.Key.TytulOgloszenia,
                            Tresc = ogloszenieGroup.Key.TrescOgloszenia,
                            CzyWazne = ogloszenieGroup.Key.CzyWazne,
                            Data = ogloszenieGroup.Key.DataWyslania
                        }
                    );
                }
                else
                {
                    List = new ObservableCollection<WszystkieOgloszeniaForAllView>
                    (
                        from ogloszenie in SzkolaEntities.OdbiorcyOgloszenia
                        where ogloszenie.CzyAktywny == true
                        group ogloszenie by ogloszenie.Ogloszenia into ogloszenieGroup
                        orderby ogloszenieGroup.Key.DataWyslania descending
                        select new WszystkieOgloszeniaForAllView
                        {
                            Tytul = ogloszenieGroup.Key.TytulOgloszenia,
                            Tresc = ogloszenieGroup.Key.TrescOgloszenia,
                            CzyWazne = ogloszenieGroup.Key.CzyWazne,
                            Data = ogloszenieGroup.Key.DataWyslania
                        }
                    );
                }
            }
        }
        public override void Delete()
        {
            throw new NotImplementedException();
        }
        private void showUsers()
        {
            Messenger.Default.Send("UsersAll");
        }
        private void getWybranyStudent(Message<UzytkownicyForAllView> wiadomosc)
        {
            if (wiadomosc.messageInfo == "Uzytkownicy") WybraneIdUzytkownika = wiadomosc.element.IdUzytkownika;
        }
        #endregion
        #region Find & Sort
        public override void Sort()
        {
            if (SortField == "Data")
            {
                List = new ObservableCollection<WszystkieOgloszeniaForAllView>(List.OrderByDescending(item => item.Data));
            }
        }
        public override List<string> GetComboboxSortList()
        {
            return new List<string>
            {
                "Data"
            };
        }
        public override void Find()
        {
            if (FindField == "Tytul")
            {
                List = new ObservableCollection<WszystkieOgloszeniaForAllView>(List.Where(item => item.Tytul != null && item.Tytul.StartsWith(FindTextBox)));
            }
            if (FindField == "Tresc")
            {
                List = new ObservableCollection<WszystkieOgloszeniaForAllView>(List.Where(item => item.Tresc != null && item.Tresc.Contains(FindTextBox)));
            }
        }
        public override List<string> GetComboboxFindList()
        {
            return new List<string>
            {
                "Tytul",
                "Tresc"
            };
        }
        #endregion
    }
}
