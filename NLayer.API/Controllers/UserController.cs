
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLayer.Core.DTOs;
using NLayer.Core.Services;
using NLayer.Service.Exceptions;
using NLayer.Service.Mappers;

namespace NLayer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : CustomController
    {

        private readonly IUserService _service;
        private readonly IUserTokenService _userTokenService;

        public UserController(IUserService service, IUserTokenService userTokenService)
        {
            _service = service;
            _userTokenService = userTokenService;
        }

        [Authorize]
        [HttpGet]

        public async Task<IActionResult> GetAll()
        {
            var didUserAuthenticated = _userTokenService.DidUserAuthenticate(HttpContext);

            if (didUserAuthenticated)
            {
                var accessToken = _userTokenService.TakeAccessToken(HttpContext);
                
                try
                {
                    var users = await _service.GetAllAsync(accessToken);
                    var usersModel = UserMapper.ToViewModelList(users);

                    return CreateActionResult(CustomResponseModel<List<UserViewModel>>.Success(200, usersModel));

                }
                catch (Exception ex)
                {
                    return StatusCode(500, ex.Message);
                }
            }

            return Unauthorized("User is not authenticated");
        }

        [Authorize]
        [HttpGet("{id}")]

        public async Task<IActionResult> GetById(int id)
        {
            var didUserAuthenticated = _userTokenService.DidUserAuthenticate(HttpContext);

            if(didUserAuthenticated)
            {
                var accessToken = _userTokenService.TakeAccessToken(HttpContext);

                try
                {
                    var user = await _service.GetByIdAsync(id, accessToken);
                    var userModel = UserMapper.ToViewModel(user);
                    return CreateActionResult(CustomResponseModel<UserViewModel>.Success(200, userModel));
                }
                catch (UnauthorizedAccessException ex)
                {
                    return Unauthorized(ex.Message);
                }
            }

            return Unauthorized("User is not authenticated");
        }

        [HttpPost]

        public async Task<IActionResult> Create(UserCreateModel model)
        {
            string username = model.UserName;
            string password = model.Password;

            _service.CreateUser(username, password);
            return CreateActionResult(CustomResponseModel<NoContentModel>.Success(204));
        }

       [Authorize]
       [HttpPut]

        public async Task<IActionResult> Update(UserUpdateModel model)
        {
            var didUserAuthenticated = _userTokenService.DidUserAuthenticate(HttpContext);
            var token = _userTokenService.TakeAccessToken(HttpContext);
            
            if(didUserAuthenticated)
            {
                var userId = _service.GetAuthenticatedUserId(token);
                try
                {
                    _service.UpdateUser(userId, model.UserName, model.Password);
                    return CreateActionResult(CustomResponseModel<NoContentModel>.Success(204));
                }

                catch (UnauthorizedAccessException ex)
                {
                    return Unauthorized(ex.Message);
                }
            }

            return Unauthorized("User is not authenticated");
        }

        [Authorize]
        [HttpDelete("{id}")]

        public async Task<IActionResult> Remove(int id)
        {
            var didUserAuthenticated = _userTokenService.DidUserAuthenticate(HttpContext);

            if (didUserAuthenticated)
            {
                var token = _userTokenService.TakeAccessToken(HttpContext);
                try
                {
                    _service.RemoveUser(id, token);
                    return CreateActionResult(CustomResponseModel<NoContentModel>.Success(204));
                }
                catch (UnauthorizedAccessException ex)
                {
                    return Unauthorized(ex.Message);
                }
            }
            return Unauthorized("User is not authenticated");
        }
    }
}

        