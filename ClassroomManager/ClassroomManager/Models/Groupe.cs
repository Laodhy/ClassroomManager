using System;
using System.Collections.Generic;
using System.Text;

namespace ClassroomManager.Models
{
    public class Groupe
    {
        public int Id { get; set; }

        public string Nom { get; set; }

        public List<int> IdsEleves { get; set; }

        public int IdClasse { get; set; }

        public Groupe()
        {
            IdsEleves = new List<int>();
        }

    }


    public class GroupeSelectable
    {
        public Groupe Groupe { get; set; }
        public bool IsSelected { get; set; }

        public GroupeSelectable(Groupe e, bool isSelected = false)
        {
            Groupe = e;
            IsSelected = isSelected;
        }
    }
}
