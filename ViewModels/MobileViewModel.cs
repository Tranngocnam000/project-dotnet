namespace Phone.ViewModels;

public class MobileViewModel
{
    public int Id { get; set; }
    public string? Name { get; set; }
     public decimal Price { get; set; }
    public string? Description {get; set; }
    public string? ImageUrl { get; set; }
    public DateTime CreateAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string? Category {get; set; }
    public int CategoryId {get; set; }
    public string? CategoryName { get; set; }

}