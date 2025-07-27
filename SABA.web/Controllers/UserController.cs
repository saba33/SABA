using Microsoft.AspNetCore.Mvc;
using SABA.Services.Abstractiuons.User;
using SABA.Services.Models.RequestModels.User;
using SABA.Services.Models.ResponseModels.User;

namespace SABA.web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet("GetById")]
        public ActionResult<UserDto> GetUserInfoById(int Id)
        {
            try
            {
                var users = _userService.GetUserById(Id);
                return Ok(users);
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }
        }

        [HttpGet("GetAllUsers")]
        public async Task<ActionResult<List<UserDto>>> GetUserInfo()
        {
            try
            {
                var resut = await _userService.GetAllUsers();
                return Ok(resut);
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }


        [HttpPut("UpdateUserInfo")]
        public async Task<ActionResult<UserDto>> UpdateUserInfo(int Id, UserDto userDto)
        {
            try
            {
                var result = await _userService.EditUserInfo(userDto, Id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }

        [HttpDelete("DeleteUserById")]
        public ActionResult<RemoveUserResponse> DeleteUserInfo(int Id)
        {
            try
            {
                var result = _userService.RemoveUser(Id);
                return BadRequest(new RemoveUserResponse { UserId = Id, Message = "UserInfo was Deleted", StatusCode = 200 });
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }
    }
}
