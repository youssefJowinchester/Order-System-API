using AutoMapper;
using OrderSystem.Core.Enums;
using OrderSystem.Core.Models;
using OrderSystem.DTO;

namespace OrderSystem.Helpers
{
    /// <summary>
    /// Defines the mappings between different DTOs and entities.
    /// </summary>
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<Customer, CustomerDto>().ReverseMap();

            CreateMap<ProductDto, Product>().ReverseMap();
            CreateMap<Invoice, InvoiceDto>()
             .ForMember(dest => dest.InvoiceDate, opt => opt.MapFrom(src => src.InvoiceDate.DateTime));

            CreateMap<InvoiceDto, Invoice>()
                .ForMember(dest => dest.InvoiceDate, opt => opt.MapFrom(src => new DateTimeOffset(src.InvoiceDate)));

            CreateMap<UserDto, User>()
                                     .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());




            CreateMap<CreateOrderDTO, Order>().ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems)).ReverseMap();
            CreateMap<CreateOrderItemDto, OrderItem>().ReverseMap();


            //CreateMap<Order, OrderDto>()
            //.ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer.Name))
            //.ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems.Select(oi => new OrderItemDto
            //{
            //    ProductName = oi.Product.Name,
            //    UnitPrice = oi.UnitPrice,
            //    Quantity = oi.Quantity
            //}))).ReverseMap();
            //CreateMap<OrderItem, OrderItemDto>()
            //    .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name));


            CreateMap<Order, OrderDto>()
                        .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer.Name))
                        .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems.Select(oi => new OrderItemDto
                        {
                            ProdouctId =oi.ProductId,
                            UnitPrice = oi.UnitPrice,
                            Quantity = oi.Quantity
                        })))
                        .ReverseMap();

            CreateMap<OrderItem, OrderItemDto>().ReverseMap();
                




        }
    }
}
