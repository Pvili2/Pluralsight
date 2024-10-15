using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A4QN57_HSZF_2024251.Persistence.MsSql
{
    public class LicenseDataProvider : ILicenseServiceDataProvider
    {
        private readonly AppDbContext _context;
        public LicenseDataProvider(AppDbContext context)
        {
            _context = context;
        }

       
    }
}
