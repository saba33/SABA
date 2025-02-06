using SABA.Core.Abstractions;
using SABA.Persistance.DataContext;

namespace SABA.Persistance.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext _dataContext;
        public IUserRepository Users { get; }

        public ISaleRepository Sales { get; }

        public IRecommendationRepository Recommendations { get; }

        public IProductRepository Products { get; }

        public UnitOfWork(DatabaseContext dataContext,
                        IProductRepository productsRepo,
                                ISaleRepository saleRepo,
                                IUserRepository userRepo,
                                IRecommendationRepository recommendationRepo)
        {
            _dataContext = dataContext;
            Products = productsRepo;
            Sales = saleRepo;
            Users = userRepo;
            Recommendations = recommendationRepo;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dataContext.Dispose();
            }
        }
        public async Task<int> SaveAsync()
        {
            return await _dataContext.SaveChangesAsync();
        }
    }
}
