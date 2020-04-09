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

namespace ClassroomManager.UI.Eleves
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListeEleves : AppPage, INotifyPropertyChanged
    {

        private ObservableCollection<Eleve> _liste;
        public ObservableCollection<Eleve> Liste
        {
            get
            {
                return _liste;
            }
            set
            {
                _liste = value;
                OnPropertyChanged("Liste");
            }
        }

        private Classe _currentClasse;

        public ListeEleves()
        {
            InitializeComponent();

            Liste = new ObservableCollection<Eleve>();

            EntryName.Keyboard = Keyboard.Create(KeyboardFlags.CapitalizeCharacter);
            EntryPrenom.Keyboard = Keyboard.Create(KeyboardFlags.CapitalizeWord);

            this.Title = "Liste des élèves";
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            Liste.Clear();

            BindingContext = this;
            Binding myBinding = new Binding("Liste");
            ListviewParticipant.SetBinding(ListView.ItemsSourceProperty, myBinding);

            ClassesPicker.BindingContext = DataManager.Instance;
            Binding myBindingClasses = new Binding("ListClasses");
            ClassesPicker.SetBinding(Picker.ItemsSourceProperty, myBindingClasses);

            ClassesPicker.SelectedIndex = 0;

            UpdateUI();
        }

        public override void ShowLoader()
        {
            base.ShowLoader();

            loaderGrid.IsVisible = true;
        }

        public override void HideLoader()
        {
            base.HideLoader();

            loaderGrid.IsVisible = false;
        }
        // =============================================================================

        private async void BtnValider_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(EntryName.Text) && !string.IsNullOrEmpty(EntryPrenom.Text))
                {
                    ShowLoader();

                    EleveAdded part = new EleveAdded();
                    part.Nom = EntryName.Text;
                    part.Prenom = EntryPrenom.Text;

                    await DataManager.Instance.AddEleve(_currentClasse.Id, part);

                    UpdateUI();

                    EntryName.Text = "";
                    EntryPrenom.Text = "";

                    EntryName.Focus();
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Oups", "Un problème à eu lieu lors de l'ajout d'un élève", "Ok");

            }
            finally
            {
                HideLoader();
            }
        }

        private void ViewCell_Eleve_EleveDeleted(object sender, EventArgs e)
        {
            UpdateUI();
        }

        private void ClassesPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            _currentClasse = ClassesPicker.SelectedItem as Classe;

            UpdateUI();

        }

        private async void ClasseEntry_Completed(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(ClasseEntry.Text))
                {
                    ShowLoader();

                    ClasseAdded c = new ClasseAdded();
                    c.Nom = ClasseEntry.Text;

                    await DataManager.Instance.AddClasse(c);

                    ClasseEntry.Text = "";

                    List<Classe> list = DataManager.Instance.ListClasses.OrderBy(x => x.Nom).ToList();
                    Classe newClasse = list.Find(x => x.Nom == c.Nom);
                    int index = list.IndexOf(newClasse);

                    ClassesPicker.SelectedIndex = index;

                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                HideLoader();
            }
        }

        private async void ButtonDeleteClasse_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (ClassesPicker.SelectedItem == null)
                    return;

                if (await DisplayAlert("Supprimer classe", "Êtes-vous sûr de vouloir supprimer cette classe ?", "Oui", "Non"))
                {
                    ShowLoader();

                    await DataManager.Instance.DeleteClasse(_currentClasse.Id);

                    ClassesPicker.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                HideLoader();
            }
        }

        private void EntryName_Completed(object sender, EventArgs e)
        {
            this.EntryPrenom.Focus();
        }

        private async void UpdateUI()
        {
            try
            {
                if (_currentClasse == null)
                {
                    nbParticipant.Text = "";
                    Liste.Clear();

                    return;
                }

                ShowLoader();

                await DataManager.Instance.UpdateListeEleveByIdClassroom(_currentClasse.Id);

                //Attendre 1 seconde
                await Task.Delay(1000);

                nbParticipant.Text = DataManager.Instance.ListEleves.Where(x => x.IdClasse == _currentClasse.Id).Count().ToString() + " participant(s)";
                Liste = new ObservableCollection<Eleve>(DataManager.Instance.ListEleves.Where(x => x.IdClasse == _currentClasse.Id).OrderBy(x => x.Nom));
            }
            catch (Exception ex)
            {
                AlertError.ShowErrorAlert("Un problème à eu lieu lors de la mise à jour du contenu");
            }
            finally
            {
                HideLoader();
            }

        }

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