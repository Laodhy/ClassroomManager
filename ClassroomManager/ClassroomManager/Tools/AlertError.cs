using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ClassroomManager.Tools
{
    public static class AlertError
    {
        public static void ShowErrorAlert(string message)
        {
            Application.Current.MainPage.DisplayAlert("Erreur", message, "Ok");
        }
    }
}
