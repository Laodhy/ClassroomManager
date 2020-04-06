using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models.Classrooms
{
    public class Classroom
    {
        public int Id { get; set; }

        [StringLength(60, MinimumLength = 3)]
        public string Nom { get; set; }

        // user ID 
        public long OwnerID { get; set; }

        //public ICollection<Eleve> Eleves { get; set; }
    }
}
