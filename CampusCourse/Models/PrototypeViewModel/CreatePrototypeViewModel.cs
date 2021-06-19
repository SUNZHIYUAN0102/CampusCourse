using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CampusCourse.Models.PrototypeViewModel
{
    public class CreatePrototypeViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int Credits { get; set; }
        [Required]
        public string Language { get; set; }
        [Required]
        public string Requirements { get; set; }
        [Required]
        public string Annotation { get; set; }
        [Required]
        public string Material { get; set; }
        [Required]
        public string Literature { get; set; }
    }
}
