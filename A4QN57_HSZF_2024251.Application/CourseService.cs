using A4QN57_HSZF_2024251.Model;
using A4QN57_HSZF_2024251.Persistence.MsSql;
using ConsoleTools;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A4QN57_HSZF_2024251.Application
{
    public class CourseService : ICourseService
    {
        private readonly ICourseServiceDataProvider _provider;
        public CourseService(ICourseServiceDataProvider provider)
        {
            _provider = provider;
        }

        public async Task<bool> CreateCourse(Course course)
        {
            bool isSucceed = await _provider.CreateCourse(course);

            return isSucceed;

        }

        public List<Course> GetCourses()
        {
            var courses = _provider.GetAllCourses();
            return courses;
        }

        public Course GetCourseById(int id)
        {
            return _provider.GetCourseById(id);
        }
        public ConsoleMenu GenerateCoursePropertyMenu(string[] args, Course course)
        {
            ConsoleMenu propertyMenu = new ConsoleMenu(args, 2);
            Type type = course.GetType();

            foreach (var item in type.GetProperties())
            {
                propertyMenu.Add(item.Name, () => 
                {
                    Console.Clear();
                    Console.Write($"Írd be az új {item.Name}-t:");
                    var value = Convert.ChangeType(Console.ReadLine(), item.PropertyType);
                    _provider.UpdateCourseProperty(course.Id, item.Name, value);
                });
            }

            propertyMenu.Add("Close", (currentMenu) =>
            {
                currentMenu.CloseMenu();
            });

            return propertyMenu;
        }

        public ConsoleMenu GenerateCoursePickerMenu(string[] args)
        {
            ConsoleMenu dynamicMenu = new ConsoleMenu(args, 1);
            List<Course> courses = _provider.GetAllCourses();

            foreach (var item in courses)
            {
                dynamicMenu.Add(item.Title, async (currentMenu) => 
                {
                    Console.Clear();
                    GenerateCoursePropertyMenu(args,item).Show();
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
