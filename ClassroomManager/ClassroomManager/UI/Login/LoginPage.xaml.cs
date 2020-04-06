using ClassroomManager.API;
using ClassroomManager.Models;
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
        #endregion
        // ====================================================================
        #region privates methods
        private async Task ConnectUser()
        {
            try
            {
                string email = entryEmail.Text;
                string pwd = entryPassword.Text;

                if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(pwd))
                    return;

                bool isAuth = await ApiManager.Instance.Authenticate(new UserAuthentification()
                {
                    Email = email,
                    Password = pwd
                });

                if (!isAuth)
                {
                    await DisplayAlert("Erreur", "L'email ou le mot de passe est incorrect", "cancel");
                }
                else
                {
                    //Change page
                    await DisplayAlert("Test", "Auth", "cancel");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erreur", ex.Message, "Ok");
            }
        }

        #endregion
        // ====================================================================
    }
}