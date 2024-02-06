using Microsoft.AspNetCore.Mvc;
using NLayer.Core.DTOs;
using NLayer.Core.Services;
using NLayer.Service.Mappers;


namespace NLayer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : CustomController
    {
        
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var Users = await _service.GetAllAsync();
            
            var usersModel = UserMapper.ToViewModelList(Users);

            return CreateActionResult(CustomResponseModel<List<UserViewModel>>.Success(200, usersModel));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var User = await _service.GetByIdAsync(id);

            var userModel = UserMapper.ToViewModel(User);

            return CreateActionResult(CustomResponseModel<UserViewModel>.Success(201,userModel));
        }

        [HttpPost]
        public async Task<IActionResult> Save(UserCreateModel userModel)
        {
            var userEntity = UserMapper.ToEntity(userModel);
            await _service.AddAsync(userEntity);

            var userViewModel = UserMapper.ToViewModel(userEntity);

            return CreateActionResult(CustomResponseModel<UserViewModel>.Success(201, userViewModel));
        }

        [HttpPut]
        public async Task<IActionResult> Update(int id,UserUpdateModel userModel)
        {
            var entity= await _service.GetByIdAsync(id);

            await _service.UpdateAsync(UserMapper.ToEntity(userModel, entity));

            return CreateActionResult(CustomResponseModel<NoContentModel>.Success(204));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var user = await _service.GetByIdAsync(id);

            await _service.RemoveAsync(user);
            
            return CreateActionResult(CustomResponseModel<NoContentModel>.Success(204));
        }

        [HttpGet("GetUsersWithBooks")]

        public async Task<IActionResult> GetUserWithBooks(int id)
        {
            var userWithBooks = await _service.GetUserWithBooks(id);
            var userModel = UserMapper.ToViewModelList(userWithBooks);
            return CreateActionResult(CustomResponseModel<List<UserViewModel>>.Success(200, userModel));

        }
    }
}
