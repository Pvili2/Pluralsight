using System;
using System.Collections.Generic;
using A4QN57_HSZF_2024251.Model;
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

        public async Task<bool> CreateLicense(License license)
        {
            try
            {
                await _context.Licenses.AddAsync(license);

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
    }
}
