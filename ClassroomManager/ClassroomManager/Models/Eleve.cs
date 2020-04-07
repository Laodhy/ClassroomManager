using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ClassroomManager.Models
{
    public class Eleve
    {
        public int Id { get; set; }

        public string Nom { get; set; }

        public string Prenom { get; set; }

        public string NomAffiche
        {
            get
            {
                return Nom + " " + Prenom;
            }
        }
        public int IdClasse { get; set; }

        public Eleve()
        {

        }
    }

    public class EleveSelectable : INotifyPropertyChanged
    {
        public Eleve Eleve { get; set; }

        private bool _isSelected;
        public bool IsSelected
        {
            get
            {
                return _isSelected;
            }
            set
            {
                _isSelected = value;
                OnPropertyChanged("IsSelected");
            }
        }

        public EleveSelectable(Eleve e, bool isSelected = false)
        {
            Eleve = e;
            IsSelected = isSelected;
        }

        // =============================================================================
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
