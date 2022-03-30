using System.ComponentModel.DataAnnotations;

namespace Shopper.Domain.Models;
public class Customer : BaseEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Gender Gender { get; set; }
    public decimal? Salary { get; set; }
    public bool? IsRemoved { get; set; }

    [Compare(nameof(ConfirmPassword))]
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
}

public enum Gender
{
    Male,
    Female
}
