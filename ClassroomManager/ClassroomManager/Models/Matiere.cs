using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ClassroomManager.Models
{
    public class MatiereAdded {
        public string Nom { get; set; }
    }

    public class Matiere : INotifyPropertyChanged
    {
        public int Id { get; set; }

        public string Nom { get; set; }

        private bool _isActive;
        public bool IsActive
        {
            get
            {
                return _isActive;
            }
            set
            {
                _isActive = value;
                OnPropertyChanged("IsActive");
            }
        }

        public int IdClasse { get; set; }

        public Matiere()
        {
            IsActive = true;
        }

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
