using ClassroomManager.Data;
using ClassroomManager.Models;
using ClassroomManager.Tools;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
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
                ApiConstantes.RegisterUser,
                new StringContent(serializedItem, Encoding.UTF8, "application/json")
                );

            if (response.IsSuccessStatusCode)
            {
                //Set the current User for the application
                string content = await response.Content.ReadAsStringAsync();
                User userAuth = JsonConvert.DeserializeObject<User>(content);
                userAuth.Password = user.Password;
                DataManager.Instance.CurrentUser = userAuth;

                _client.DefaultRequestHeaders.Clear();
                _client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", userAuth.AccessToken);
            }
            else
            {
                _client.DefaultRequestHeaders.Clear();
                DataManager.Instance.CurrentUser = null;
            }

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> Authenticate(UserAuthentification user)
        {
            if (user == null || !IsConnected)
                return false;

            var serializedItem = JsonConvert.SerializeObject(user);

            HttpResponseMessage response = await _client.PostAsync(
                ApiConstantes.AuthenticateUser,
                new StringContent(serializedItem, Encoding.UTF8, "application/json")
                );

            if (response.IsSuccessStatusCode)
            {
                //Set the current User for the application
                string content = await response.Content.ReadAsStringAsync();
                User userAuth = JsonConvert.DeserializeObject<User>(content);
                userAuth.Password = user.Password;
                DataManager.Instance.CurrentUser = userAuth;

                _client.DefaultRequestHeaders.Clear();
                _client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", userAuth.AccessToken);
            }
            else
            {
                _client.DefaultRequestHeaders.Clear();
                DataManager.Instance.CurrentUser = null;
            }

            return response.IsSuccessStatusCode;

        }
        #endregion
        // ====================================================================
        #region Classrooms

        public async Task<List<Classe>> GetListClassrooms()
        {
            try
            {
                if (DataManager.Instance.CurrentUser == null || !IsConnected)
                    throw new Exception();

                List<Classe> list = new List<Classe>();

                HttpResponseMessage response = await _client.GetAsync(ApiConstantes.Classrooms);

                if (response.IsSuccessStatusCode)
                {
                    //Set the current User for the application
                    string content = await response.Content.ReadAsStringAsync();
                    list = JsonConvert.DeserializeObject<List<Classe>>(content);
                }

                return list;
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de la récupération de la liste des classes");
            }

        }

        public async Task<Classe> AddClassroom(ClasseAdded classe)
        {
            try
            {
                if (DataManager.Instance.CurrentUser == null || !IsConnected)
                    throw new Exception();

                var serializedItem = JsonConvert.SerializeObject(classe);
                HttpResponseMessage response = await _client.PostAsync(
                    ApiConstantes.Classrooms,
                    new StringContent(serializedItem, Encoding.UTF8, "application/json")
                );

                if (response.IsSuccessStatusCode)
                {
                    //Set the current User for the application
                    string content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<Classe>(content);
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de la création d'une nouvelle classe");
            }
        }

        public async Task<Classe> DeleteClassroomById(int id)
        {
            try
            {
                if (DataManager.Instance.CurrentUser == null || !IsConnected)
                    throw new Exception();

                HttpResponseMessage response = await _client.DeleteAsync(ApiConstantes.Classrooms + "/" + id);

                if (response.IsSuccessStatusCode)
                {
                    //Set the current User for the application
                    string content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<Classe>(content);
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de la suppression d'une classe");
            }
        }
        #endregion
        // ====================================================================
        #region Eleves

        public async Task<List<Eleve>> GetListElevesByIdClassroom(int idClassroom)
        {
            try
            {
                if (DataManager.Instance.CurrentUser == null || !IsConnected)
                    throw new Exception();

                List<Eleve> list = new List<Eleve>();

                HttpResponseMessage response = 
                    await _client.GetAsync(string.Format(ApiConstantes.Eleves, idClassroom));

                if (response.IsSuccessStatusCode)
                {
                    //Set the current User for the application
                    string content = await response.Content.ReadAsStringAsync();
                    list = JsonConvert.DeserializeObject<List<Eleve>>(content);
                }

                return list;
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de la récupération de la liste des élèves");
            }
        }

        public async Task<Eleve> AddEleveByIdClassroom(int idClassroom, EleveAdded eleve)
        {
            try
            {
                if (DataManager.Instance.CurrentUser == null || !IsConnected)
                    throw new Exception();

                var serializedItem = JsonConvert.SerializeObject(eleve);
                HttpResponseMessage response = await _client.PostAsync(
                    string.Format(ApiConstantes.Eleves, idClassroom),
                    new StringContent(serializedItem, Encoding.UTF8, "application/json")
                );

                if (response.IsSuccessStatusCode)
                {
                    //Set the current User for the application
                    string content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<Eleve>(content);
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de la création d'un nouvel élève");
            }
        }

        public async Task<Eleve> DeleteEleveById(int id)
        {
            try
            {
                if (DataManager.Instance.CurrentUser == null || !IsConnected)
                    throw new Exception();

                HttpResponseMessage response = await _client.DeleteAsync(string.Format(ApiConstantes.Eleves, id));

                if (response.IsSuccessStatusCode)
                {
                    //Set the current User for the application
                    string content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<Eleve>(content);
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de la suppression d'un élève");
            }
        }
        #endregion  
        // ====================================================================
        #region Matieres

        public async Task<List<Matiere>> GetListMatieresByIdClassroom(int idClassroom)
        {
            try
            {
                if (DataManager.Instance.CurrentUser == null || !IsConnected)
                    throw new Exception();

                List<Matiere> list = new List<Matiere>();

                HttpResponseMessage response =
                    await _client.GetAsync(string.Format(ApiConstantes.Matieres, idClassroom));

                if (response.IsSuccessStatusCode)
                {
                    //Set the current User for the application
                    string content = await response.Content.ReadAsStringAsync();
                    list = JsonConvert.DeserializeObject<List<Matiere>>(content);
                }

                return list;
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de la récupération de la liste des matières");
            }
        }


        public async Task<Matiere> AddMatiereByIdClassroom(int idClassroom, MatiereAdded mat)
        {
            try
            {
                if (DataManager.Instance.CurrentUser == null || !IsConnected)
                    throw new Exception();

                var serializedItem = JsonConvert.SerializeObject(mat);
                HttpResponseMessage response = await _client.PostAsync(
                    string.Format(ApiConstantes.Matieres, idClassroom),
                    new StringContent(serializedItem, Encoding.UTF8, "application/json")
                );

                if (response.IsSuccessStatusCode)
                {
                    //Set the current User for the application
                    string content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<Matiere>(content);
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de la création d'une nouvelle matière");
            }
        }

        public async Task<Matiere> DeleteMatiereById(int id)
        {
            try
            {
                if (DataManager.Instance.CurrentUser == null || !IsConnected)
                    throw new Exception();

                HttpResponseMessage response = await _client.DeleteAsync(string.Format(ApiConstantes.Matieres, id));

                if (response.IsSuccessStatusCode)
                {
                    //Set the current User for the application
                    string content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<Matiere>(content);
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de la suppression d'une matière");
            }
        }

        #endregion

        // ====================================================================
    }
}
