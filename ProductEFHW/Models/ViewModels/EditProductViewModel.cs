namespace ProductEFHW.Models.ViewModels;

public class EditProductViewModel
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int CategoryId { get; set; }
    public double Price { get; set; }
    public string? ImageUrl { get; set; }
    public List<int>? TagIds { get; set; }
}
