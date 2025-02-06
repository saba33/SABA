using SABA.Core.Models.UserModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SABA.Core.Models.RecomendationModel
{
    public class Recommendation
    {
        [Key]
        public int RecommendationId { get; set; }

        public int RecommenderId { get; set; }
        [ForeignKey("RecommenderId")]
        public User Recommender { get; set; }

        public int RecommendedUserId { get; set; }
        [ForeignKey("RecommendedUserId")]
        public User RecommendedUser { get; set; }
    }
}
