using ClassroomManager.API;
using ClassroomManager.Data;
using ClassroomManager.Models;
using ClassroomManager.UI.MasterDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ClassroomManager.UI.Login
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        // ====================================================================
        #region events
        private async void BtnValidate_Clicked(object sender, EventArgs e)
        {
            await ConnectUser();
        }

        private async void EntryPassword_Completed(object sender, EventArgs e)
        {
            await ConnectUser();
        }
        private async void EntryEmail_Completed(object sender, EventArgs e)
        {
            await ConnectUser();
        }
        private async void CreateAccountlbl_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegisterPage());
        }

        private async void ContentPage_Appearing(object sender, EventArgs e)
        {
#if DEBUG
            await ConnectUser();
#endif
        }

        #endregion
        // ====================================================================
        #region privates methods
        private async Task ConnectUser()
        {
            try
            {
#if DEBUG
                loaderGrid.IsVisible = true;

                bool isAuth = await ApiManager.Instance.Authenticate(new UserAuthentification()
                {
                    Email = "test@test.fr",
                    Password = "password"
                });
#else
                string email = entryEmail.Text;
                string pwd = entryPassword.Text;

                if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(pwd))
                    return;

                loaderGrid.IsVisible = true;

                bool isAuth = await ApiManager.Instance.Authenticate(new UserAuthentification()
                {
                    Email = email,
                    Password = pwd
                });
#endif

                if (!isAuth)
                {
                    await DisplayAlert("Erreur", "L'email ou le mot de passe est incorrect", "cancel");
                }
                else
                {
                    await ((App)App.Current).UserIsAuth();
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erreur", ex.Message, "Ok");
#if DEBUG
                await ((App)App.Current).UserIsAuth();
#endif
            }
            finally
            {
                loaderGrid.IsVisible = false;
            }
        }
#endregion
        // ====================================================================
    }
}