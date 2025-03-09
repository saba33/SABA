using SABA.Core.Abstractions;
using SABA.Core.Models.Enums;
using SABA.Core.Models.ProductModel;
using SABA.Services.Abstractiuons.Filtration;
using SABA.Services.Models.Enum;

namespace SABA.Services.Implementations.Filtration
{
    public class FilterProductService : IFilterProductService
    {
        private readonly IFilterProductRepository _filterRepo;
        public FilterProductService(IFilterProductRepository filterRepo)
        {
            _filterRepo = filterRepo;
        }

        public async Task<List<Product>> FilterByProductNameService(string productName)
        {
            return await _filterRepo.FilterByProductName(productName);
        }

        public async Task<List<Product>> FilterByProductProductCodeService(string productCode)
        {
            return await _filterRepo.FilterByProductProductCode(productCode);
        }

        public async Task<List<Product>> FilterByProductProductTypeService(ProductType productType)
        {
            return await _filterRepo.FilterByProductProductType(((int)productType));
        }

        public async Task<List<Product>> FilterProductByPriceService(decimal priceFrom, decimal priceTo)
        {
            return await _filterRepo.FilterProductByPrice(priceFrom, priceTo);
        }

        public async Task<List<Product>> FilterProductsService(string name, decimal? from, decimal? to, string productCode, ProductTypes? productType, string search)
        {
            var query = _filterRepo.GetProductsQueryable(); // Assuming _filterRepo is your repository for querying products.

            // Apply search filter if search parameter is provided
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(p =>
                    p.ProductName.Contains(search) ||
                    p.ProductType.ToString().Contains(search) ||
                    p.ProductCode.Contains(search)); // Assuming ProductCode is another field to search by.
            }

            // Apply name filter if provided
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(p => p.ProductName.Contains(name));
            }

            // Apply price range filter if from or to is provided
            if (from.HasValue)
            {
                query = query.Where(p => p.UnitPrice >= from.Value);
            }
            if (to.HasValue)
            {
                query = query.Where(p => p.UnitPrice <= to.Value);
            }

            // Apply productCode filter if provided
            if (!string.IsNullOrEmpty(productCode))
            {
                query = query.Where(p => p.ProductCode.Contains(productCode));
            }

            // Apply productType filter if provided
            if (productType.HasValue)
            {
                query = query.Where(p => p.ProductType == productType.Value);
            }

            // Execute the query and return the result as a list
            return query.ToList();
        }
    }
}
