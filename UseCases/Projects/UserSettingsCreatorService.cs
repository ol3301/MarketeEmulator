using Domain.Entities;
using Mongo;
using MongoDB.Driver;
using UseCases.Dtos;

namespace UseCases.Projects;

public class UserSettingsCreatorService(MongoDbContext mongo)
{
    public async Task CreateOrUpdateAsync(int userId, UserSettingsUpdateRequestDto model)
    {
        await mongo.UserSettings.ReplaceOneAsync(x => x.UserId == userId, new UserSettings
        {
            UserId = userId,
            Language = model.Language,
            Theme = model.Theme
        }, new ReplaceOptions{IsUpsert = true});
    }
}