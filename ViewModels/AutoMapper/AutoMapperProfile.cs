using AutoMapper;
using Phone.Data.Entities;
using Phone.ViewModels.AutoMapper;

namespace Phone.ViewModels.AutoMapper;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<CategoryCreateRequest, Category>();
        CreateMap<Category, CategoryViewModel>();
        CreateMap<MobileCreateRequest, Mobile>();
        CreateMap<Mobile, MobileEditRequest>();
        CreateMap<MobileEditRequest, Mobile>();
        CreateMap<Mobile, MobileViewModel>().ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category != null ? src.Category.CategoryName : null));
    }
}