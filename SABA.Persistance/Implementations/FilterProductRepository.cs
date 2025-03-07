using Microsoft.EntityFrameworkCore;
using PashaBank.Repository.Implementations;
using SABA.Core.Abstractions;
using SABA.Core.Models.ProductModel;
using SABA.Persistance.DataContext;

namespace SABA.Persistance.Implementations
{
    public class FilterProductRepository : GenericRepository<Product>, IFilterProductRepository
    {
        private readonly DatabaseContext _context;
        public FilterProductRepository(DatabaseContext context) : base(context)
        {
            _context = context;
        }
        public async Task<List<Product>> FilterByProductName(string productName)
        {
            return (List<Product>)await this.FindAsync(p => p.ProductName == productName);
        }

        public async Task<List<Product>> FilterByProductProductCode(string productCode)
        {
            return (List<Product>)await this.FindAsync(p => p.ProductCode == productCode);
        }

        public async Task<List<Product>> FilterByProductProductType(int productType)
        {
            return (List<Product>)await this.FindAsync(p => (int)p.ProductType == productType);
        }
        public IQueryable<Product> GetProductsQueryable()
        {
            return _context.Products.AsQueryable();
        }
        public async Task<List<Product>> FilterProductByPrice(decimal priceFrom, decimal priceTo)
        {
            return (List<Product>)await this.FindAsync(p => p.UnitPrice >= priceFrom && p.UnitPrice <= priceTo);
        }
    }
}
