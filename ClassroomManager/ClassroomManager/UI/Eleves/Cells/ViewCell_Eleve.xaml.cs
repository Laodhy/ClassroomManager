using ClassroomManager.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ClassroomManager.UI.Eleves.Cells
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewCell_Eleve : ViewCell
    {
        public ViewCell_Eleve()
        {
            InitializeComponent();
        }

        private async void DeleteBtn_Clicked(object sender, EventArgs e)
        {
            if (await App.Current.MainPage.DisplayAlert("Supprimer élève", "Êtes-vous sûr de vouloir supprimer cet élève ?", "Oui", "Non"))
            {
                int IdParticipant = Convert.ToInt32((sender as Button).CommandParameter);
                await DataManager.Instance.DeleteEleve(IdParticipant);
                EleveDeleted?.Invoke(this, e);
            }
        }

        public event EventHandler EleveDeleted;
    }
}