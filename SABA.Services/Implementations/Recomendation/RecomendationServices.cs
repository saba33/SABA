using SABA.Core.Abstractions;
using SABA.Core.Models.RecomendationModel;
using SABA.Services.Abstractiuons.Recomentations;

namespace SABA.Services.Implementations.Recomendation
{
    public class RecomendationServices : IRecomendationServices
    {
        private readonly IUnitOfWork _unit;
        public RecomendationServices(IUnitOfWork unit)
        {
            _unit = unit;
        }

        public int CountRecommendedUsers(int recommenderId)
        {
            var recommendations = _unit.Recommendations.GetRecommendationsByRecommenderId(recommenderId).Result;
            return recommendations.Count();
        }
        public async Task<List<Recommendation>> GetRecommendationsAtLevel(int recommenderId, int depthLimit, int targetLevel, int currentLevel = 1)
        {
            List<Recommendation> recommendationsToGet = new List<Recommendation>();

            if (currentLevel > depthLimit)
            {
                return recommendationsToGet;
            }

            var currentRecommendations = await _unit.Recommendations.GetRecommendationsByRecommenderId(recommenderId);

            foreach (var recommendation in currentRecommendations)
            {

                if (await this.HasAcceptableDepth(recommendation.RecommenderId, depthLimit)
                    && this.CountRecommendedUsers(recommendation.RecommenderId) <= 2)
                {
                    if (currentLevel == targetLevel)
                    {
                        recommendationsToGet.Add(recommendation);
                    }

                    await GetRecommendationsAtLevel(recommendation.RecommenderId, depthLimit, targetLevel, currentLevel + 1);
                }
            }

            return recommendationsToGet;
        }

        public async Task<List<Recommendation>> GetRecommendatorList(int recommenderId, int depthLimit)
        {
            List<Recommendation> recommendations = new List<Recommendation>();

            if (depthLimit <= 0)
            {
                return recommendations;
            }

            var currentRecommendations = await _unit.Recommendations.GetRecommendationsByRecommenderId(recommenderId);

            foreach (var recommendation in currentRecommendations)
            {
                if (await HasAcceptableDepth(recommendation.RecommendedUser.UserId, depthLimit)
                    && CountRecommendedUsers(recommendation.RecommendedUser.UserId) <= 2)
                {
                    recommendations.Add(recommendation);
                    await GetRecommendatorList(recommendation.RecommendedUser.UserId, depthLimit - 1);
                }
            }

            return recommendations;
        }

        public async Task<bool> HasAcceptableDepth(int userId, int depthLimit)
        {
            if (depthLimit <= 0)
            {
                return false;
            }

            var currentRecommendations = await _unit.Recommendations.GetRecommendationsByRecommenderId(userId);

            foreach (var recommendation in currentRecommendations)
            {
                if (recommendation.RecommendedUser != null)
                {
                    if (await HasAcceptableDepth(recommendation.RecommendedUser.UserId, depthLimit - 1))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
