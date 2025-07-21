using AutoMapper;
using itsc_dotnet_practice.Models.Dtos;

namespace itsc_dotnet_practice.Models.Mapper;

public class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateMap<OrderDto.OrderRequest, Order>()
            .ForMember(dest => dest.Id, opt => opt.Ignore()) // Id should not be mapped from request
            .ForMember(dest => dest.Status, opt => opt.Ignore()) // default status
            .ForMember(dest => dest.ShippingAddress, opt => opt.Ignore()) // default address
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore()) // handled by entity
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore()) // handled by entity
            .ForMember(dest => dest.User, opt => opt.Ignore()) // will be set by EF based on UserId
            .ForMember(dest => dest.OrderDetails, opt => opt.MapFrom(src => src.OrderDetails));
    }
}
