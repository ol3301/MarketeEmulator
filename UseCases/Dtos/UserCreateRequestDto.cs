using System.ComponentModel.DataAnnotations;

namespace UseCases.Dtos;

public class UserCreateRequestDto
{
    [Required]
    public string Name { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; }
}