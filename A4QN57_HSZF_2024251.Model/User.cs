using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A4QN57_HSZF_2024251.Model
{
    public class User
    {

        public int Id {get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public DateTime RegistrationDate { get; set; }
        public int NumberOfLicense { get; set; }

        public virtual ICollection<License> Licenses { get; set; }
    }
}
