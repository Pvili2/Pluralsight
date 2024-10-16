using A4QN57_HSZF_2024251.Application;
using A4QN57_HSZF_2024251.Model;
using ConsoleTools;
using System;
using System.Threading;

namespace A4QN57_HSZF_2024251.Console
{
    public class Menus
    {
        public static bool isLoggedIn = false;
        public static bool isAdmin = false;
        public static User user = null;
        public static ConsoleMenu CreateMainMenu(string[] args, IUserService userService, ICourseService courseService)
        {
            if (!isLoggedIn)
            {
                var loginSubMenu = CreateLoginSubMenu(args, userService, courseService);
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
                        CreateMainMenu(args, userService, courseService).Show();
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
                if (!isAdmin)
                {
                    return new ConsoleMenu(args, level: 0)
                       .Add("My courses", (thisMenu) =>
                       {
                           System.Console.Clear();
                           System.Console.WriteLine(thisMenu.CurrentItem.Name);
                           string helo = System.Console.ReadLine();
                           System.Console.WriteLine(helo);
                       })
                       .Add("My licences", () =>
                       {
                           System.Console.Clear();
                       })
                       .Add("Buy licences", () =>
                       {
                           System.Console.Clear();
                       })
                       .Add("Renew License", () =>
                       {
                           System.Console.Clear();
                       })
                       .Add("Logout", () =>
                       {
                           System.Console.WriteLine("Logout...");
                           Thread.Sleep(3000);
                           isLoggedIn = false;
                           CreateMainMenu(args, userService, courseService).Show();

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
                else
                {
                    return new ConsoleMenu(args, level: 0)
                        .Add("Upload new course", async (thisMenu) =>
                        {
                            System.Console.Clear();
                            System.Console.Write("Title: ");
                            string title = System.Console.ReadLine();
                            System.Console.Write("Status: ");
                            Status status = Enum.Parse<Status>(System.Console.ReadLine());
                            System.Console.Write("Hours: ");
                            int hours = int.Parse(System.Console.ReadLine());
                            System.Console.Write("Description: ");
                            string description = System.Console.ReadLine();
                            System.Console.Write("Category: ");
                            Category category = Enum.Parse<Category>(System.Console.ReadLine());
                            DateTime publicDate = DateTime.Now;
                            Course course = new Course {
                                Category = category,
                                PublicDate = publicDate,
                                Description = description,
                                HoursLength = hours,
                                Status = status,
                                Title = title,
                            };
                            await courseService.CreateCourse(course);
                            System.Console.WriteLine("Upload course...");
                            Thread.Sleep(1000);
                            System.Console.Clear();

                        })
                        .Add("Modify courses", () =>
                        {
                            System.Console.Clear();
                            courseService.GenerateCoursePickerMenu(args).Show();
                        })
                        .Add("Send license expire warning", () =>
                        {
                            System.Console.Clear();
                        })
                        .Add("Logout", () =>
                        {
                            System.Console.Clear();
                            System.Console.WriteLine("Logout...");
                            Thread.Sleep(3000);
                            isLoggedIn = false;
                            CreateMainMenu(args, userService, courseService).Show();
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
        public static ConsoleMenu CreateLoginSubMenu(string[] args, IUserService userService, ICourseService courseService)
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
                    CreateMainMenu(args, userService, courseService).Show();
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
                    CreateMainMenu(args, userService, courseService).Show();
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
