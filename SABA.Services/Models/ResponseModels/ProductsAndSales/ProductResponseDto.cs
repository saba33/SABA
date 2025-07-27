using SABA.Core.Models.Enums;

namespace SABA.Services.Models.ResponseModels.ProductsAndSales
{
    public class ProductResponseDto
    {
        public int ProductId { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public ProductTypes ProductType { get; set; }
        public string? ProductSize { get; set; }
        public List<string> Images { get; set; }
    }
}
