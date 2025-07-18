using System.ComponentModel.DataAnnotations;
using Core;

namespace UsersApi.Internal.Application.Models;

public class UserUpdateSubscriptionRequestModel
{
    [Required]
    public DateTime StartDate { get; set; }
    [Required]
    public DateTime EndDate { get; set; }
    [Required]
    public SubscriptionType SubscriptionTypeId { get; set; }
}