using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace ClassroomManager.API
{
    public static class ApiConstantes
    {
        //API URL
        public static string BackendUrl = DeviceInfo.Platform == DevicePlatform.Android ?
            "http://10.0.2.2:5000" : 
            "https://localhost:5001/api/";

        public static string RegisterUser = "users/register";
        public static string AuthenticateUser = "users/authenticate";

        public static string Classrooms = "classrooms";
        public static string Eleves = "eleves/{0}";
        public static string Matieres = "matieres/{0}";
    }
}
