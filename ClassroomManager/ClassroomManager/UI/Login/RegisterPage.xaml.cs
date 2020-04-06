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
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();
        }

        // ====================================================================
        #region events
        private void ContentPage_Appearing(object sender, EventArgs e)
        {
            lblErrorPassword.IsVisible = false;
        }

        private async void BtnValidate_Clicked(object sender, EventArgs e)
        {
            await RegisterUser();
        }

        private async void EntryPassword_Completed(object sender, EventArgs e)
        {
            await RegisterUser();
        }
        private async void EntryEmail_Completed(object sender, EventArgs e)
        {
            await RegisterUser();
        }
        #endregion
        // ====================================================================
        #region privates methods
        private async Task RegisterUser()
        {
            try
            {
                string email = entryEmail.Text;
                string pwd = entryPassword.Text;
                string confirmPwd = entryConfirmPassword.Text;

                if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(pwd) || string.IsNullOrEmpty(confirmPwd))
                    return;

                if (!confirmPwd.Equals(pwd))
                    lblErrorPassword.IsVisible = true;
                else 
                    lblErrorPassword.IsVisible = false;

                bool isAuth = await ApiManager.Instance.Register(new UserAuthentification()
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