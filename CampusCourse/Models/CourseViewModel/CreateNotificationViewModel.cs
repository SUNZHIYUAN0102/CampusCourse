using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CampusCourse.Models.CourseViewModel
{
    public class CreateNotificationViewModel
    {
        [Required]
        public string text { get; set; }
    }
}
