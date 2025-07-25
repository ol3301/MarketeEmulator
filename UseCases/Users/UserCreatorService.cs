using Domain.Entities;
using Postgres;
using UseCases.Dtos;

namespace UseCases.Users;

public class UserCreatorAppService(UsersDbContext context)
{
    public async Task<int> CreateAsync(UserCreateRequestDto model)
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