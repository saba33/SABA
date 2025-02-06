using AutoMapper;
using Microsoft.AspNetCore.Http;
using SABA.Core.Abstractions;
using SABA.Services.Abstractiuons.Recomentations;
using SABA.Services.Abstractiuons.User;
using SABA.Services.Models.RequestModels.User;
using SABA.Services.Models.ResponseModels.User;

namespace SABA.Services.Implementations.User
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unit;
        private readonly IRecommendationRepository _recommendationRepository;
        private readonly IRecomendationServices _recomendationServices;
        private readonly IMapper _mapper;
        public UserService(IUnitOfWork unit, IRecommendationRepository recommendationRepository, IRecomendationServices recomendationServices, IMapper mapper)
        {
            _unit = unit;
            _recommendationRepository = recommendationRepository;
            _recomendationServices = recomendationServices;
            _mapper = mapper;
        }

        public async Task<EditUserResponse> EditUserInfo(UserDto entity, int? recommenderId)
        {
            var user = await _unit.Users.GetById(recommenderId.Value);
            _mapper.Map(entity, user);
            _unit.Users.Update(user);
            await _unit.SaveAsync();
            return new EditUserResponse
            {
                Message = "Users returend Successfully",
                StatusCode = StatusCodes.Status200OK,
                UserId = user.UserId
            };
        }

        public async Task<GetUsersResponse> GetAllUsers()
        {
            var result = await _unit.Users.GetAllAsync();
            var usersToReturn = _mapper.Map<List<UserDto>>(result);
            if (usersToReturn == null)
                return new GetUsersResponse
                {
                    Users = usersToReturn,
                    Message = "Users table is empty",
                    StatusCode = StatusCodes.Status200OK
                };

            return new GetUsersResponse
            {
                Users = usersToReturn,
                Message = "Users returend Successfully",
                StatusCode = StatusCodes.Status200OK
            };
        }

        public async Task<GetUsersResponse> GetRecommendatorList()
        {
            List<UserDto> users = new List<UserDto>();

            int startingRecommenderId = 1;
            int depthLimit = 5;

            var result = await _recomendationServices.GetRecommendatorList(startingRecommenderId, depthLimit);
            foreach (var item in result)
            {
                var user = _unit.Users.GetById(item.RecommenderId);
                var userToIsert = _mapper.Map<UserDto>(user);
                users.Add(userToIsert);
            }

            return new GetUsersResponse
            {
                Message = "Recommenders Returned Successuflly",
                StatusCode = StatusCodes.Status200OK,
                Users = users
            };
        }

        public async Task<RemoveUserResponse> RemoveUser(int Id)
        {
            var user = await _unit.Users.GetById(Id);
            if (user == null)
                return new RemoveUserResponse
                {
                    Message = "User With this Id could not be found!",
                    StatusCode = StatusCodes.Status404NotFound,
                    UserId = Id
                };

            _unit.Users.Delete(user);
            await _unit.SaveAsync();

            return new RemoveUserResponse
            {
                Message = "User succe3ssfully delated",
                StatusCode = StatusCodes.Status200OK,
                UserId = Id
            };
        }
    }
}
