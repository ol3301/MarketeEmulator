using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Driver;
using ProjectsApplication.Models;
using ProjectsDomain;
using SharedDomain;
using UsersDatabase;

namespace ProjectsApplication;

public class ProjectQueriesService(IMongoClient client, UsersDbContext context)
{
    public async Task<MostUsedIndicatorsResponseViewModel> GetMostUsedIndecatorsAsync(int subscriptionType)
    {
        if (!Enum.IsDefined(typeof(SubscriptionType), subscriptionType))
        {
            throw new DomainException($"Invalid subscription type. {subscriptionType}");
        }
        
        var subscriptionId = (SubscriptionType)subscriptionType;

        var userIds = await context.Subscriptions
            .Where(x => x.EndDate >= DateTime.UtcNow && x.SubscriptionTypeId == subscriptionId)
            .Join(context.Users,
                subscription => subscription.Id,
                user => user.SubscriptionId,
                (subscription, user) => new { subscription, user })
            .Select(x => x.user.Id)
            .ToListAsync();
        
        var collection = client.GetDatabase("Projects").GetCollection<ProjectEntity>("Projects");

        var results = await collection.Aggregate()
            .Match(x => userIds.Contains(x.UserId))
            .Unwind("Charts")
            .Unwind("Charts.Indicators")
            .Group(new BsonDocument
            {
                { "_id", "$Charts.Indicators.Name" },
                { "Count", new BsonDocument("$sum", 1) }
            })
            .ToListAsync();

        return new MostUsedIndicatorsResponseViewModel
        {
            Indicators = results.Select(x => new MostUsedIndicatorEntryResponseViewModel
            {
                Name = x["_id"].AsString,
                Used = x["Count"].AsInt32
            }).ToList()
        };
    }
}