using SABA.Core.Models.RecomendationModel;

namespace SABA.Core.Abstractions
{
    public interface IRecommendationRepository : IGenericRepository<Recommendation>
    {
        Task<IEnumerable<Recommendation>> GetRecommendationsByRecommenderId(int recommenderId);
    }
}
