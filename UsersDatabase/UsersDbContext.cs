using Microsoft.EntityFrameworkCore;
using UsersDatabase.Configuration;
using UsersDomain;

namespace UsersDatabase;

public class UsersDbContext(DbContextOptions<UsersDbContext> options) : DbContext(options)
{
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<SubscriptionEntity> Subscriptions { get; set; }
    public DbSet<SubscriptionTypeEntity> SubscriptionTypes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
        modelBuilder.ApplyConfiguration(new SubscriptionEntityConfiguration());
        modelBuilder.ApplyConfiguration(new SubscriptionTypeEntityConfiguration());
    }
}