using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CampusCourse.Models
{
    public class Review
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Comment { get; set; }

        public Guid CourseId { get; set; }
        public Course Course { get; set; }

        public string CreatorId { get; set; }
        public User Creator { get; set; }
    }
}
