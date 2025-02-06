using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SABA.Services.Abstractiuons.Auth;
using SABA.Services.Models.RequestModels;
using SABA.Services.Models.RequestModels.User;
using SABA.Services.Models.ResponseModels.Auth;

namespace SABA.web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;
        public AuthController(IAuthService authService, IMapper mapper)
        {
            _authService = authService;
            _mapper = mapper;
        }
        [HttpPost("Login")]
        public async Task<ActionResult<LoginResponse>> Login(LoginRequestModel request)
        {
            var result = await _authService.LoginUser(request);
            return Ok(result);
        }

        [HttpPost("Register")]
        public async Task<ActionResult<RegisterResponse>> Register(UserDto request, int? recommenderId)
        {
            var result = await _authService.RegisterUserAsync(request, recommenderId);
            return Ok(result);
        }
    }
}
