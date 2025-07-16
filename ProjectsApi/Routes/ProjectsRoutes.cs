using ProjectsApplication;
using ProjectsApplication.Models;

namespace ProjectsApi.Routes;

public static class ProjectsRoutes
{
    public static IEndpointRouteBuilder MapProjectsRoutes(this IEndpointRouteBuilder builder)
    {
        builder.MapGet("/popularIndicators/{subscriptionType:int}", async (int subscriptionType, ProjectQueriesService queriesService) =>
        {
            return Results.Ok(await queriesService.GetMostUsedIndecatorsAsync(subscriptionType));
        });

        builder.MapPut("/projects/{userId:int}", async (int userId, ProjectCreateRequestModel model, ProjectCreatorService creatorService) =>
        {
            await creatorService.CreateAsync(userId, model);
            return Results.Ok();
        });
        
        return builder;
    }
}