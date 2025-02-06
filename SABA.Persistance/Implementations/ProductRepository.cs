using PashaBank.Repository.Implementations;
using SABA.Core.Abstractions;
using SABA.Core.Models.ProductModel;
using SABA.Persistance.DataContext;

namespace SABA.Persistance.Implementations
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(DatabaseContext context) : base(context)
        {

        }
    }
}
