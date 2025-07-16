using System.Net;
using System.Text.Json;
using UsersApplication.Models;
using Xunit;

namespace UsersIntegrationTests;

public class UsersApiTests : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public UsersApiTests(CustomWebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetUserFound()
    {
        var response = await _client.GetAsync("/api/users/1");
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        var user = JsonSerializer.Deserialize<UserResponseViewModel>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
        
        Assert.NotNull(user);
        Assert.Equal("TestUser", user.Name);
        Assert.Equal("test@test.com", user.Email);
    }
    
    [Fact]
    public async Task GetUserNoFound()
    {
        var response = await _client.GetAsync("/api/users/2");
        
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }
}
