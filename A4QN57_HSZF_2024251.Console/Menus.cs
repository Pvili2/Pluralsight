using ConsoleTools;
using System;
using System.Threading;

namespace A4QN57_HSZF_2024251.Console
{
    public class Menus
    {
        public static bool isLoggedIn = false;

        public static ConsoleMenu CreateMainMenu(string[] args)
        {
            if (!isLoggedIn)
            {
                var loginSubMenu = CreateLoginSubMenu(args);
                return new ConsoleMenu(args, level: 0)
                    .Add("Registration", (thisMenu) =>
                    {
                        System.Console.WriteLine("Submitting...");
                        Thread.Sleep(2000); // Simulálja a regisztráció feldolgozását
                        isLoggedIn = true;

                        // Menü frissítése a regisztráció után
                        System.Console.Clear();
                        CreateMainMenu(args).Show();
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
                        CreateMainMenu(args).Show();
                        
                    System.Console.Clear();
                    })
                    .Add("Exit", () => Environment.Exit(0))
                    .Configure(config =>
                    {
                        config.Selector = "-->";
                        config.Title = "Plurasight - Logged In";
                        config.EnableBreadcrumb = true;
                    });
            }
        }

        public static ConsoleMenu CreateLoginSubMenu(string[] args)
        {
            return new ConsoleMenu(args, level: 1)
                .Add("Admin login", () => System.Console.WriteLine("Admin login selected"))
                .Add("User login", () => System.Console.WriteLine("User login selected"))
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
