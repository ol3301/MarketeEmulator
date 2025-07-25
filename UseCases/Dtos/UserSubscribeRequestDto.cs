using System.ComponentModel.DataAnnotations;
using Domain;

namespace UseCases.Dtos;

public class UserSubscribeRequestDto
{
    [Required]
    public DateTime StartDate { get; set; }
    [Required]
    public DateTime EndDate { get; set; }
    [Required]
    public SubscriptionType SubscriptionTypeId { get; set; }
}