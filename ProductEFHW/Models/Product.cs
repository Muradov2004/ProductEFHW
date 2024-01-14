namespace ProductEFHW.Models;

public class Product : BaseEntity
{
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public double Price { get; set; }
    public string? ImageUrl { get; set; }
    public Category Category { get; set; } = null!;
    public int CategoryId { get; set; }
    public List<Tag> Tags { get; set; } = null!;

}
