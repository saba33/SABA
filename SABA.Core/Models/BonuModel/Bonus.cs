using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SABA.Core.Models.BonuModel
{
    public class Bonus
    {
        [Key]
        public int BonusId { get; set; }

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public decimal TotalVolumeSalesBonus { get; set; }
        public decimal SelfSalesBonus { get; set; }
        public decimal RecommendedDistributorsBonus { get; set; }
        public decimal SecondLevelSalesBonus { get; set; }

        public DateTime BonusAssignDate { get; set; } = DateTime.Now;
    }
}
