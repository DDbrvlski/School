using Firma.Helper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Szkola.Model.BusinessLogic;
using Szkola.Model.Entities;
using Szkola.Model.EntitiesForView;

namespace Szkola.ViewModel
{
    public class RaportNieobecnosciViewModel : WorkspaceViewModel
    {
        #region Konstruktor
        public RaportNieobecnosciViewModel()
        {
            DisplayName = "Raport nieobecności";
            szkolaEntities = new SzkolaEntities();
            IsSelectedKlasa = true;
        }
        #endregion
        #region Properties
        #region Pola
        private SzkolaEntities szkolaEntities { get; set; }
        private bool _IsSelectedKlasa;
        public bool IsSelectedKlasa
        {
            get
            {
                return _IsSelectedKlasa;
            }
            set
            {
                if (value != _IsSelectedKlasa)
                {
                    _IsSelectedKlasa = value;
                    CheckBox();
                    base.OnPropertyChanged(() => IsSelectedKlasa);
                }
            }
        }
        private bool _IsSelectedRok;
        public bool IsSelectedRok
        {
            get
            {
                return _IsSelectedRok;
            }
            set
            {
                if (value != _IsSelectedRok)
                {
                    _IsSelectedRok = value;
                    base.OnPropertyChanged(() => IsSelectedRok);
                }
            }
        }
        private int _WybraneIdPrzedmiotu;
        public int WybraneIdPrzedmiotu
        {
            get
            {
                return _WybraneIdPrzedmiotu;
            }
            set
            {
                if (value != _WybraneIdPrzedmiotu)
                {
                    _WybraneIdPrzedmiotu = value;
                    base.OnPropertyChanged(() => WybraneIdPrzedmiotu);
                }
            }
        }
        private int _WybraneIdGrupy;
        public int WybraneIdGrupy
        {
            get
            {
                return _WybraneIdGrupy;
            }
            set
            {
                if (value != _WybraneIdGrupy)
                {
                    _WybraneIdGrupy = value;
                    base.OnPropertyChanged(() => WybraneIdGrupy);
                }
            }
        }
        private int _NajwiecejNieobecnosci;
        public int NajwiecejNieobecnosci
        {
            get
            {
                return _NajwiecejNieobecnosci;
            }
            set
            {
                if (value != _NajwiecejNieobecnosci)
                {
                    _NajwiecejNieobecnosci = value;
                    base.OnPropertyChanged(() => NajwiecejNieobecnosci);
                }
            }
        }
        private int _NajmniejNieobecnosci;
        public int NajmniejNieobecnosci
        {
            get
            {
                return _NajmniejNieobecnosci;
            }
            set
            {
                if (value != _NajmniejNieobecnosci)
                {
                    _NajmniejNieobecnosci = value;
                    base.OnPropertyChanged(() => NajmniejNieobecnosci);
                }
            }
        }
        private int _IloscOsob;
        public int IloscOsob
        {
            get
            {
                return _IloscOsob;
            }
            set
            {
                if (value != _IloscOsob)
                {
                    _IloscOsob = value;
                    base.OnPropertyChanged(() => IloscOsob);
                }
            }
        }
        private string _KtoraPlec;
        public string KtoraPlec
        {
            get
            {
                return _KtoraPlec;
            }
            set
            {
                if (value != _KtoraPlec)
                {
                    _KtoraPlec = value;
                    base.OnPropertyChanged(() => KtoraPlec);
                }
            }
        }
        private string _NajlepszaKlasa;
        public string NajlepszaKlasa
        {
            get
            {
                return _NajlepszaKlasa;
            }
            set
            {
                if (value != _NajlepszaKlasa)
                {
                    _NajlepszaKlasa = value;
                    base.OnPropertyChanged(() => NajlepszaKlasa);
                }
            }
        }
        private string _NajgorszaKlasa;
        public string NajgorszaKlasa
        {
            get
            {
                return _NajgorszaKlasa;
            }
            set
            {
                if (value != _NajgorszaKlasa)
                {
                    _NajgorszaKlasa = value;
                    base.OnPropertyChanged(() => NajgorszaKlasa);
                }
            }
        }
        private string _NajlepszaKlasaText;
        public string NajlepszaKlasaText
        {
            get
            {
                return _NajlepszaKlasaText;
            }
            set
            {
                if (value != _NajlepszaKlasaText)
                {
                    _NajlepszaKlasaText = value;
                    base.OnPropertyChanged(() => NajlepszaKlasaText);
                }
            }
        }
        private string _NajgorszaKlasaText;
        public string NajgorszaKlasaText
        {
            get
            {
                return _NajgorszaKlasaText;
            }
            set
            {
                if (value != _NajgorszaKlasaText)
                {
                    _NajgorszaKlasaText = value;
                    base.OnPropertyChanged(() => NajgorszaKlasaText);
                }
            }
        }
        private DateTime _DataOd = DateTime.Now;
        public DateTime DataOd
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
        #region Listy
        private ObservableCollection<RaportNieobecnosciForAllView> _RaportNieobecnosciList;
        public ObservableCollection<RaportNieobecnosciForAllView> RaportNieobecnosciList
        {
            get
            {
                return _RaportNieobecnosciList;
            }
            set
            {
                _RaportNieobecnosciList = value;
                OnPropertyChanged(() => RaportNieobecnosciList);
            }
        }
        private ObservableCollection<RaportNieobecnosciLimitForAllView> _RaportNieobecnosciLimitList;
        public ObservableCollection<RaportNieobecnosciLimitForAllView> RaportNieobecnosciLimitList
        {
            get
            {
                return _RaportNieobecnosciLimitList;
            }
            set
            {
                _RaportNieobecnosciLimitList = value;
                OnPropertyChanged(() => RaportNieobecnosciLimitList);
            }
        }
        #endregion
        #region Comboboxy
        private ObservableCollection<KeyAndValue> _GrupaComboboxItems;
        public ObservableCollection<KeyAndValue> GrupaComboboxItems
        {
            get
            {
                return _GrupaComboboxItems;
            }
            set
            {
                _GrupaComboboxItems = value;
                OnPropertyChanged(() => GrupaComboboxItems);
            }
        }
        public IQueryable<KeyAndValue> PrzedmiotyComboboxItems
        {
            get
            {
                return new PodstawoweComboboxyLogic(szkolaEntities).GetAktywnePrzedmioty();
            }
        }
        #endregion
        #endregion
        #region Komendy
        private BaseCommand _StworzRaportCommand;
        public ICommand StworzRaportCommand
        {
            get
            {
                if (_StworzRaportCommand == null) { }
                {
                    _StworzRaportCommand = new BaseCommand(() => Load());
                }
                return _StworzRaportCommand;
            }
        }
        private BaseCommand _WyslijPowiadomienieCommand;
        public ICommand WyslijPowiadomienieCommand
        {
            get
            {
                if (_WyslijPowiadomienieCommand == null) { }
                {
                    _WyslijPowiadomienieCommand = new BaseCommand(() => WyslijPowiadomienie());
                }
                return _WyslijPowiadomienieCommand;
            }
        }
        #endregion
        #region Helpers
        public void CheckBox()
        {
            if (IsSelectedKlasa)
            {
                GrupaComboboxItems = new RaportOcenLogic(szkolaEntities).GetKlasy();
            }
            else
            {
                GrupaComboboxItems = new RaportOcenLogic(szkolaEntities).GetRok();
            }
        }
        public void Load()
        {
            RaportNieobecnosciList = new RaportNieobecnosciLogic(szkolaEntities).GetRaportNieobecnosci(IsSelectedKlasa, WybraneIdGrupy, WybraneIdPrzedmiotu, DataOd, DataDo);
            RaportNieobecnosciLimitList = new RaportNieobecnosciLogic(szkolaEntities).GetRaportNieobecnosciLimit(RaportNieobecnosciList, WybraneIdPrzedmiotu);
            NajwiecejNieobecnosci = new RaportNieobecnosciLogic(szkolaEntities).NajwiecejNieobecnosci(RaportNieobecnosciList);
            NajmniejNieobecnosci = new RaportNieobecnosciLogic(szkolaEntities).NajmniejNieobecnosci(RaportNieobecnosciList);
            IloscOsob = new RaportNieobecnosciLogic(szkolaEntities).IleOsobPrzekroczyloLimit(RaportNieobecnosciList);
            KtoraPlec = new RaportNieobecnosciLogic(szkolaEntities).KtoraPlecOtrzymalaLepszaFrekwencje(RaportNieobecnosciList);
            if (!IsSelectedKlasa)
            {
                NajlepszaKlasaText = "Najlepsza frekwencja w klasie: ";
                NajgorszaKlasaText = "Najgorsza frekwencja w klasie: ";
                NajlepszaKlasa = new RaportNieobecnosciLogic(szkolaEntities).KtoraKlasaOtrzymalaLepszaFrekwencje(RaportNieobecnosciList);
                NajgorszaKlasa = new RaportNieobecnosciLogic(szkolaEntities).KtoraKlasaOtrzymalaGorszaFrekwencje(RaportNieobecnosciList);
            }
            else
            {
                NajlepszaKlasaText = "";
                NajgorszaKlasaText = "";
                NajlepszaKlasa = "";
                NajgorszaKlasa = "";
            }
        }
        public void WyslijPowiadomienie()
        {
            new RaportNieobecnosciLogic(szkolaEntities).WyslijPowiadomienie(RaportNieobecnosciLimitList);
        }
        #endregion

    }
}
