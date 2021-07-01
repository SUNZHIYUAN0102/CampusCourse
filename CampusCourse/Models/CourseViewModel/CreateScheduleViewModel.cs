using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CampusCourse.Models.CourseViewModel
{
    public class CreateScheduleViewModel
    {
        [Required]
        public string Day { get; set; }
        [Required]
        public string Building { get; set; }
        [Required]
        public string Class { get; set; }
        [Required]
        public DateTime StartTime { get; set; }
        [Required]
        public DateTime EndTime { get; set; }
    }
}
