using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;
using Microsoft.EntityFrameworkCore;
using UsersDatabase;

namespace UsersTests;

public class AutoMoqDataAttribute() : AutoDataAttribute(() =>
{
    var fixture = new Fixture().Customize(new AutoMoqCustomization());

    var dbContext = new UsersDbContext(new DbContextOptionsBuilder<UsersDbContext>()
        .UseInMemoryDatabase(Guid.NewGuid().ToString())
        .Options);

    fixture.Inject(dbContext);

    return fixture;
})
{
}