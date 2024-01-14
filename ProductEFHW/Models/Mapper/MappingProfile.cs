using AutoMapper;
using ProductEFHW.Models.ViewModels;
namespace ProductEFHW.Models.Mapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<AddProductViewModel, Product>()
            .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => new List<Tag>()));
        CreateMap<AddCategoryViewModel, Category>();
        CreateMap<AddProductViewModel, Product>();
        CreateMap<AddTagViewModel, Tag>();
    }
}
