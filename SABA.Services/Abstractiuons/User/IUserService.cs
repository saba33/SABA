using SABA.Services.Models.RequestModels.User;
using SABA.Services.Models.ResponseModels.User;

namespace SABA.Services.Abstractiuons.User
{
    public interface IUserService
    {
        //Task<AddUserResponse> AddUserAsync(UserDto user, int? recommenderId);
        Task<EditUserResponse> EditUserInfo(UserDto entity, int? recommenderId);
        Task<RemoveUserResponse> RemoveUser(int Id);
        Task<GetUsersResponse> GetAllUsers();
        Task<GetUsersResponse> GetRecommendatorList();
    }
}
