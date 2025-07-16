using Microsoft.EntityFrameworkCore;
using SharedDomain;
using UsersApplication.Models;
using UsersDatabase;
using UsersDomain;

namespace UsersApplication;

public class UserSubscriptionService(UsersDbContext context)
{
    public async Task SubscribeAsync(int id, UserSubscribeRequestModel model)
    {
        var user = await context.Users
            .Include(x => x.Subscription)
            .FirstOrDefaultAsync(x => x.Id == id);
            
        if (user == null)
        {
            throw new DomainException($"User not found. {id}");
        }

        if (user.Subscription != null && user.Subscription.EndDate.Date >= DateTime.UtcNow.Date)
        {
            throw new DomainException("User already has an active subscription.");
        }

        user.Subscription = new SubscriptionEntity
        {
            SubscriptionTypeId = model.SubscriptionTypeId,
            StartDate = model.StartDate.ToUniversalTime(),
            EndDate = model.EndDate.ToUniversalTime()
        };

        await context.SaveChangesAsync();
    }

    public async Task UpdateSubscriptionAsync(int id, UserUpdateSubscriptionRequestModel model)
    {
        var user = await context.Users
            .Include(x => x.Subscription)
            .FirstOrDefaultAsync(x => x.Id == id);
            
        if (user == null)
        {
            throw new DomainException($"User not found. {id}");
        }

        if (user.Subscription == null)
        {
            throw new DomainException("User does not have an subscription to update.");
        }
        
        user.Subscription.SubscriptionTypeId = model.SubscriptionTypeId;
        user.Subscription.StartDate = model.StartDate.ToUniversalTime();
        user.Subscription.EndDate = model.EndDate.ToUniversalTime();
        
        await context.SaveChangesAsync();
    }
}