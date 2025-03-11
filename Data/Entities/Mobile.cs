using System.ComponentModel.DataAnnotations;

namespace Phone.Data.Entities;

public class Mobile
{
    public int Id {get; set; }
    [Display(Name = "Tên sản phẩm")]
    public string? Name {get; set; }
    [Display(Name = "Giá")]
     public decimal Price { get; set; }
    [Display(Name = "Mô tả")]
    public string? Description {get; set; }
    [Display(Name = "Hình ảnh")]
    public string? ImageUrl {get; set; }
    [Display(Name = "Ngày tạo")]
    public DateTime CreateAt {get; set; }
    [Display(Name = "Ngày cập nhật")]
    public DateTime UpdatedAt { get; set; }
    public int CategoryId { get; set; }
    public Category? Category { get; set; }
}