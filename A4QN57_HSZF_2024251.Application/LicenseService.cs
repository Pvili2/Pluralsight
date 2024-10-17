using A4QN57_HSZF_2024251.Model;
using A4QN57_HSZF_2024251.Persistence.MsSql;
using ConsoleTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A4QN57_HSZF_2024251.Application
{
    public class LicenseService : ILicenseService
    {
        private readonly ILicenseServiceDataProvider _provider;
        readonly int[] LICENSE_LENGHT = [1, 3, 6, 12]; //months


        public LicenseService(ILicenseServiceDataProvider provider)
        {
            _provider = provider;
        }

        public async Task<bool> CreateLicense(License license)
        {
            bool success = await _provider.CreateLicense(license);

            return success;
        }

        public ConsoleMenu GenerateLicenseLengthChooseMenu(User user, Course course)
        {
            ConsoleMenu licenseMenu = new ConsoleMenu();

            foreach (var item in LICENSE_LENGHT)
            {
                licenseMenu.Add($"{item} month access",async () =>
                {
                    await _provider.CreateLicense(new License
                    {
                        UserId = user.Id,
                        CourseId = course.Id,
                        AvailabilityDate = DateTime.Now.AddMonths(item),
                        LicenseUsageNumber = 0
                    });
                });
            }

            licenseMenu.Add("Close", (currentMenu) => currentMenu.CloseMenu());

            return licenseMenu;
        }

        public ConsoleMenu GenerateCoursePickerMenu(ICourseService courseService, User user)
        {
            ConsoleMenu dynamicMenu = new ConsoleMenu();
            List<Course> courses = courseService.GetCourses();

            foreach (var item in courses)
            {
                dynamicMenu.Add(item.Title, (currentMenu) =>
                {
                    Console.Clear();
                    GenerateLicenseLengthChooseMenu(user, item).Show();
                });
            }

            dynamicMenu.Add("Close", () =>
            {
                dynamicMenu.CloseMenu();
            });

            return dynamicMenu;
        }
    }
}
