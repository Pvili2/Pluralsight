using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A4QN57_HSZF_2024251.Model
{
    public class License
    {

        public int Id { get; set; }
        public  int UserId { get; set; }
        public  int CourseId { get; set; }
        public DateTime AvailabilityDate { get; set; }
        public int LicenseUsageNumber { get; set; }
    }
}
