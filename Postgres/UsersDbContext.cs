using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Postgres.Configuration;

namespace Postgres;

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