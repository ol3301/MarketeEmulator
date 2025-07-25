using Domain.Entities;
using MongoDB.Driver;

namespace Mongo;

public class MongoDbContext(IMongoClient client)
{
    public IMongoCollection<ProjectEntity> Projects = client.GetDatabase("Projects").GetCollection<ProjectEntity>("Projects");
    public IMongoCollection<UserSettings> UserSettings = client.GetDatabase("Projects").GetCollection<UserSettings>("UserSettings");
}