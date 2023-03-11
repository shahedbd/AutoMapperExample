using AutoMapper;
using D7CRUD.Models;

namespace AutoMapperDemo.MappingConfigurations
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            // Default mapping when property names are same
            CreateMap<Category, CategoryViewModel>();
            CreateMap<CategoryViewModel, Category>();
            
            //CreateMap<List<Category>, List<CategoryViewModel>>();

            // Mapping when property names are different
            //CreateMap<User, UserViewModel>()
            //    .ForMember(dest =>
            //    dest.FName,
            //    opt => opt.MapFrom(src => src.FirstName))
            //    .ForMember(dest =>
            //    dest.LName,
            //    opt => opt.MapFrom(src => src.LastName));
        }
    }
}
