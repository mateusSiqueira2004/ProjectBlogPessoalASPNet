﻿using Microsoft.AspNetCore.Mvc.Testing;
using BlogPessoal;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using BlogPessoal.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.TestHost;
using WebMotions.Fake.Authentication.JwtBearer;

namespace BlogPessoalTeste.Factory
{
    public class WebAppFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {

            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<AppDbContext>));

                if (descriptor != null)
                    services.Remove(descriptor);

                services.AddDbContext<AppDbContext>(options =>{
                    options.UseInMemoryDatabase("InMemoryBlogPessoalTest");
                });

                var sp = services.BuildServiceProvider();
                using var scope = sp.CreateScope();
                using var appContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                try
                {
                    appContext.Database.EnsureCreated();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    throw;
                }
            });

            builder.UseContentRoot(".");
            builder.UseTestServer().ConfigureTestServices(collection =>{
                collection.AddAuthentication(options =>{
                    options.DefaultAuthenticateScheme = FakeJwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = FakeJwtBearerDefaults.AuthenticationScheme;
                }).AddFakeJwtBearer();
            });
            base.ConfigureWebHost(builder);
        }
    }
}
