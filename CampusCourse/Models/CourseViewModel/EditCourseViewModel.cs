using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CampusCourse.Models.CourseViewModel
{
    public class EditCourseViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int Credits { get; set; }
        [Required]
        public string Year { get; set; }
        [Required]
        public string Semester { get; set; }
        [Required]
        public int TotalStudents { get; set; }
        [Required]
        public int MinStudents { get; set; }
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
