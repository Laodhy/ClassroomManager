using ClassroomManager.Data;
using ClassroomManager.Models;
using ClassroomManager.Tools;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace ClassroomManager.API
{
    public class ApiManager : Singleton<ApiManager>
    {
        HttpClient _client;
        bool IsConnected => Connectivity.NetworkAccess == NetworkAccess.Internet;

        public ApiManager()
        {
            //LDN : Passer outre ce fucking certicat
            var handler = new HttpClientHandler();
            handler.ClientCertificateOptions = ClientCertificateOption.Manual;
            handler.ServerCertificateCustomValidationCallback =
                (httpRequestMessage, cert, cetChain, policyErrors) =>
                {
                    return true;
                };
            // ---------------------------------

            _client = new HttpClient(handler);
            _client.BaseAddress = new Uri($"{ApiConstantes.BackendUrl}");
        }

        // ====================================================================
        #region Users
        public async Task<bool> Register(UserAuthentification user)
        {
            if (user == null || !IsConnected)
                return false;

            var serializedItem = JsonConvert.SerializeObject(user);

            HttpResponseMessage response = await _client.PostAsync(
                $"users/register",
                new StringContent(serializedItem, Encoding.UTF8, "application/json")
                );

            if (response.IsSuccessStatusCode)
            {
                //Set the current User for the application
                string content = await response.Content.ReadAsStringAsync();
                User userAuth = JsonConvert.DeserializeObject<User>(content);
                userAuth.Password = user.Password;
                DataManager.Instance.CurrentUser = userAuth;
            }
            else
                DataManager.Instance.CurrentUser = null;

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> Authenticate(UserAuthentification user)
        {
            if (user == null || !IsConnected)
                return false;

            var serializedItem = JsonConvert.SerializeObject(user);

            HttpResponseMessage response = await _client.PostAsync(
                $"users/authenticate",
                new StringContent(serializedItem, Encoding.UTF8, "application/json")
                );

            if (response.IsSuccessStatusCode)
            {
                //Set the current User for the application
                string content = await response.Content.ReadAsStringAsync();
                User userAuth = JsonConvert.DeserializeObject<User>(content);
                userAuth.Password = user.Password;
                DataManager.Instance.CurrentUser = userAuth;
            }
            else
                DataManager.Instance.CurrentUser = null;

            return response.IsSuccessStatusCode;

        }
        #endregion
        // ====================================================================
        #region 

        #endregion
        // ====================================================================
    }
}
