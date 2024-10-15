using A4QN57_HSZF_2024251.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A4QN57_HSZF_2024251.Persistence.MsSql
{
    public class CourseDataProvider : ICourseServiceDataProvider
    {
        private readonly AppDbContext _context;

        public CourseDataProvider(AppDbContext context)
        {
            _context = context;
        }
        public async Task<bool> CreateCourse(Course course)
        {
            try
            {
                await _context.Courses.AddAsync(course);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public List<Course> GetAllCourses()
        {
            var courses = _context.Courses.ToList();

            return courses;
        }
    }
}
