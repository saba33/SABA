using SABA.Core.Models.Enums;
using SABA.Core.Models.ProductModel;
using SABA.Services.Models.Enum;

namespace SABA.Services.Abstractiuons.Filtration
{
    public interface IFilterProductService
    {
        public Task<List<Product>> FilterProductByPriceService(decimal priceFrom, decimal priceTo);
        public Task<List<Product>> FilterByProductNameService(string productName);
        public Task<List<Product>> FilterByProductProductCodeService(string productCode);
        public Task<List<Product>> FilterByProductProductTypeService(ProductType productType);
        public Task<List<Product>> FilterProductsService(string name, decimal? from, decimal? to, string productCode, ProductTypes? productType, string search);
        public Task<List<Product>> GetAll();

    }
}