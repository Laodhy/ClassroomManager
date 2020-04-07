using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ClassroomManager.UI.MasterDetail
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterPage : MasterDetailPage
    {
        public MasterPage()
        {
            InitializeComponent();
            MasterPageCurrent.ListView.ItemSelected += ListView_ItemSelected;
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                var item = e.SelectedItem as MasterPageMasterMenuItem;
                if (item == null)
                    return;

                var page = (Page)Activator.CreateInstance(item.TargetType);
                page.Title = item.Title;

                Detail = new NavigationPage(page);
                IsPresented = true;

                MasterPageCurrent.ListView.SelectedItem = null;
            }
            catch (Exception ex)
            {

            }
        }

        private void MasterPageCurrent_ShowHomePage(object sender, EventArgs e)
        {
            Detail = new NavigationPage(new MainMenuPage());
        }
    }
}