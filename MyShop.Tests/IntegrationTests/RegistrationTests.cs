using System;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using MyShop.SharedProject.DTOs;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MyShop.Infrastructure.Databases;
using Serilog;
using Xunit;
using Xunit.Abstractions;

namespace MyShop.Tests.IntegrationTests;

public class RegistrationTests : IDisposable
{
    private readonly WebApplicationFactory<Program> _app;

    public RegistrationTests(ITestOutputHelper output)
    {
        _app = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                services.AddDbContext<AppDbContext>(options =>
                {
                    options.UseSqlite("Data Source=:memory");
                });

                using var serviceProvider = services.BuildServiceProvider();
                using var serviceScope = serviceProvider.CreateScope();

                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();
                context!.Database.EnsureCreated();
            });

            builder.UseSerilog((_, config) =>
            {
                config.WriteTo.TestOutput(output);
            });
        });
    }
    
    
    
    [Fact] 
    public async Task Request_with_correct_body_responses_with_status_code_201()
    {
        const string correctPassword = "correct123";
        var dto = new RegistrationAccountDto()
        {
            Email = "correct@gmail.com",
            Name = "CorrectName",
            Password = correctPassword,
            RepeatPassword = correctPassword
        };

        var client = _app.CreateDefaultClient();
        var response = await client.PostAsJsonAsync("/account/register", dto);
        
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }
    
    [Fact] 
    public async Task Request_with_incorrect_email_responses_with_status_code_400()
    {
        const string correctPassword = "correct123";
        var dto = new RegistrationAccountDto()
        {
            Email = "correct@",
            Name = "CorrectName",
            Password = correctPassword,
            RepeatPassword = correctPassword
        };

        var client = _app.CreateDefaultClient();
        var response = await client.PostAsJsonAsync("/account/register", dto);
        
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
    
    [Fact] 
    public async Task Request_with_incorrect_password_responses_with_status_code_400()
    {
        const string correctPassword = "inco";
        var dto = new RegistrationAccountDto()
        {
            Email = "correct@email.com",
            Name = "CorrectName",
            Password = correctPassword,
            RepeatPassword = correctPassword
        };

        var client = _app.CreateDefaultClient();
        var response = await client.PostAsJsonAsync("/account/register", dto);
        
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
    
    [Fact] 
    public async Task Request_with_no_repeat_password_responses_with_status_code_400()
    {
        const string correctPassword = "correct123456";
        var dto = new RegistrationAccountDto()
        {
            Email = "correct@email.com",
            Name = "CorrectName",
            Password = correctPassword
        };

        var client = _app.CreateDefaultClient();
        var response = await client.PostAsJsonAsync("/account/register", dto);
        
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    public void Dispose()
    {
        using (var serviceScope = _app.Server.Services.CreateScope())
        {
            var context = serviceScope.ServiceProvider.GetService<AppDbContext>();
            context!.Database.EnsureDeleted();
        }
        _app.Dispose();
    }
}