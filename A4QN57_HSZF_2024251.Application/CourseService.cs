﻿using A4QN57_HSZF_2024251.Model;
using A4QN57_HSZF_2024251.Persistence.MsSql;
using ConsoleTools;
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

        public ConsoleMenu GenerateCoursePickerMenu(string[] args)
        {
            ConsoleMenu dynamicMenu = new ConsoleMenu(args, 1);
            List<Course> courses = _provider.GetAllCourses();

            foreach (var item in courses)
            {
                dynamicMenu.Add(item.Title, (currentMenu) => 
                {
                
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