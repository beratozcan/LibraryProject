using Microsoft.AspNetCore.Mvc;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Services;
using NLayer.Service.Mapping;

namespace NLayer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : CustomController
    {
        
        private readonly IService<User> _service;
        private readonly EntityMapper _entityMapper;

        public UserController(EntityMapper entityMapper, IService<User> service)
        {
            _entityMapper = entityMapper;
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var Users = await _service.GetAllAsync();
            
            var usersModel = Users.Select(user => _entityMapper.UserMapEntitytoModel(user)).ToList();

            return CreateActionResult(CustomResponseModel<List<UserModel>>.Success(200, usersModel));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var User = await _service.GetByIdAsync(id);

            var userModel = _entityMapper.UserMapEntitytoModel(User);

            return CreateActionResult(CustomResponseModel<UserModel>.Success(201,userModel));
        }

        [HttpPost]
        public async Task<IActionResult> Save(UserPostModel userModel)
        {
            var userEntity = _entityMapper.UserMapModelToEntity(userModel);
            _service.AddAsync(userEntity);

            var _userModel = _entityMapper.UserMapEntitytoModel(userEntity);

            return CreateActionResult(CustomResponseModel<UserModel>.Success(201, _userModel));
        }

        [HttpPut]
        public async Task<IActionResult> Update(UserPutModel userModel)
        {
            var entity= await _service.GetByIdAsync(userModel.Id);

            await _service.UpdateAsync(_entityMapper.UserPutMapEntityToModel(userModel, entity));

            return CreateActionResult(CustomResponseModel<NoContentModel>.Success(204));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var user = await _service.GetByIdAsync(id);

            await _service.RemoveAsync(user);
            
            return CreateActionResult(CustomResponseModel<NoContentModel>.Success(204));
        }
    }
}
