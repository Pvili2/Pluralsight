using A4QN57_HSZF_2024251.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A4QN57_HSZF_2024251.Persistence.MsSql
{
    public interface IUserServiceDataProvider
    {
        User Login(string name, string password);
        User AdminLogin(string name, string password);
        Task<bool> Registration(string name, string password);

        string HashPassword(string password);
    }
}
