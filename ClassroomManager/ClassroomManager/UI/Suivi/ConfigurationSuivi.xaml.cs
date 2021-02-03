using ClassroomManager.Data;
using ClassroomManager.Models;
using ClassroomManager.Tools;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ClassroomManager.UI.Suivi
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConfigurationSuivi : AppPage, INotifyPropertyChanged
    {
        private ObservableCollection<Groupe> _listeGroupe;
        public ObservableCollection<Groupe> ListeGroupe
        {
            get
            {
                return _listeGroupe;
            }
            set
            {
                _listeGroupe = value;
                OnPropertyChanged("ListeGroupe");
            }
        }

        private ObservableCollection<Matiere> _listeMatiere;
        public ObservableCollection<Matiere> ListeMatiere
        {
            get
            {
                return _listeMatiere;
            }
            set
            {
                _listeMatiere = value;
                OnPropertyChanged("ListeMatiere");
            }
        }

        private Classe _currentClasse { get; set; }

        public ConfigurationSuivi()
        {
            InitializeComponent();

            ToolbarItem changeClasseBarItem = new ToolbarItem()
            {
                IconImageSource = "settings.png",
                Order = ToolbarItemOrder.Primary,
                Text = "Changer de classe",
            };

            changeClasseBarItem.Clicked += SelectCurrentClasseAndUpdateUI;
            ToolbarItems.Add(changeClasseBarItem);

            ListeGroupe = new ObservableCollection<Groupe>();
            ListeMatiere = new ObservableCollection<Matiere>();

            this.Title = "Configuration du suivi";

            MatiereEntry.Keyboard = Keyboard.Create(KeyboardFlags.CapitalizeCharacter);
        }

        private async void SelectCurrentClasseAndUpdateUI(object sender, EventArgs e)
        {
            string[] listClasses = DataManager.Instance.ListClasses.Select(x => x.Nom).ToArray();

            string nomClasse = await DisplayActionSheet("Veuillez choisir une classe : ",
                "Annuler", null, listClasses);

            if (string.IsNullOrEmpty(nomClasse) || nomClasse == "Annuler")
                return;

            _currentClasse = DataManager.Instance.ListClasses.First(x => x.Nom == nomClasse);

            UpdateGroupeUI();
            UpdateMatiereUI();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            BindingContext = this;

            Binding myBindingGroupe = new Binding("ListeGroupe");
            GroupeList.SetBinding(ListView.ItemsSourceProperty, myBindingGroupe);

            Binding myBindingMatiere = new Binding("ListeMatiere");
            MatieresList.SetBinding(ListView.ItemsSourceProperty, myBindingMatiere);

            SelectCurrentClasseAndUpdateUI(this,new EventArgs());

        }
        // =============================================================================
        #region Matières

        private async void MatiereEntry_Completed(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(MatiereEntry.Text))
            {
                Matiere newMatiere = new Matiere()
                {
                    Nom = MatiereEntry.Text,
                };

                //await DataManager.Instance.AddMatiere(newMatiere);
                UpdateMatiereUI();

                MatiereEntry.Text = "";
            }
        }


        private void ViewCell_Matiere_MatiereDeleted(object sender, EventArgs e)
        {
            UpdateMatiereUI();
        }

        private void MatiereList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                Matiere mat = e.SelectedItem as Matiere;

                //CreateActivitiesViewCtrl.Init(mat);

                MatieresList.SelectedItem = null;
            }
        }

        private void UpdateMatiereUI()
        {
            try
            {
                //ListeMatiere = new ObservableCollection<Matiere>(DataManager.Instance.ListMatieres);
            }
            catch (Exception ex)
            {
                AlertError.ShowErrorAlert("Un problème à eu lieu lors de la mise à jour du contenu des matières");
            }

        }
        #endregion
        // =============================================================================
        #region Groupes
        private void GroupeEntry_Completed(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(GroupeEntry.Text))
            {
                //PopupGroupeGrid.Init(GroupeEntry.Text);
                //PopupGroupeGrid.IsVisible = true;

                GroupeEntry.Text = "";
            }
        }

        private void GroupeList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                Groupe grp = e.SelectedItem as Groupe;
                //PopupGroupeGrid.Init(grp);
                //PopupGroupeGrid.IsVisible = true;

                GroupeList.SelectedItem = null;
            }
        }

        private async void PopupGroupeGrid_GroupeCreated(object sender, Groupe e)
        {
            //await DataManager.Instance.AddGroupe(e);
            UpdateGroupeUI();
            //PopupGroupeGrid.IsVisible = false;
        }

        private void ViewCell_Groupe_GroupeDeleted(object sender, EventArgs e)
        {
            UpdateGroupeUI();
        }

        private void UpdateGroupeUI()
        {
            try
            {
                //ListeGroupe = new ObservableCollection<Groupe>(DataManager.Instance.ListGroupes);
            }
            catch (Exception ex)
            {
                DisplayAlert("Oups", "Un problème à eu lieu lors de la mise à jour du contenu des groupes", "Ok");
            }

        }
        #endregion

        // =============================================================================
        /*private async void PopupAddActivityGrid_ActivityCreated(object sender, Activite e)
        {
            await DataManager.Instance.AddActivite(e);
            UpdateActiviteUI();
            PopupAddActivityGrid.IsVisible = false;
        }


        private void UpdateActiviteUI()
        {
            CreateActivitiesViewCtrl.UpdateUI();
        }

        private void CreateActivitiesViewCtrl_ActivityAddedOrEdit(object sender, Activite e)
        {
            PopupAddActivityGrid.Init(e);
            PopupAddActivityGrid.IsVisible = true;
        }*/
        // =============================================================================
        #region INotify
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}