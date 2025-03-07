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
            var query = _filterRepo.GetProductsQueryable();
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(p =>
                    p.ProductName.Contains(search) ||
                    p.ProductType.ToString().Contains(search) ||
                    p.ProductType.ToString().Contains(search));
            }
            return null;
        }
    }
}
