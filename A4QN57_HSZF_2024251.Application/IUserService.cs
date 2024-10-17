using A4QN57_HSZF_2024251.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A4QN57_HSZF_2024251.Application
{
    public interface IUserService
    {
        User Login(string name, string password);
        User AdminLogin(string name, string password);
        void Registration(string name, string password, string repassword);
    }
}
