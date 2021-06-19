using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CampusCourse.Models
{
    public class Prototype
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }

        public int Credits { get; set; }

        public string Language { get; set; }

        public string Requirements { get; set; }

        public string Annotation { get; set; }

        public string Material { get; set; }

        public string Literature { get; set; }
        public string CreatorId { get; set; }
        public User Creator { get; set; }
        public Guid GroupId { get; set; }
        public Group Group { get; set; }

        public ICollection<Course> Courses { get; set; }
    }
}
