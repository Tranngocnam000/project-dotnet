using System.ComponentModel.DataAnnotations;
using Phone.Data.Interfaces;

namespace Phone.Data.Entities;

public class Category : IDateTracking
{
    public int Id {get; set; }
    [Display(Name = "Tên loại")]
    public string? CategoryName {get; set; }
    public ICollection<Mobile>? Mobiles { get; set; }
    [Display(Name = "Ngày tạo")]
    public DateTime CreateAt { get; set; }
    [Display(Name = "Ngày cập nhật")]
    public DateTime UpdatedAt { get; set; }
}