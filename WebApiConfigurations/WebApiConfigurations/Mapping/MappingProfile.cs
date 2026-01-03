using AutoMapper;
using WebApiConfigurations.DTOs.AuthDTO;
using WebApiConfigurations.DTOs.CategoryDTOs;
using WebApiConfigurations.DTOs.OrderDTOs;
using WebApiConfigurations.DTOs.OrderItemDTOs;
using WebApiConfigurations.DTOs.ProductDTOs;
using WebApiConfigurations.Entities;
using WebApiConfigurations.Entities.UserModel;

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

            CreateMap<CreateOrderDTO, Order>().ReverseMap();
            CreateMap<Order, GetOrderDTO>().ReverseMap();
            CreateMap<UpdateOrderDTO, Order>().ForAllMembers(opt =>
                opt.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<CreateOrderItemDTO, OrderItem>().ReverseMap();
            CreateMap<OrderItem, GetOrderItemDTO>().ReverseMap();
            CreateMap<UpdateOrderItemDTO, OrderItem>().ForAllMembers(opt => 
                opt.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<RegisterDTO, AppUser<Guid>>();

        }
    }
}
