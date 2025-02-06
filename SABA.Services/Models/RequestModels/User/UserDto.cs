using SABA.Core.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace SABA.Services.Models.RequestModels.User
{
    public class UserDto
    {
        [Required(ErrorMessage = "First name is required.")]
        [StringLength(50, ErrorMessage = "First name must be no more than 50 characters.")]
        public string FirstName { get; set; }

        [StringLength(50, ErrorMessage = "Last name must be no more than 50 characters.")]
        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Password { get; set; }
        public string Mail { get; set; }
        public Gender Gender { get; set; }
    }
}
