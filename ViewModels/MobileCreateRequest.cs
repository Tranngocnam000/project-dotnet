namespace Phone.ViewModels;

public class MobileCreateRequest
{
    public int Id { get; set; }
    public string? Name { get; set; }
     public decimal Price { get; set; }
    public string? Description {get; set; }
    public IFormFile? ImageFile {get; set; }
    public int CategoryId { get; set; }
}