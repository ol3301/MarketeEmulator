using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using ProjectsApi.Routes;
using ProjectsApplication;
using UsersDatabase;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IMongoClient>(x =>
{
    var connectionString = builder.Configuration.GetSection("ProjectsDatabase")["ConnectionString"];
    return new MongoClient(connectionString);
});
builder.Services.AddDbContext<UsersDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("UsersDatabase"));
});
builder.Services.AddTransient<ProjectQueriesService>();
builder.Services.AddTransient<ProjectCreatorService>();
builder.Services.AddTransient<UserSettingsCreatorService>();

var app = builder.Build();

app.MapGroup("api")
    .MapProjectsRoutes()
    .MapUserSettingsRoutes();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();

