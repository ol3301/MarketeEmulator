using Domain;
using Domain.Entities;
using Postgres;
using UseCases.Dtos;
using UseCases.Users;
using Xunit;

namespace UsersTests;

public class UserSubscriptionServiceTests
{
    [Theory, AutoMoqData]
    public async Task SubscribeAsyncUserNotFoundTest(UserSubscriptionService sut)
    {
        var userId = 1;
        var ex = await Assert.ThrowsAsync<DomainException>(async () => await sut.SubscribeAsync(userId, new UserSubscribeRequestDto()));
        
        Assert.Equal($"User not found. {userId}", ex.Message);
    }
    
    [Theory, AutoMoqData]
    public async Task SubscribeAsyncUserAlreadyHasActiveSubscription(UserSubscriptionService sut, UsersDbContext context, UserEntity entity)
    {
        entity.Id = 1;
        entity.Subscription = new SubscriptionEntity { EndDate = DateTime.UtcNow };
        context.Users.Add(entity);
        context.SaveChanges();
        
        var ex = await Assert.ThrowsAsync<DomainException>(async () => await sut.SubscribeAsync(1, new UserSubscribeRequestDto()));
        
        Assert.Equal($"User already has an active subscription.", ex.Message);
    }
    
    [Theory, AutoMoqData]
    public async Task SubscribeAsyncSuccessTest(UserSubscriptionService sut, UsersDbContext context, UserEntity entity, UserSubscribeRequestDto model)
    {
        entity.Id = 1;
        entity.Subscription = null;
        context.Users.Add(entity);
        context.SaveChanges();

        await sut.SubscribeAsync(1, model);

        Assert.NotNull(entity.Subscription);
        Assert.Equal(model.SubscriptionTypeId, entity.Subscription.SubscriptionTypeId);
        Assert.Equal(model.StartDate.ToUniversalTime(), entity.Subscription.StartDate);
        Assert.Equal(model.EndDate.ToUniversalTime(), entity.Subscription.EndDate);
    }
}