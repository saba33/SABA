using PashaBank.Repository.Implementations;
using SABA.Core.Abstractions;
using SABA.Core.Models.RecomendationModel;
using SABA.Persistance.DataContext;

namespace SABA.Persistance.Implementations
{
    public class RecommendationRepository : GenericRepository<Recommendation>, IRecommendationRepository
    {
        public RecommendationRepository(DatabaseContext context) : base(context)
        {

        }

        public async Task<IEnumerable<Recommendation>> GetRecommendationsByRecommenderId(int recommenderId)
        {
            return await this.FindAsync(r => r.RecommenderId == recommenderId);
        }
    }
}
