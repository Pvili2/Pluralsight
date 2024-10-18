using A4QN57_HSZF_2024251.Model;
using ConsoleTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A4QN57_HSZF_2024251.Application
{
    public interface ILicenseService
    {
        Task<bool> CreateLicense(License license);
        ConsoleMenu GenerateCoursePickerMenu(ICourseService courseService, User user);
        public ConsoleMenu GenerateMyLicenseMenu(User user, ICourseService courseService);
    }
}
