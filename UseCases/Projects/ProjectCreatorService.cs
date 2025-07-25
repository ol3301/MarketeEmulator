using Domain;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Mongo;
using Postgres;
using UseCases.Dtos;

namespace UseCases.Projects;

public class ProjectCreatorService(MongoDbContext mongo, UsersDbContext context)
{
    public async Task CreateAsync(int userId, ProjectCreateRequestDto model)
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
        
        await mongo.Projects.InsertOneAsync(entity);
    }
}