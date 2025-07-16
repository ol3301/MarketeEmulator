using UsersApplication.Models;
using UsersDatabase;
using UsersDomain;

namespace UsersApplication;

public class UserCreatorAppService(UsersDbContext context)
{
    public async Task<int> CreateAsync(UserCreateRequestModel model)
    {
        var entity = new UserEntity
        {
            Name = model.Name,
            Email = model.Email,
        };

        await context.Users.AddAsync(entity);
        await context.SaveChangesAsync();

        return entity.Id;
    }
}