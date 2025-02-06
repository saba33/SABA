using SABA.Services.Models.RequestModels;
using SABA.Services.Models.RequestModels.User;
using SABA.Services.Models.ResponseModels.Auth;
using SABA.Services.Models.ResponseModels.User;

namespace SABA.Services.Abstractiuons.Auth
{
    public interface IAuthService
    {
        Task<AddUserResponse> RegisterUserAsync(UserDto request, int? recommenderId);
        Task<LoginResponse> LoginUser(LoginRequestModel request);
        Task<Dictionary<byte[], byte[]>> GetHashandSalt(string mail);
    }
}
