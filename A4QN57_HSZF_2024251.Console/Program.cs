﻿
using A4QN57_HSZF_2024251.Persistence.MsSql;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using DotNetEnv;
using A4QN57_HSZF_2024251.Application;
using A4QN57_HSZF_2024251.Console;

Env.Load();

var host = Host.CreateDefaultBuilder()
    .ConfigureServices((services) =>
    {
        services.AddTransient<AppDbContext>();
        services.AddSingleton<ICourseServiceDataProvider, CourseDataProvider>();
        services.AddSingleton<IUserServiceDataProvider, UserDataProvider>();
        services.AddSingleton<ILicenseServiceDataProvider, LicenseDataProvider>();
        services.AddSingleton<IUserService, UserService>();
        services.AddSingleton<ICourseService, CourseService>();
        services.AddSingleton<ILicenseService, LicenseService>();
    }).Build();

await host.StartAsync();

using IServiceScope serviceScope = host.Services.CreateScope();
IServiceProvider serviceProvider = serviceScope.ServiceProvider;

var courseService = serviceProvider?.GetService<ICourseService>();
var userService = serviceProvider?.GetService<IUserService>();
var licenseService = serviceProvider?.GetService<ILicenseService>();
var mainMenu = Menus.CreateMainMenu(args, userService, courseService, licenseService);

mainMenu.Show();
