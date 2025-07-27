using AutoMapper;
using SABA.Core.Models.ProductModel;
using SABA.Core.Models.SaleModel;
using SABA.Core.Models.UserModel;
using SABA.Services.Models.RequestModels.Products;
using SABA.Services.Models.RequestModels.Sales;
using SABA.Services.Models.RequestModels.User;
using SABA.Services.Models.ResponseModels.ProductsAndSales;

namespace SABA.Services.Infrastructure.Mappings
{
    public class MappingInitializer : Profile
    {
        public MappingInitializer()
        {
            CreateMap<UserDto, User>().ReverseMap();
            CreateMap<SaleDto, Sale>().ReverseMap();
            CreateMap<ProductImageDto, ProductImage>().ReverseMap();
            CreateMap<ProductDto, Product>()
             .ForMember(dest => dest.Images, opt => opt.Ignore())
             .ForMember(dest => dest.ProductId, opt => opt.Ignore());
            CreateMap<Product, ProductDto>();
            CreateMap<Product, ProductResponseDto>()
          .ForMember(
              dest => dest.Images,
              opt => opt.MapFrom(src =>
                  src.Images.Select(image =>
                      $"data:{image.ContentType};base64,{Convert.ToBase64String(image.ImageData)}"
                  ).ToList())
          );
        }
    }
}
