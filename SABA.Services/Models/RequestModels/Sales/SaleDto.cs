using SABA.Services.Models.RequestModels.Products;

namespace SABA.Services.Models.RequestModels.Sales
{
    public class SaleDto
    {
        public int UserId { get; set; }
        public DateTime SaleDate { get; set; }
        public decimal SalePrice { get; set; }
        public ICollection<ProductDto> SoldProducts { get; set; }
    }
}
