using SABA.Core.Models.ProductModel;

namespace SABA.Core.Abstractions
{
    public interface IFilterProductRepository
    {
        public Task<List<Product>> FilterProductByPrice(decimal priceFrom, decimal priceTo);
        public Task<List<Product>> FilterByProductName(string productName);
        public Task<List<Product>> FilterByProductProductCode(string productCode);
    }
}
