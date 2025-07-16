using System.ComponentModel.DataAnnotations;
using SharedDomain;

namespace UsersApplication.Models;

public class UserUpdateSubscriptionRequestModel
{
    [Required]
    public DateTime StartDate { get; set; }
    [Required]
    public DateTime EndDate { get; set; }
    [Required]
    public SubscriptionType SubscriptionTypeId { get; set; }
}