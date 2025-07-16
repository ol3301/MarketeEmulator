using System.ComponentModel.DataAnnotations;

namespace UsersApplication.Models;

public class UserCreateRequestModel
{
    [Required]
    public string Name { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; }
}