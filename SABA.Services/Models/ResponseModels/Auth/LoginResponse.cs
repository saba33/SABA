using SABA.Services.Models.RequestModels.User;

namespace SABA.Services.Models.ResponseModels.Auth
{
    public class LoginResponse : BaseResponse
    {
        public string Token { get; set; }
        public UserDto UserInfo { get; set; }
    }
}
