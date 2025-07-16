using MongoDB.Driver;
using ProjectsApplication.Models;
using ProjectsDomain;

namespace ProjectsApplication;

public class UserSettingsCreatorService(IMongoClient client)
{
    public async Task CreateOrUpdateAsync(int userId, UserSettingsUpdateRequestModel model)
    {
        var collection = client.GetDatabase("Projects").GetCollection<UserSettings>("UserSettings");

        await collection.ReplaceOneAsync(x => x.UserId == userId, new UserSettings
        {
            UserId = userId,
            Language = model.Language,
            Theme = model.Theme
        }, new ReplaceOptions{IsUpsert = true});
    }
}