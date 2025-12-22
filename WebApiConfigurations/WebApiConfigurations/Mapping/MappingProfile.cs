using AutoMapper;
using WebApiConfigurations.DTOs.CategoryDTOs;
using WebApiConfigurations.DTOs.ProductDTOs;
using WebApiConfigurations.Entities;

namespace WebApiConfigurations.Mapping
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateCategoryDTO, Category>().ReverseMap();
            CreateMap<Category, GetCategoryDTO>().ReverseMap();
            CreateMap<UpdateCategoryDTO, Category>().ForAllMembers(opt =>
                opt.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<CreateProductDTO, Product>().ReverseMap();
            CreateMap<Product, GetProductDTO>().ReverseMap();
            CreateMap<UpdateProductDTO, Product>().ForAllMembers(opt =>
                opt.Condition((src, dest, srcMember) => srcMember != null));

        }
    }
}
