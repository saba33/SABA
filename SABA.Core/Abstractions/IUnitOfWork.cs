namespace SABA.Core.Abstractions
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        ISaleRepository Sales { get; }
        IRecommendationRepository Recommendations { get; }
        IProductRepository Products { get; }

        Task<int> SaveAsync();
    }
}
