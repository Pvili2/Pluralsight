﻿
using A4QN57_HSZF_2024251.Model;
using A4QN57_HSZF_2024251.Persistence.MsSql;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using DotNetEnv;
using ConsoleTools;
using System.Reflection.Metadata;

namespace A4QN57_HSZF_2024251.Console
{
    internal class Program
    {
        private static AppDbContext context = new AppDbContext();
        static async Task Main(string[] args)
        {
            Env.Load();
            
            var exampleCourse = new Course
            {
                Title = "Software Architecture",
                Description = "This course provides an in-depth overview of software architecture principles and practices.",
                Category = Category.Science,
                HoursLength = 40,
                PublicDate = DateTime.Now.AddMonths(-1),
                Status = Status.Active,
                Licenses = new List<License>()
            };

            var host = Host.CreateDefaultBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddTransient<AppDbContext>();
                    services.AddSingleton<ICourseServiceDataProvider, CourseDataProvider>();
                    services.AddSingleton<IUserServiceDataProvider, UserDataProvider>();
                }).Build();

            await host.StartAsync();

            using IServiceScope serviceScope = host.Services.CreateScope();
            IServiceProvider serviceProvider = serviceScope.ServiceProvider;

            var courseService = serviceProvider.GetService<ICourseServiceDataProvider>();
            var userService = serviceProvider.GetService<IUserServiceDataProvider>();

            await courseService.CreateCourse(exampleCourse);

            var mainMenu = Menus.CreateMainMenu(args);

            mainMenu.Show();
            System.Console.WriteLine("Press any key to shutdown!");
            System.Console.ReadKey();
        }
    }
}
