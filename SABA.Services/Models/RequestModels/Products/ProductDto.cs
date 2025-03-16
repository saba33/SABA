using SABA.Core.Models.Enums;
using System.Text.Json.Serialization;

namespace SABA.Services.Models.RequestModels.Products
{
    public class ProductDto
    {
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public ProductTypes ProductType { get; set; }
        public string ProductSize { get; set; }
    }
}
