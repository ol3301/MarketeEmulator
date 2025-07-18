using System.ComponentModel.DataAnnotations;

namespace UsersApi.Internal.Application.Models;

public class UserCreateRequestModel
{
    [Required]
    public string Name { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; }
}