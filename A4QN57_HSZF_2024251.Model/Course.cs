using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A4QN57_HSZF_2024251.Model
{
    public class Course
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Category Category { get; set; }
        public int HoursLength { get; set; }
        public DateTime PublicDate { get; set; }
        public Status Status {get; set; }

        //this help the migration to understand foreign keys
        public virtual ICollection<License> Licenses { get; set; }
    }
}
