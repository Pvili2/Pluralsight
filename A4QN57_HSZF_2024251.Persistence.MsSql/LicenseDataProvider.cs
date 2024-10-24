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
                if (GetLicenseByCourseId(license.CourseId) == null || GetLicenseByUserId(license.UserId) ==null )
                {
                    await _context.Licenses.AddAsync(license);
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<License> GetLicensesByUserId(int id)
        {
            List<License> result = _context.Licenses.Where(x => x.UserId == id).ToList();

            return result;
        }

        public License GetLicenseByCourseId(int id)
        {
            return _context.Licenses.Where(x => x.CourseId == id).FirstOrDefault();
        }

        public License GetLicenseByUserId(int id)
        {
            return _context.Licenses?.Where(x => x.UserId == id).FirstOrDefault() ?? throw new Exception("Null reference");
        }

        public License GetLicenseByUserAndCourseId(int userId, int courseId)
        {
            return _context.Licenses.Where(x => x.UserId == userId && x.CourseId == courseId).FirstOrDefault();
        }

        public async Task<bool> UpdateExpireMonth(License license, int month)
        {
            try
            {
                license.AvailabilityDate = license.AvailabilityDate.AddMonths(month);

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
