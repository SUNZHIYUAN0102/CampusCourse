using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CampusCourse.Models
{
    public class Notification
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Text { get; set; }
        public DateTime Created { get; set; }

        public Guid CourseId { get; set; }
        public Course Course { get; set; }

        
    }
}
