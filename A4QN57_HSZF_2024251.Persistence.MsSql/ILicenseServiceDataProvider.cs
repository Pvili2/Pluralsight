using System;
using System.Collections.Generic;
using A4QN57_HSZF_2024251.Model;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A4QN57_HSZF_2024251.Persistence.MsSql
{
    public interface ILicenseServiceDataProvider
    {
        Task<bool> CreateLicense(License license);
        public List<License> GetLicenseByUserId(int id);
    }
}
