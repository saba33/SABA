using SABA.Core.Models.Enums;

namespace SABA.Services.Models.RequestModels.Products
{
    public class ProductDto
    {
        public int? ProductId { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public ProductTypes ProductType { get; set; }
        //public string ProductSize { get; set; }
    }
}
