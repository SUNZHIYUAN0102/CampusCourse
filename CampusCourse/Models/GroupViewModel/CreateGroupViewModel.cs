using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CampusCourse.Models.GroupViewModel
{
    public class CreateGroupViewModel
    {
        [Required]
        public string Name { get; set; }
    }
}
