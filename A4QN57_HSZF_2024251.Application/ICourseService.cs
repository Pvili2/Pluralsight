﻿using A4QN57_HSZF_2024251.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A4QN57_HSZF_2024251.Application
{
    public interface ICourseService
    {
        Task<bool> CreateCourse(Course course);
    }
}
