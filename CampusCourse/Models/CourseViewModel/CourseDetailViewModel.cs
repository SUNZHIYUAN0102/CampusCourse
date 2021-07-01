using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CampusCourse.Models.CourseViewModel
{
    public class CourseDetailViewModel
    {
        public Course course { get; set; }
        public CreateScheduleViewModel createScheduleViewModel { get; set; }
        public CreateNotificationViewModel createNotificationViewModel { get; set; }
    }
}
