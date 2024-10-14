using System.Security.Cryptography;
using A4QN57_HSZF_2024251.Model;

namespace A4QN57_HSZF_2024251.Persistence.MsSql
{
    public class UserDataProvider : IUserServiceDataProvider
    {
        
        private readonly AppDbContext _context;
        private readonly string _adminName = Environment.GetEnvironmentVariable("ADMIN_NAME");
        private readonly string _adminPassword = Environment.GetEnvironmentVariable("ADMIN_PASSWORD");
        public UserDataProvider(AppDbContext context)
        {
            _context = context;
        }

        public string HashPassword(string password)
        {
            byte[] salt = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);

            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            // Convert the result to a Base64 string for storage
            string hashedPassword = Convert.ToBase64String(hashBytes);
            return hashedPassword;
        }

        public static bool VerifyPassword(string enteredPassword, string storedHashPassword)
        {
            byte[] hashBytes = Convert.FromBase64String(storedHashPassword);

            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);

            var pbkdf2 = new Rfc2898DeriveBytes(enteredPassword, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);

            for (int i = 0; i < 20; i++)
            {
                if (hashBytes[i + 16] != hash[i])
                    return false;
            }

            return true;
        }

        public bool Login(string name, string password)
        {

            var user = _context.Users.Where(t => t.Name == name).Select(t => t.Password).FirstOrDefault();

            if (user != null)
            {
                return VerifyPassword(password, user);
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> Registration(string name, string password)
        {
            var newUser = new User {
                Name = name,
                Password = HashPassword(password),
                RegistrationDate = DateTime.UtcNow,
                NumberOfLicense = 0
            };
            await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AdminLogin(string name, string password)
        {
            var userPassword = _context.Users
                                .Where(t => t.Name == name)
                                .Select(t => t.Password)
                                .FirstOrDefault();
            if (userPassword != null && VerifyPassword(password, userPassword))
            {
                if (name == _adminName && userPassword == HashPassword(_adminPassword))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
