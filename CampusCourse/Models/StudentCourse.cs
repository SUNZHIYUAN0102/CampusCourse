using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CampusCourse.Models
{
    public class StudentCourse
    {
        public string StudentId { get; set; }
        public User Student { get; set; }

        public Guid CourseId { get; set; }
        public Course Course { get; set; }
    }
}
