using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using UsersDatabase;
using UsersDomain;

namespace UsersIntegrationTests;

public class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram>
    where TProgram : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.Remove(services.First(d => d.ServiceType == typeof(IDbContextOptionsConfiguration<UsersDbContext>)));

            services.AddDbContext<UsersDbContext>(options =>
            {
                options.UseInMemoryDatabase("TestDb");
            });

            var sp = services.BuildServiceProvider();
            using var scope = sp.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<UsersDbContext>();
            db.Database.EnsureCreated();
            db.Users.Add(new UserEntity { Id = 1, Name = "TestUser", Email = "test@test.com"});
            db.SaveChanges();
        });
    }
}
