using SABA.Core.Models.ProductModel;
using SABA.Core.Models.UserModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SABA.Core.Models.SaleModel
{
    public class Sale
    {
        [Key]
        public int SaleId { get; set; }

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User Distributor { get; set; }

        public DateTime SaleDate { get; set; }
        public decimal SalePrice { get; set; }
        public ICollection<Product> SoldProducts { get; set; }
    }
}
