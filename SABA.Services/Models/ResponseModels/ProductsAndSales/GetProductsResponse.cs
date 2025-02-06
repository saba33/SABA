using SABA.Services.Models.RequestModels.Products;

namespace SABA.Services.Models.ResponseModels.ProductsAndSales
{
    public class GetProductsResponse : BaseResponse
    {
        public List<ProductDto> Products { get; set; }
    }
}
