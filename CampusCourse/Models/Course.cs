using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CampusCourse.Models
{
    public class Course
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Name { get; set; }

        public int Credits { get; set; }

        public string Year { get; set; }
        public string Semester { get; set; }
        public int TotalStudents { get; set; }
        public int MinStudents { get; set; }

        public string Language { get; set; }

        public string Requirements { get; set; }

        public string Annotation { get; set; }

        public string Material { get; set; }

        public string Literature { get; set; }

        public string CreatorId { get; set; }
        public User Creator { get; set; }

        public Guid PrototypeId { get; set; }
        public Prototype Prototype { get; set; }
        public ICollection<StudentCourse> StudentCourses { get; set; }

        public ICollection<Review> Reviews { get; set; }
    }
}
