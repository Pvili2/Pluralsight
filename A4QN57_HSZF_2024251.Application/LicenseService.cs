using A4QN57_HSZF_2024251.Model;
using A4QN57_HSZF_2024251.Persistence.MsSql;
using ConsoleTools;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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

        private int GetRemainingMonths(DateTime date1, DateTime date2) //2024, 2026
        {
            int monthsDifference = ((date2.Year - date1.Year) * 12) + date2.Month - date1.Month;

            return monthsDifference;
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
        
        public ConsoleMenu GenerateMyLicenseMenu(User user, ICourseService courseService, string chooseMenu)
        {
            if (chooseMenu == "License")
            {
                
                ConsoleMenu licenseMenu = new ConsoleMenu();
                var licenses = _provider.GetLicensesByUserId(user.Id);
                foreach (var item in licenses)
                {
                
                    var course = courseService.GetCourseById(item.CourseId);
                    licenseMenu.Add($"{course.Title}({Math.Abs(GetRemainingMonths(item.AvailabilityDate, DateTime.Now))} month remaining)", () => { });
                }

                licenseMenu.Add("Close", (currentMenu) => currentMenu.CloseMenu());
                return licenseMenu;
            }
            if(chooseMenu == "Course")
            {
                ConsoleMenu licenseMenu = new ConsoleMenu();
                var licenses = _provider.GetLicensesByUserId(user.Id);
                foreach (var item in licenses)
                {

                    var course = courseService.GetCourseById(item.CourseId);
                    licenseMenu.Add($"{course.Title}", () =>
                    {
                        Console.Clear();
                        Console.WriteLine($"{course.Description}");

                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                    });
                }

                licenseMenu.Add("Close", (currentMenu) => currentMenu.CloseMenu());
                return licenseMenu;
            }
            if (chooseMenu == "ExtendLicense")
            {
                ConsoleMenu licenseMenu = new ConsoleMenu();
                var licenses = _provider.GetLicensesByUserId(user.Id);
                foreach (var item in licenses)
                {

                    var course = courseService.GetCourseById(item.CourseId);
                    var license = _provider.GetLicenseByUserAndCourseId(user.Id, course.Id);
                    var month = Math.Abs(GetRemainingMonths(item.AvailabilityDate, DateTime.Now));
                    licenseMenu.Add($"{course.Title}({month} month remaining)", async () =>
                    {
                        Console.Clear();
                        await _provider.UpdateExpireMonth(license, month);
                    });
                }

                licenseMenu.Add("Close", (currentMenu) => currentMenu.CloseMenu());
                return licenseMenu;
            }

            return null;
        }


    }
}
