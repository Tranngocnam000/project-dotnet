namespace Phone.ViewModels;

public class MobileEditRequest
{
    public int Id { get; set; }
    public string? Name { get; set; }
     public decimal Price { get; set; }
    public string? Description {get; set; }
    public string? ImageUrl {get; set; }
    public IFormFile? ImageFile {get; set; }
    public int CategoryId { get; set; }
}