using SABA.Services.Models.RequestModels.Sales;

namespace SABA.Services.Models.ResponseModels.ProductsAndSales
{
    public class GetSaleResponse : BaseResponse
    {
        public List<SaleDto> Sales { get; set; }
    }
}
