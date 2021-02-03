using ClassroomManager.API;
using ClassroomManager.Models;
using ClassroomManager.Tools;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassroomManager.Data
{
    public class DataManager : Singleton<DataManager>, INotifyPropertyChanged
    {
        #region properties
        private ObservableCollection<Classe> _listClasses;
        public ObservableCollection<Classe> ListClasses
        {
            get
            {
                return _listClasses;
            }
            set
            {
                _listClasses = value;
                OnPropertyChanged("ListClasses");
            }
        }

        private ObservableCollection<Eleve> _listEleves;
        public ObservableCollection<Eleve> ListEleves
        {
            get
            {
                return new ObservableCollection<Eleve>(_listEleves.OrderBy(x => x.Prenom));
            }
            set
            {
                _listEleves = value;
                OnPropertyChanged("ListEleves");
            }
        }
        public User CurrentUser { get; set; }
        #endregion

        public DataManager()
        {
            CurrentUser = null;
            ListEleves = new ObservableCollection<Eleve>();
            ListClasses = new ObservableCollection<Classe>();
        }
        public async Task Init()
        {
            try
            {
                ListClasses = new ObservableCollection<Classe>(await ApiManager.Instance.GetListClassrooms());
            }
            catch (Exception ex)
            {
                AlertError.ShowErrorAlert(ex.Message);
            }
        }

        // =============================================================================
        #region Classes
        public async Task AddClasse(ClasseAdded c)
        {
            try
            {
                if (ListClasses.FirstOrDefault(x => x.Nom == c.Nom) != null)
                    throw new Exception("Une classe avec ce nom existe déjà !");

                Classe classroom = await ApiManager.Instance.AddClassroom(c);
                ListClasses.Add(classroom);
            }
            catch (Exception ex)
            {
                AlertError.ShowErrorAlert(ex.Message);
            }
        }

        public async Task DeleteClasse(int id)
        {
            try
            {
                Classe classroom = await ApiManager.Instance.DeleteClassroomById(id);
                ListClasses.Remove(classroom);

            }
            catch (Exception ex)
            {
                AlertError.ShowErrorAlert(ex.Message);
            }
        }
        #endregion
        // =============================================================================

        #region Eleves
        public async Task UpdateListeEleveByIdClassroom(int idClassroom)
        {

            try
            {
                ListEleves = new ObservableCollection<Eleve>(
                    await ApiManager.Instance.GetListElevesByIdClassroom(idClassroom));
            }
            catch (Exception ex)
            {
                AlertError.ShowErrorAlert(ex.Message);
            }
        }
        public async Task AddEleve(int idClassroom, EleveAdded e)
        {
            try
            {
                Eleve eleve = await ApiManager.Instance.AddEleveByIdClassroom(idClassroom, e);
                ListEleves.Add(eleve);
            }
            catch (Exception ex)
            {
                AlertError.ShowErrorAlert(ex.Message);
            }
        }

        public async Task DeleteEleve(int id)
        {
            try
            {
                Eleve eleve = await ApiManager.Instance.DeleteEleveById(id);
                ListEleves.Remove(eleve);
            }
            catch (Exception ex)
            {
                AlertError.ShowErrorAlert(ex.Message);
            }
        }
        #endregion
        // =============================================================================
        #region Matieres

        public async Task<ObservableCollection<Matiere>> GetListMatieresByIdClassroom(int idClassroom)
        {

            try
            {
                return new ObservableCollection<Matiere>(
                    await ApiManager.Instance.GetListMatieresByIdClassroom(idClassroom));
            }
            catch (Exception ex)
            {
                AlertError.ShowErrorAlert(ex.Message);
                return null;
            }
        }
        public async Task<Matiere> AddMatiere(int idClassroom, MatiereAdded e)
        {
            try
            {
                return await ApiManager.Instance.AddMatiereByIdClassroom(idClassroom, e);
            }
            catch (Exception ex)
            {
                AlertError.ShowErrorAlert(ex.Message);
                return null;
            }
        }

        public async Task<Matiere> DeleteMatiere(int id)
        {
            try
            {
               return await ApiManager.Instance.DeleteMatiereById(id);
            }
            catch (Exception ex)
            {
                AlertError.ShowErrorAlert(ex.Message);
                return null;
            }
        }
        #endregion
        // =============================================================================
        #region INotify
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
