using MongoDB.Driver;
using ProjectsApplication;
using ProjectsApplication.Models;
using ProjectsDomain;

namespace ProjectsApi.Routes;

public static class UserSettingsRoutes
{
    public static IEndpointRouteBuilder MapUserSettingsRoutes(this IEndpointRouteBuilder app)
    {
        app.MapGet("/usersettings/{id:int}", async (int id, IMongoClient client) =>
        {
            var collection = client.GetDatabase("Projects").GetCollection<UserSettings>("UserSettings");
            
            return Results.Ok(await collection.Find(x => x.UserId == id).FirstOrDefaultAsync());
        });
        
        app.MapPost("/usersettings/{id:int}", async (int id, UserSettingsUpdateRequestModel model, UserSettingsCreatorService settingsCreatorService) =>
        {
            await settingsCreatorService.CreateOrUpdateAsync(id, model);
            return Results.Ok();
        });
        return app;
    }
}