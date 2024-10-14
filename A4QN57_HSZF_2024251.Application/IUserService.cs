using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A4QN57_HSZF_2024251.Application
{
    public interface IUserService
    {
        bool Login(string name, string password);
        bool AdminLogin(string name, string password);
        void Registration(string name, string password, string repassword);
    }
}
