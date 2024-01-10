using ProductEFHW.Data;

namespace ProductEFHW.Models.ViewModels;

public class EditCategoryViewModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public EditCategoryViewModel() { }

    public EditCategoryViewModel(Category category)
    {
        Id = category.Id;
        Name = category.Name;
    }
}