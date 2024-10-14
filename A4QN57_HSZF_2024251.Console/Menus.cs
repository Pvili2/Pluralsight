using A4QN57_HSZF_2024251.Application;
using ConsoleTools;
using System;
using System.Threading;

namespace A4QN57_HSZF_2024251.Console
{
    public class Menus
    {
        public static bool isLoggedIn = false;
        public static bool isAdmin = false;
        public static ConsoleMenu CreateMainMenu(string[] args, IUserService userService)
        {
            if (!isLoggedIn)
            {
                var loginSubMenu = CreateLoginSubMenu(args, userService);
                return new ConsoleMenu(args, level: 0)
                    .Add("Registration", (thisMenu) =>
                    {
                        System.Console.Clear();
                        System.Console.Write("Username: ");
                        string username = System.Console.ReadLine();
                        System.Console.Write("Password: ");
                        string password = PasswordInput();
                        System.Console.Write("Re-Password: ");
                        string repassword = PasswordInput();
                        userService.Registration(username, password, repassword);
                        System.Console.WriteLine("Submitting...");
                        Thread.Sleep(2000); 

                        System.Console.Clear();
                        CreateMainMenu(args, userService).Show();
                    })
                    .Add("Login", () =>
                    {
                        System.Console.Clear();
                        loginSubMenu.Show();
                    })
                    .Add("Exit", () => Environment.Exit(0))
                    .Configure(config =>
                    {
                        config.Selector = "-->";
                        config.Title = "Plurasight";
                        config.EnableBreadcrumb = true;
                    });
            }
            else
            {
                return new ConsoleMenu(args, level: 0)
                    .Add("Data", (thisMenu) =>
                    {
                        System.Console.Clear();
                        System.Console.WriteLine(thisMenu.CurrentItem.Name);
                        string helo = System.Console.ReadLine();
                        System.Console.WriteLine(helo);
                    })
                    .Add("Another data", () =>
                    {
                        System.Console.Clear();
                    })
                    .Add("Logout", () =>
                    {
                        System.Console.WriteLine("Logout...");
                        Thread.Sleep(3000);
                        isLoggedIn = false;
                        CreateMainMenu(args, userService).Show();
                        
                    System.Console.Clear();
                    })
                    .Add("Exit", () => Environment.Exit(0))
                    .Configure(config =>
                    {
                        config.Selector = "-->";
                        config.Title = !isAdmin ? "Plurasight - User" : "Plurasight - Admin";
                        config.EnableBreadcrumb = true;
                    });
            }
        }
        static string PasswordInput()
        {
            var pass = string.Empty;
            ConsoleKey key;
            do
            {
                var keyInfo = System.Console.ReadKey(intercept: true);
                key = keyInfo.Key;

                if (key == ConsoleKey.Backspace && pass.Length > 0)
                {
                    System.Console.Write("\b \b");
                    pass = pass[0..^1];
                }
                else if (!char.IsControl(keyInfo.KeyChar))
                {
                    System.Console.Write("*");
                    pass += keyInfo.KeyChar;
                }
            } while (key != ConsoleKey.Enter);

            return pass;
        }
        public static ConsoleMenu CreateLoginSubMenu(string[] args, IUserService userService)
        {
            return new ConsoleMenu(args, level: 1)
                .Add("Admin login", () =>
                {
                    System.Console.Clear();
                    System.Console.Write("Username: ");
                    string username = System.Console.ReadLine();
                    System.Console.Write("Password: ");
                    string password = PasswordInput();

                    isLoggedIn = userService.AdminLogin(username, password);
                    isAdmin = userService.AdminLogin(username, password);
                    Thread.Sleep(2000);
                    CreateMainMenu(args, userService).Show();
                })
                .Add("User login", () =>
                {
                    System.Console.Clear();
                    System.Console.Write("Username: ");
                    string username = System.Console.ReadLine();
                    System.Console.Write("Password: ");
                    string password = PasswordInput();
                    isLoggedIn = userService.Login(username, password);
                    isAdmin = false;
                    System.Console.WriteLine("Logging in...");
                    Thread.Sleep(2000);
                    CreateMainMenu(args, userService).Show();
                })
                .Add("Exit", (currentMenu) =>
                {
                    System.Console.Clear();
                    currentMenu.CloseMenu();
                })
                .Configure(config =>
                {
                    config.Selector = "-->";
                    config.Title = "Login";
                    config.EnableBreadcrumb = true;
                });
        }
    }
}
