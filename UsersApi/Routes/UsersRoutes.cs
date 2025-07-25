using UseCases.Dtos;
using UseCases.Users;

namespace UsersApi.Routes;

public static class UsersRoutes
{
    public static IEndpointRouteBuilder MapUsersRoutes(this IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup("/users");
        group.MapGet("/{id:int}", async (int id, UserQueriesService queriesService) =>
        {
            return (await queriesService.GetUserByIdAsync(id)) is { } user
                ? Results.Ok(user)
                : Results.NoContent();
        });
        
        group.MapPost("/", async (UserCreateRequestDto model, UserCreatorAppService creatorService) =>
        {
            var id = await creatorService.CreateAsync(model);
            
            return Results.Created($"/users/{id}", id);
        });

        group.MapPost("/{id:int}/subscription", async (int id, UserSubscribeRequestDto model, UserSubscriptionService subscriptionService) =>
        {
            await subscriptionService.SubscribeAsync(id, model);

            return Results.Ok();
        });
        
        group.MapPut("/{id:int}/subscription", async (int id, UserUpdateSubscriptionRequestDto model, UserSubscriptionService subscriptionService) =>
        {
            await subscriptionService.UpdateSubscriptionAsync(id, model);

            return Results.Ok();
        });

        return builder;
    }
}