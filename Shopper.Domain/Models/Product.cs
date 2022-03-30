using System.ComponentModel.DataAnnotations;

namespace Shopper.Domain.Models;

public class Product : BaseEntity
{
    [Required, StringLength(50, MinimumLength = 3, ErrorMessage = "Nazwa powinna mieć od 3 do 50 znaków")]
    public string Name { get; set; }
    public string Description { get; set; }

    [Required]
    public string Color { get; set; }

    [Required, Range(1, 100)]
    public decimal Price { get; set; }
    public bool HasDiscount { get; set; }
    public decimal? Discount { get; set; }
    public string Supplier { get; set; }
    public string ImageLink { get; set; }
    public SizeType Size { get; set; }

}

public enum SizeType
{    
    S,
    M,
    L,
    XL
}


