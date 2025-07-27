using AutoMapper;
using Microsoft.AspNetCore.Http;
using SABA.Core.Abstractions;
using SABA.Core.Models.RecomendationModel;
using SABA.Core.Models.UserModel;
using SABA.Services.Abstractiuons.Auth;
using SABA.Services.Abstractiuons.Email;
using SABA.Services.Abstractiuons.User;
using SABA.Services.Models.RequestModels;
using SABA.Services.Models.RequestModels.User;
using SABA.Services.Models.ResponseModels.Auth;
using SABA.Services.Models.ResponseModels.User;


namespace PashaBank.Services.Implementation.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IPasswordHasher _hasher;
        private readonly IUnitOfWork _unit;
        private readonly IJwtService _jwtService;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly IEmailSendService _emailSendService;
        public AuthService(IPasswordHasher hasher, IUnitOfWork unit, IJwtService jwtService, IMapper mapper/*, IUserService userService*/, IEmailSendService mail)
        {
            _hasher = hasher;
            _unit = unit;
            _jwtService = jwtService;
            _mapper = mapper;
            //_userService = userService;
            _emailSendService = mail;
        }

        public async Task<Dictionary<byte[], byte[]>> GetHashandSalt(string mail)
        {
            byte[] passHash;
            byte[] passSalt;
            var result = new Dictionary<byte[], byte[]>();
            var user = (await _unit.Users.FindAsync(p => p.Equals(mail))).FirstOrDefault();

            if (user != null)
            {
                passHash = user.PasswordHash;
                passSalt = user.PasswordSalt;
                result.Add(passHash, passSalt);
                return result;
            }

            return result;
        }

        public async Task<LoginResponse> LoginUser(LoginRequestModel request)
        {
            var user = (await _unit.Users.FindAsync(u => u.Mail == request.Mail))
               .FirstOrDefault();

            if (user == null)
            {
                return new LoginResponse { StatusCode = StatusCodes.Status400BadRequest, Token = null, Message = "მეილი ან პაროლი არასწორია გთხოვთ გადაამოწმოთ" };
            }

            if (!_hasher.VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                return new LoginResponse { StatusCode = StatusCodes.Status400BadRequest, Token = null, Message = "მეილი ან პაროლი არასწორია გთხოვთ გადაამოწმოთ" };
            }

            var token = _jwtService.GenerateToken(user.UserId.ToString(), user.Role);

            return new LoginResponse
            {
                Token = token,
                Message = "ტოკენი წარმატებით დაგენერირდა",
                StatusCode = StatusCodes.Status200OK,
                UserInfo = _mapper.Map<UserDto>(user)
            };

        }
        //public async Task<UploadImageResponse> UploadImage(IFormFile image, int userId)
        //{
        //    var user = await _unit.Users.FindUserAsync(u => u.UserId == userId);
        //    byte[] imageToInsert = await ProcessImage.ProcessImageAsync(image);
        //    user.Picture = imageToInsert;
        //    return new UploadImageResponse
        //    {
        //        Message = "Picture uploaded succesfuly",
        //        StatusCode = StatusCodes.Status200OK,
        //    };
        //}
        public async Task<AddUserResponse> RegisterUserAsync(UserDto request, int? recommenderId)
        {
            _hasher.CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
            var userToInsert = _mapper.Map<User>(request);

            userToInsert.PasswordHash = passwordHash;
            userToInsert.PasswordSalt = passwordSalt;

            if (recommenderId.HasValue)
            {
                var recommender = await _unit.Users.GetById(recommenderId.Value);
                var recommendatorToCheck = _mapper.Map<UserDto>(recommender);
                var recommendators = await _userService.GetRecommendatorList();
                if (recommendators.Users.Contains(recommendatorToCheck))
                {
                    var recommendation = new Recommendation
                    {
                        Recommender = recommender,
                        RecommendedUser = userToInsert,
                        RecommenderId = recommender.UserId
                    };
                    if (userToInsert.Recommendations == null)
                    {
                        userToInsert.Recommendations = new List<Recommendation>();
                    }
                    await _unit.Recommendations.Add(recommendation);
                    await _unit.Users.Add(userToInsert);
                    await _unit.SaveAsync();

                    return new AddUserResponse
                    {
                        Message = "Registration was Sucessfull",
                        StatusCode = StatusCodes.Status200OK
                    };
                }
                return new AddUserResponse
                {
                    Message = "Recommender has more that 5 Depth or more that 3 recommender",
                    StatusCode = StatusCodes.Status200OK
                };

            }
            await _unit.Users.Add(userToInsert);
            await _unit.SaveAsync();

            return new AddUserResponse
            {
                Message = "Registration was Sucessfull",
                StatusCode = StatusCodes.Status200OK
            };
        }

        public async Task<ResetPasswordResponse> ResetPassword(string mail)
        {
            var user = (await _unit.Users.FindAsync(u => u.Mail == mail))
               .FirstOrDefault();
            if (user == null)
            {
                return new ResetPasswordResponse()
                {
                    Message = "please enter valid mail",
                    StatusCode = StatusCodes.Status404NotFound
                };
            }
            string Password = GenerateRandomCode();
            _hasher.CreatePasswordHash(Password, out byte[] passwordHash, out byte[] passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            _unit.Users.Update(user);

            try
            {
                await _emailSendService.SendEmailAsync(user.Mail, "Password From Company (Saba)", $"your temoporary Password id {Password}. ");
                await _unit.SaveAsync();
                return new ResetPasswordResponse
                {
                    Message = "your password succesfully reset",
                    StatusCode = StatusCodes.Status200OK
                };

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        private static Random _random = new Random();

        public static string GenerateRandomCode(int length = 9)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[_random.Next(s.Length)]).ToArray());
        }
    }
}
