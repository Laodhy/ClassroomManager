using ClassroomManager.UI.Eleves;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ClassroomManager.UI.MasterDetail
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterPageMaster : ContentPage
    {
        public ListView ListView;

        public MasterPageMaster()
        {
            InitializeComponent();

            BindingContext = new MasterPageMasterViewModel();
            ListView = MenuItemsListView;
        }

        class MasterPageMasterViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<MasterPageMasterMenuItem> MenuItems { get; set; }

            public MasterPageMasterViewModel()
            {
                MenuItems = new ObservableCollection<MasterPageMasterMenuItem>(new[]
                {
                    new MasterPageMasterMenuItem { Id = 1, Title = "Nouveau quizz" },
                    new MasterPageMasterMenuItem { Id = 3, Title = "Historique des quizz" },
                    new MasterPageMasterMenuItem { Id = 2, Title = "Nouvelle évalutation" },
                    new MasterPageMasterMenuItem { Id = 4, Title = "Historique des évaluations" },
                    new MasterPageMasterMenuItem { Id = 0, Title = "Suivi du travail" },
                    new MasterPageMasterMenuItem { Id = 7, Title = "Configuration du suivi" },
                    new MasterPageMasterMenuItem { Id = 5, Title = "Liste des élèves", TargetType=typeof(ListeEleves) },
                    new MasterPageMasterMenuItem { Id = 6, Title = "Déconnexion" },
                });
            }

            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            ShowHomePage?.Invoke(this, e);
        }

        public event EventHandler ShowHomePage;
    }
}