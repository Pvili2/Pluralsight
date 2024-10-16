using A4QN57_HSZF_2024251.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A4QN57_HSZF_2024251.Persistence.MsSql
{
    public interface ICourseServiceDataProvider
    {
        List<Course> GetAllCourses();
        Task<bool> CreateCourse(Course course);
        Course GetCourseById(int id);
        void UpdateCourseProperty(int id, string propertyName, object newValue);
    }
}
