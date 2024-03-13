using Microsoft.AspNetCore.Mvc;
using NLayer.Core.DTOs;
using NLayer.Core.Mappers;
using NLayer.Core.Models;
using NLayer.Core.Services;

namespace NLayer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : CustomController
    {
        private readonly IAuthenticateService _service;
        private readonly IUserTokenService _userTokenService;
       
        public AuthenticateController(IAuthenticateService service, IUserTokenService userTokenService)
        {
            _service = service;
            _userTokenService = userTokenService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(string username, string password)
        {
            var token = await _service.AuthenticateUser(username, password);

            var tokenModel = UserTokenMapper.ToViewModel(token);

            return CreateActionResult(CustomResponseModel<UserTokenViewModel>.Success(200, tokenModel));
        }

        [HttpPost("Logout")]
        public async Task<IActionResult> Logout()
        {
            var token = _userTokenService.TakeAccessToken(HttpContext);

            if (string.IsNullOrEmpty(token))
            {
                return BadRequest("Access token is missing");
            }

            await _userTokenService.RevokeTokenAsync(token);

            return Ok();
        }

        [HttpPost("RefreshToken")]

        public async Task<IActionResult> RefreshToken(string refreshToken)
        {
            var accessToken = await _userTokenService.RefreshTokenAsync(refreshToken);

            return Ok(accessToken);
        }
    }
}
