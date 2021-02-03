using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models.Classrooms
{
    public class Matiere
    {
        public int Id { get; set; }

        [Required]
        public string Nom { get; set; }

        public int IdClasse { get; set; }

        public bool IsActive { get; set; }
    }
}
