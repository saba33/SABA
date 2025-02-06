using SABA.Core.Models.Enums;
using SABA.Core.Models.RecomendationModel;
using SABA.Core.Models.SaleModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SABA.Core.Models.UserModel
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "სახელი უნდა შეიცავდეს მაქსიმუმ 50 სიმბოლოს!")]
        public string FirstName { get; set; }
        [StringLength(50, ErrorMessage = "გვარი უნდა შეიცავდეს მაქსიმუმ 50 სიმბოლოს!")]
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public byte[]? Picture { get; set; }
        public string Mail { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        [ForeignKey("RecommenderId")]
        public int? RecommenderId { get; set; }
        public ICollection<User> RecommendedUsers { get; set; }
        public ICollection<Sale> Sales { get; set; }
        public User Recommender { get; set; }
        public string Role { get; set; } = UserRoles.User.ToString();
        public ICollection<Recommendation> Recommendations { get; set; }
    }
}
