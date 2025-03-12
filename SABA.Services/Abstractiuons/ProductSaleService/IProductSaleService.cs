using SABA.Services.Models.RequestModels.Products;
using SABA.Services.Models.RequestModels.Sales;
using SABA.Services.Models.ResponseModels.AddProductRes;
using SABA.Services.Models.ResponseModels.ProductsAndSales;
using SABA.Services.Models.ResponseModels.Sales;

namespace SABA.Services.Abstractiuons.ProductSaleService
{
    public interface IProductSaleService
    {
        Task<UpdateProductResponse> UpdateProduct(ProductDto entity, int id);
        Task<CreateSaleResponse> AddSale(SaleDto entity);
        Task<AddProductResponse> AddProduct(ProductDto entity);
        Task<GetSaleResponse> GetSalesByUserId(int userId, DateTime StartDate, DateTime EndDate);
        Task<GetProductsResponse> GetProducts();
        Task<GetProductsResponse> GetById(int id);
    }
}
