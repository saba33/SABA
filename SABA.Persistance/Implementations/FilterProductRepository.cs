using SABA.Core.Abstractions;
using SABA.Core.Models.ProductModel;

namespace SABA.Persistance.Implementations
{
    public class FilterProductRepository : IFilterProductRepository
    {
        public Task<List<Product>> FilterByProductName(string productName)
        {
            throw new NotImplementedException();
        }

        public Task<List<Product>> FilterByProductProductCode(string productCode)
        {
            throw new NotImplementedException();
        }

        public Task<List<Product>> FilterProductByPrice(decimal priceFrom, decimal priceTo)
        {
            throw new NotImplementedException();
        }
    }
}
