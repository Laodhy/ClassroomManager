using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models.Classrooms
{

    public class Groupe
    {
        public int Id { get; set; }

        [Required]
        public string Nom { get; set; }

        public List<int> IdsEleves { get; set; }

        public int IdClasse { get; set; }

        public Groupe()
        {
            IdsEleves = new List<int>();
        }

    }
}
