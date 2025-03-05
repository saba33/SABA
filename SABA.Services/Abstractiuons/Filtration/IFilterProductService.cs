using SABA.Core.Models.ProductModel;

namespace SABA.Services.Abstractiuons.Filtration
{
    public interface IFilterProductService
    {
        public Task<List<Product>> FilterProductByPriceService(decimal priceFrom, decimal priceTo);
        public Task<List<Product>> FilterByProductNameService(string productName);
        public Task<List<Product>> FilterByProductProductCodeService(string productCode);
    }
}