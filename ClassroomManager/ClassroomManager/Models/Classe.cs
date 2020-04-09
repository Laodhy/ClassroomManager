using System;
using System.Collections.Generic;
using System.Text;

namespace ClassroomManager.Models
{
    public class ClasseAdded
    {
        public string Nom { get; set; }
    }

    public class Classe
    {
        public int Id { get; set; }

        public string Nom { get; set; }

        public override bool Equals(object obj)
        {
            // Check for null  
            if (ReferenceEquals(obj, null))
                return false;
            // Check for same reference  
            if (ReferenceEquals(this, obj))
                return true;

            var objet = (Classe)obj;
            return this.Id == objet.Id;
        }
        public override int GetHashCode()
        {
            return Id ^ 7;
        }
    }
}
