using Firma.Helper;
using Firma.ViewModel;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace Szkola.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {
        #region Komendy menu i paska narzedzi
        public ICommand NowyUzytkownikCommand
        {
            get
            {
                return new BaseCommand(() => createView(new NowyUzytkownikViewModel()));
            }

        }
        public ICommand UzytkownicyCommand 
        {
            get
            {
                return new BaseCommand(() => createView(new WszyscyUzytkownicyViewModel()));
            }

        }
        public ICommand NoweOgloszenieCommand
        {
            get
            {
                return new BaseCommand(() => createView(new NoweOgloszenieViewModel()));
            }
        }
        public ICommand OgloszeniaCommand
        {
            get
            {
                return new BaseCommand(() => createView(new WszystkieOgloszeniaViewModel()));
            }
        }
        public ICommand NowyPlanLekcjiCommand
        {
            get
            {
                return new BaseCommand(() => createView(new NowyPlanLekcjiViewModel()));
            }
        }
        public ICommand PlanyLekcjiCommand
        {
            get
            {
                return new BaseCommand(() => createView(new WszystkiePlanyLekcjiViewModel()));
            }
        }
        public ICommand NowaKlasaCommand
        {
            get
            {
                return new BaseCommand(() => createView(new NowaKlasaViewModel()));
            }
        }
        public ICommand KlasyCommand
        {
            get
            {
                return new BaseCommand(() => createView(new WszystkieKlasyViewModel()));
            }
        }
        public ICommand DziennikOcenCommand
        {
            get
            {
                return new BaseCommand(() => createView(new DziennikOcenViewModel()));
            }
        }
        public ICommand DodajOceneCommand
        {
            get
            {
                return new BaseCommand(() => createView(new DodajOceneViewModel()));
            }
        }
        public ICommand DziennikObecnosciCommand
        {
            get
            {
                return new BaseCommand(() => createView(new DziennikObecnosciViewModel()));
            }
        }
        public ICommand DodajNieobecnoscCommand
        {
            get
            {
                return new BaseCommand(() => createView(new DodajNieobecnoscViewModel()));
            }
        }
        public ICommand RaportNieobecnosciCommand
        {
            get
            {
                return new BaseCommand(() => createView(new RaportNieobecnosciViewModel()));
            }
        }
        public ICommand RaportOcenCommand
        {
            get
            {
                return new BaseCommand(() => createView(new RaportOcenViewModel()));
            }
        }
        public ICommand ZamknijCommand
        {
            get
            {
                return new BaseCommand(Application.Current.Shutdown);
            }
        }
        #endregion

        #region Przyciski w menu z lewej strony
        private ReadOnlyCollection<CommandViewModel> _Commands;
        public ReadOnlyCollection<CommandViewModel> Commands
        {
            get
            {
                if (_Commands == null)
                {
                    List<CommandViewModel> cmds = this.CreateCommands();
                    _Commands = new ReadOnlyCollection<CommandViewModel>(cmds);
                }
                return _Commands;
            }
        }

        private List<CommandViewModel> CreateCommands()
        {
            Messenger.Default.Register<string>(this, open);
            return new List<CommandViewModel>
            {
                new CommandViewModel("Użytkownicy", "/View/Content/Images/UzytkownicyImage.png" , new BaseCommand(()=>createView(new WszyscyUzytkownicyViewModel()))),
                new CommandViewModel("Nowy Użytkownik", "/View/Content/Images/DodajUzytkownikaImage.png" , new BaseCommand(()=>createView(new NowyUzytkownikViewModel()))),
                new CommandViewModel("Klasy", "/View/Content/Images/KlasyImage.png" , new BaseCommand(() => createView(new WszystkieKlasyViewModel()))),
                new CommandViewModel("Nowa Klasa", "/View/Content/Images/DodajKlaseImage.png" , new BaseCommand(() => createView(new NowaKlasaViewModel()))),
                new CommandViewModel("Plany Lekcji", "/View/Content/Images/PlanyLekcjiImage.png" , new BaseCommand(() => createView(new WszystkiePlanyLekcjiViewModel()))),
                new CommandViewModel("Nowy Plan Lekcji", "/View/Content/Images/DodajPlanLekcjiImage.png" , new BaseCommand(() => createView(new NowyPlanLekcjiViewModel()))),
                new CommandViewModel("Ogłoszenia", "/View/Content/Images/OgloszeniaImage.png" , new BaseCommand(() => createView(new WszystkieOgloszeniaViewModel()))),
                new CommandViewModel("Nowe Ogłoszenie", "/View/Content/Images/DodajOgloszenieImage.png" , new BaseCommand(() => createView(new NoweOgloszenieViewModel()))),
                new CommandViewModel("Dziennik Ocen", "/View/Content/Images/DodajOgloszenieImage.png" , new BaseCommand(() => createView(new DziennikOcenViewModel()))),
                new CommandViewModel("Dodaj Ocene", "/View/Content/Images/DodajOgloszenieImage.png" , new BaseCommand(() => createView(new DodajOceneViewModel()))),
                new CommandViewModel("Dziennik Obcenosci", "/View/Content/Images/ListaObecnosciImage.png" , new BaseCommand(() => createView(new DziennikObecnosciViewModel()))),
                new CommandViewModel("Dodaj Nieobecnosc", "/View/Content/Images/DodajNieobecnoscImage.png" , new BaseCommand(() => createView(new DodajNieobecnoscViewModel()))),
                new CommandViewModel("Raport Nieobecnosci", "/View/Content/Images/RaportObecnosciImage.png" , new BaseCommand(() => createView(new RaportNieobecnosciViewModel()))),
                new CommandViewModel("Raport Ocen", "/View/Content/Images/RaportOcenImage.png" , new BaseCommand(() => createView(new RaportOcenViewModel())))
            };
        }

        #endregion

        #region Zakladki
        private ObservableCollection<WorkspaceViewModel> _Workspaces; //kolekcja zakladek
        public ObservableCollection<WorkspaceViewModel> Workspaces
        {
            get
            {
                if (_Workspaces == null)
                {
                    _Workspaces = new ObservableCollection<WorkspaceViewModel>();
                    _Workspaces.CollectionChanged += this.onWorkspacesChanged;
                }
                return _Workspaces;
            }
        }
        private void onWorkspacesChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null && e.NewItems.Count != 0)
                foreach (WorkspaceViewModel workspace in e.NewItems)
                    workspace.RequestClose += this.onWorkspaceRequestClose;
            if (e.OldItems != null && e.OldItems.Count != 0) foreach (WorkspaceViewModel
                    workspace in e.OldItems) workspace.RequestClose -= this.onWorkspaceRequestClose;
        }
        private void onWorkspaceRequestClose(object sender, EventArgs e)
        {
            WorkspaceViewModel workspace = sender as WorkspaceViewModel;
            this.Workspaces.Remove(workspace);
        }
        #endregion
        #region Funkcje pomocnicze
        private void createView(WorkspaceViewModel workspace)
        {
            this.Workspaces.Add(workspace);
            this.setActiveWorkspace(workspace);
        }
        private void setActiveWorkspace(WorkspaceViewModel workspace)
        {
            Debug.Assert(this.Workspaces.Contains(workspace));
            ICollectionView collectionView = CollectionViewSource.GetDefaultView(this.Workspaces);
            if (collectionView != null) collectionView.MoveCurrentTo(workspace);
        }
        private void open(string name)
        {
            if (name == "UzytkownicyAdd")
            {
                createView(new NowyUzytkownikViewModel());
            }
            if (name == "KlasyAdd")
            {
                createView(new NowaKlasaViewModel());
            }
            if (name == "OgloszeniaAdd")
            {
                createView(new NoweOgloszenieViewModel());
            }
            if (name == "Plany lekcjiAdd")
            {
                createView(new NowyPlanLekcjiViewModel());
            }
            if (name == "StudenciOceny")
            {
                createView(new WszyscyUzytkownicyViewModel());
                Messenger.Default.Send("StudenciOcena");
            }
            if (name == "StudenciObecnosci")
            {
                createView(new WszyscyUzytkownicyViewModel());
                Messenger.Default.Send("StudenciObecnosc");
            }
            if (name == "UsersAll")
            {
                createView(new WszyscyUzytkownicyViewModel());
                Messenger.Default.Send("Uzytkownicy");
            }
            if (name == "KlasyOceny")
            {
                createView(new WszystkieKlasyViewModel());
                Messenger.Default.Send("KlasyOcena");
            }
            if (name == "KlasyObecnosci")
            {
                createView(new WszystkieKlasyViewModel());
                Messenger.Default.Send("KlasyObecnosc");
            }
            if (name == "Dziennik ocenAdd")
            {
                createView(new DodajOceneViewModel());
            }
            if (name == "Dziennik obecnościAdd")
            {
                createView(new DodajNieobecnoscViewModel());
            }
        }
        #endregion
    }
}
