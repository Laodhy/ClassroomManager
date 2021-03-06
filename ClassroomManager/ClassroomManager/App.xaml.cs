﻿using ClassroomManager.API;
using ClassroomManager.Data;
using ClassroomManager.Models;
using ClassroomManager.UI.Login;
using ClassroomManager.UI.MasterDetail;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ClassroomManager
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new LoginPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override async void OnResume()
        {

            if (DataManager.Instance.CurrentUser != null)
            {
                UserAuthentification user = new UserAuthentification()
                {
                    Email = DataManager.Instance.CurrentUser.Email,
                    Password = DataManager.Instance.CurrentUser.Password
                };

                if (!await ApiManager.Instance.Authenticate(user))
                {
                    MainPage = new NavigationPage(new LoginPage());
                }
            }
        }

        public async Task UserIsAuth()
        {
            await DataManager.Instance.Init();
            //Change page
            App.Current.MainPage = new MasterPage();
        }
    }
}
