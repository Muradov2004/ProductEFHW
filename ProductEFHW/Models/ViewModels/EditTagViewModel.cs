using ProductEFHW.Data;

namespace ProductEFHW.Models.ViewModels;

public class EditTagViewModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public EditTagViewModel() { }
    public EditTagViewModel(Tag tag)
    {
        Id = tag.Id;
        Name = tag.Name;
    }
}
