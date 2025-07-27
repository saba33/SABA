namespace SABA.Services.Models.ResponseModels.ProductsAndSales
{
    public class GetProductsResponse : BaseResponse
    {
        public List<ProductResponseDto> Products { get; set; }
    }
}
