namespace Shopper.Domain.Models;

public class Product : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Color { get; set; }
    public decimal Price { get; set; }
    public bool HasDiscount { get; set; }
    public decimal Discount { get; set; }
    public string Supplier { get; set; }
    public string ImageLink { get; set; }

}


