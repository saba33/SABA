using AutoMapper;
using SABA.Core.Models.ProductModel;
using SABA.Core.Models.SaleModel;
using SABA.Core.Models.UserModel;
using SABA.Services.Models.RequestModels.Products;
using SABA.Services.Models.RequestModels.Sales;
using SABA.Services.Models.RequestModels.User;

namespace SABA.Services.Infrastructure.Mappings
{
    public class MappingInitializer : Profile
    {
        public MappingInitializer()
        {
            CreateMap<UserDto, User>().ReverseMap();
            CreateMap<SaleDto, Sale>().ReverseMap();
            CreateMap<ProductDto, Product>().ReverseMap();
            CreateMap<ProductImageDto, ProductImage>().ReverseMap();
        }
    }
}
