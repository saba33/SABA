using SABA.Core.Models.Enums;
using SABA.Core.Models.SaleModel;
using System.ComponentModel.DataAnnotations;

namespace SABA.Core.Models.ProductModel
{
    public class Product
    {
        public Product()
        {
            Images = new HashSet<ProductImage>();
            Sales = new HashSet<Sale>();
        }
        [Key]
        public int ProductId { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public ProductTypes ProductType { get; set; }
        public string ProductSize { get; set; }
        public ICollection<ProductImage> Images { get; set; }
        public ICollection<Sale> Sales { get; set; }
    }
}
