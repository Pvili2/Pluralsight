using A4QN57_HSZF_2024251.Model;
using A4QN57_HSZF_2024251.Persistence.MsSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A4QN57_HSZF_2024251.Application
{
    public class UserService : IUserService
    {
        private readonly IUserServiceDataProvider _provider;

        public UserService(IUserServiceDataProvider provider)
        {
            _provider = provider;
        }
        public User Login(string name, string password)
        {
           
            return _provider.Login(name, password);  
        }

        public async void Registration(string name, string password, string repassword)
        {

            if (password == repassword)
            {
                await _provider.Registration(name, password);
            }
            else
            {
                //szar vagy
            }
        }

        public User AdminLogin(string name, string password)
        {

            return _provider.AdminLogin(name, password);

        }
    }
}
