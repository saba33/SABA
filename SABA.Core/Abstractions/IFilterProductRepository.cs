using SABA.Core.Models.ProductModel;

namespace SABA.Core.Abstractions
{
    public interface IFilterProductRepository : IGenericRepository<Product>
    {
        public Task<List<Product>> FilterProductByPrice(decimal priceFrom, decimal priceTo);
        public Task<List<Product>> FilterByProductName(string productName);
        public Task<List<Product>> FilterByProductProductCode(string productCode);
        public Task<List<Product>> FilterByProductProductType(int productType);
        public IQueryable<Product> GetProductsQueryable();
    }
}
