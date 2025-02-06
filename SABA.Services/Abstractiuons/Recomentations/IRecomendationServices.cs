using SABA.Core.Models.RecomendationModel;

namespace SABA.Services.Abstractiuons.Recomentations
{
    public interface IRecomendationServices
    {
        int CountRecommendedUsers(int recommenderId);
        Task<bool> HasAcceptableDepth(int userId, int depthLimit);
        Task<List<Recommendation>> GetRecommendatorList(int recommenderId, int depthLimit);
        Task<List<Recommendation>> GetRecommendationsAtLevel(int recommenderId, int depthLimit, int targetLevel, int currentLevel = 1);
    }
}
