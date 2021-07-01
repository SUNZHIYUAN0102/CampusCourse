using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CampusCourse.Models
{
    public class Schedule
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Day { get; set; }
        public string Building { get; set; }
        public string Class { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Guid CourseId { get; set; }
        public Course Course { get; set; }
    }
}
