using Microsoft.EntityFrameworkCore;
using UsersApplication.Models;
using UsersDatabase;

namespace UsersApplication;

public class UserQueriesService(UsersDbContext context)
{
    public async Task <UserResponseViewModel?> GetUserByIdAsync(int id)
    {
        return await context.Users
            .Where(u => u.Id == id)
            .Select(u => new UserResponseViewModel
            {
                Name = u.Name,
                Email = u.Email,
                SubscriptionId = u.SubscriptionId
            })
            .FirstOrDefaultAsync();
    }
}