using System.ComponentModel.DataAnnotations;

namespace SABA.Services.Models.RequestModels
{
    public class LoginRequestModel
    {
        [Required]
        public string Mail { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
