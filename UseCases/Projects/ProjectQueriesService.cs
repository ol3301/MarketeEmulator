using Domain;
using Microsoft.EntityFrameworkCore;
using Mongo;
using MongoDB.Bson;
using MongoDB.Driver;
using Postgres;
using UseCases.Dtos;

namespace UseCases.Projects;

public class ProjectQueriesService(MongoDbContext mongo, UsersDbContext context)
{
    public async Task<MostUsedIndicatorsResponseDto> GetMostUsedIndecatorsAsync(int subscriptionType)
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
        
        var results = await mongo.Projects.Aggregate()
            .Match(x => userIds.Contains(x.UserId))
            .Unwind("Charts")
            .Unwind("Charts.Indicators")
            .Group(new BsonDocument
            {
                { "_id", "$Charts.Indicators.Name" },
                { "Count", new BsonDocument("$sum", 1) }
            })
            .ToListAsync();

        return new MostUsedIndicatorsResponseDto
        {
            Indicators = results.Select(x => new MostUsedIndicatorEntryResponseDto
            {
                Name = x["_id"].AsString,
                Used = x["Count"].AsInt32
            }).ToList()
        };
    }
}