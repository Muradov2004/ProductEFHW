using ProductEFHW.Models.ViewModels;

namespace ProductEFHW.Models;

public class Tag : BaseEntity
{
    public string Name { get; set; } = null!;
    public List<Product>? Products { get; set; }
}
