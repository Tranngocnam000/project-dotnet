namespace Phone.ViewModels;

public class CategoryViewModel
{
    public int Id { get; set; }
    public string CategoryName { get; set; } = String.Empty;
    public DateTime CreateAt { get; set; }
    public DateTime UpdatedAt { get; set; }

}