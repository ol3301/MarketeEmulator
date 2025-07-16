using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using ProjectsApplication.Models;
using ProjectsDomain;
using SharedDomain;
using UsersDatabase;

namespace ProjectsApplication;

public class ProjectCreatorService(IMongoClient client, UsersDbContext context)
{
    public async Task CreateAsync(int userId, ProjectCreateRequestModel model)
    {
        if(!await context.Users.AnyAsync(x => x.Id == userId))
        {
            throw new DomainException($"User with id {userId} does not exist.");
        }
        
        var entity = new ProjectEntity
        {
            UserId = userId,
            Name = model.Name,
            Charts = model.Charts?.Select(c => new ChartEntity
            {
                Symbol = c.Symbol,
                Timeframe = c.Timeframe,
                Indicators = c.Indicators?.Select(i => new IndicatorEntity
                {
                    Name = i.Name,
                    Parameters = i.Parameters
                }).ToList()
            }).ToList()
        };
        
        await client.GetDatabase("Projects")
            .GetCollection<ProjectEntity>("Projects")
            .InsertOneAsync(entity);
    }
}