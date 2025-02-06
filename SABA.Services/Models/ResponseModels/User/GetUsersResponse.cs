using SABA.Services.Models.RequestModels.User;

namespace SABA.Services.Models.ResponseModels.User
{
    public class GetUsersResponse : BaseResponse
    {
        public IEnumerable<UserDto> Users { get; set; }
    }
}
