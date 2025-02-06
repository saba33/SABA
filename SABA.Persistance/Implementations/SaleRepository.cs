using PashaBank.Repository.Implementations;
using SABA.Core.Abstractions;
using SABA.Core.Models.SaleModel;
using SABA.Persistance.DataContext;

namespace SABA.Persistance.Implementations
{
    public class SaleRepository : GenericRepository<Sale>, ISaleRepository
    {
        public SaleRepository(DatabaseContext context) : base(context)
        {

        }
    }
}
