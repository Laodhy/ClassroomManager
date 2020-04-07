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

        [Required]
        [StringLength(60, MinimumLength = 1)]
        public string Nom { get; set; }

        // user ID 
        public long OwnerID { get; set; }

    }
}
