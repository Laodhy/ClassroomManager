using ClassroomManager.UI.Eleves;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ClassroomManager.UI
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainMenuPage : AppPage
    {
        public MainMenuPage()
        {
            InitializeComponent();
        }

        public override void ShowLoader()
        {
            base.ShowLoader();
        }

        // ====================================================================
        #region events

        private void BtnListeEleve_Clicked(object sender, EventArgs e)
        {
            ListeEleves page = new ListeEleves();

            (Application.Current.MainPage as UI.MasterDetail.MasterPage).Detail 
                = new NavigationPage(page);
        }

        #endregion
        // ====================================================================
    }
}