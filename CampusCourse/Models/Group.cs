using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CampusCourse.Models
{
    public class Group
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }

        public string CreatorId { get; set; }
        public User Creator { get; set; }
        public ICollection<Prototype> Prototypes { get; set; }
    }
}
