using Microsoft.EntityFrameworkCore;
using Postgres;
using UseCases.Dtos;

namespace UseCases.Users;

public class UserQueriesService(UsersDbContext context)
{
    public async Task <UserResponseDto?> GetUserByIdAsync(int id)
    {
        return await context.Users
            .Where(u => u.Id == id)
            .Select(u => new UserResponseDto
            {
                Name = u.Name,
                Email = u.Email,
                SubscriptionId = u.SubscriptionId
            })
            .FirstOrDefaultAsync();
    }
}