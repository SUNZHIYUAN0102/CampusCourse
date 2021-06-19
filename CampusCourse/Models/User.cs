using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CampusCourse.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ImagePath { get; set; }

        public string FullName
        {
            get { return FirstName + " " + LastName; }
        }

        public ICollection<StudentCourse> StudentCourses { get; set; }
        public ICollection<Course> Courses { get; set; }
        public ICollection<Prototype> Prototypes { get; set; }
        public ICollection<Group> Groups { get;set; }
    }
}
