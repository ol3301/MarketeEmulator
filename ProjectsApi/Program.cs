using Microsoft.EntityFrameworkCore;
using Mongo;
using MongoDB.Driver;
using Postgres;
using ProjectsApi.Routes;
using UseCases.Projects;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<MongoDbContext>(x =>
{
    var connectionString = builder.Configuration.GetSection("ProjectsDatabase")["ConnectionString"];
    return new MongoDbContext(new MongoClient(connectionString));
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

