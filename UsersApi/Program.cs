using Microsoft.EntityFrameworkCore;
using UsersApi.Routes;
using UsersApplication;
using UsersDatabase;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<UsersDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("UsersDatabase"));
});

builder.Services.AddTransient<UserCreatorAppService>();
builder.Services.AddTransient<UserQueriesService>();
builder.Services.AddTransient<UserSubscriptionService>();

var app = builder.Build();

app.MapGroup("/api")
    .MapUsersRoutes();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    using (var scope = app.Services.CreateScope())
    {
        var db = scope.ServiceProvider.GetRequiredService<UsersDbContext>();
        db.Database.Migrate();
    }
}

app.Run();
public partial class Program { }//for integration tests