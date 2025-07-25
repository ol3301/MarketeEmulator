using Mongo;
using MongoDB.Driver;
using UseCases.Dtos;
using UseCases.Projects;

namespace ProjectsApi.Routes;

public static class UserSettingsRoutes
{
    public static IEndpointRouteBuilder MapUserSettingsRoutes(this IEndpointRouteBuilder app)
    {
        app.MapGet("/usersettings/{id:int}", async (int id, MongoDbContext client) =>
        {
            return Results.Ok(await client.UserSettings.Find(x => x.UserId == id).FirstOrDefaultAsync());
        });
        
        app.MapPost("/usersettings/{id:int}", async (int id, UserSettingsUpdateRequestDto model, UserSettingsCreatorService settingsCreatorService) =>
        {
            await settingsCreatorService.CreateOrUpdateAsync(id, model);
            return Results.Ok();
        });
        return app;
    }
}