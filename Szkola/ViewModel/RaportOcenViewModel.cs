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
    public class RaportOcenViewModel : WorkspaceViewModel
    {
        #region Konstruktor
        public RaportOcenViewModel()
        {
            DisplayName = "Raport ocen";
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
        private int _WybraneIdFormySprawdzaniaWiedzy;
        public int WybraneIdFormySprawdzaniaWiedzy
        {
            get
            {
                return _WybraneIdFormySprawdzaniaWiedzy;
            }
            set
            {
                if (value != _WybraneIdFormySprawdzaniaWiedzy)
                {
                    _WybraneIdFormySprawdzaniaWiedzy = value;
                    base.OnPropertyChanged(() => WybraneIdFormySprawdzaniaWiedzy);
                }
            }
        }
        private double _NajwyzszaOcena;
        public double NajwyzszaOcena
        {
            get
            {
                return _NajwyzszaOcena;
            }
            set
            {
                if (value != _NajwyzszaOcena)
                {
                    _NajwyzszaOcena = value;
                    base.OnPropertyChanged(() => NajwyzszaOcena);
                }
            }
        }
        private double _NajnizszaOcena;
        public double NajnizszaOcena
        {
            get
            {
                return _NajnizszaOcena;
            }
            set
            {
                if (value != _NajnizszaOcena)
                {
                    _NajnizszaOcena = value;
                    base.OnPropertyChanged(() => NajnizszaOcena);
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
        private ObservableCollection<RaportOcenForAllView> _RaportOcenList;
        public ObservableCollection<RaportOcenForAllView> RaportOcenList
        {
            get
            {
                return _RaportOcenList;
            }
            set
            {
                _RaportOcenList = value;
                OnPropertyChanged(() => RaportOcenList);
            }
        }
        private ObservableCollection<RaportOcenProcentowo> _RaportOcenProcentowoList;
        public ObservableCollection<RaportOcenProcentowo> RaportOcenProcentowoList
        {
            get
            {
                return _RaportOcenProcentowoList;
            }
            set
            {
                _RaportOcenProcentowoList = value;
                OnPropertyChanged(() => RaportOcenProcentowoList);
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
        public IQueryable<KeyAndValue> FormySprawdzaniaWiedzyComboboxItems
        {
            get
            {
                return new PodstawoweComboboxyLogic(szkolaEntities).GetAktywneFormySprawdzaniaWiedzy();
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
            RaportOcenList = new RaportOcenLogic(szkolaEntities).GetRaportOcen(IsSelectedKlasa, WybraneIdGrupy, WybraneIdPrzedmiotu, _WybraneIdFormySprawdzaniaWiedzy, DataOd, DataDo);
            RaportOcenProcentowoList = new RaportOcenLogic(szkolaEntities).GetRaportOcenProcentowo(RaportOcenList);
            NajwyzszaOcena = new RaportOcenLogic(szkolaEntities).NajwyzszaOcena(RaportOcenList);
            NajnizszaOcena = new RaportOcenLogic(szkolaEntities).NajnizszaOcena(RaportOcenList);
            IloscOsob = new RaportOcenLogic(szkolaEntities).IleNajwyzszychOcen(RaportOcenList);
            KtoraPlec = new RaportOcenLogic(szkolaEntities).KtoraPlecOtrzymalaLepszeWyniki(RaportOcenList);
            if (!IsSelectedKlasa)
            {
                NajlepszaKlasaText = "Najlepsze oceny w klasie: ";
                NajgorszaKlasaText = "Najgorsze oceny w klasie: ";
                NajlepszaKlasa = new RaportOcenLogic(szkolaEntities).KtoraKlasaOtrzymalaLepszeWyniki(RaportOcenList);
                NajgorszaKlasa = new RaportOcenLogic(szkolaEntities).KtoraKlasaOtrzymalaGorszeWyniki(RaportOcenList);
            }
            else
            {
                NajlepszaKlasaText = "";
                NajgorszaKlasaText = "";
                NajlepszaKlasa = "";
                NajgorszaKlasa = "";
            }
        }
        #endregion

    }
}
