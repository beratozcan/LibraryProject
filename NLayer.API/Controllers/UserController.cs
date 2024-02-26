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

        public async Task<IActionResult> GetAll()
        {
            
                var users = await _service.GetAllAsync();
                var usersModel = UserMapper.ToViewModelList(users);

                return CreateActionResult(CustomResponseModel<List<UserViewModel>>.Success(200, usersModel));

        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetById(int getUserId,int id)
        {
            var didUserLogin = _service.DidUserLogin(id);

            if(didUserLogin && id == getUserId)
            {
                var user = await _service.GetByIdAsync(getUserId);
                var userModel = UserMapper.ToViewModel(user);

                return CreateActionResult(CustomResponseModel<UserViewModel>.Success(200, userModel));

            }
            else if (didUserLogin && id !=getUserId)
            {
                throw new Exception("Kullanici bu yetkiye sahip degil");
            }
            else
            {
                throw new Exception("Kullanici login degil");
            } 
        }

        [HttpPost]

        public async Task<IActionResult> Create(UserCreateModel model, int id)
        {
            var didUserLogin = _service.DidUserLogin(id);

            if(didUserLogin)
            {
                string username = model.UserName;
                string password = model.Password;

                _service.CreateUser(username, password);

                return CreateActionResult(CustomResponseModel<NoContentModel>.Success(204));

            }
            else
            {
                throw new Exception("Kullanici login degil");
            }

        } 

       [HttpPut]

        public async Task<IActionResult> Update(int updatedUserId, UserUpdateModel model, int id)
        {
            var didUserLogin = _service.DidUserLogin(id);
            if(didUserLogin && updatedUserId == id)
            {
                _service.UpdateUser(updatedUserId, model.UserName, model.Password);
                return CreateActionResult(CustomResponseModel<NoContentModel>.Success(204));
            }
            else if(didUserLogin && updatedUserId!=id)
            {
                throw new Exception("Kullanici bu yetkiye sahip degil");
            }
            else
            {
                throw new Exception("Kullanici login degil");
            }
            
        } 

        [HttpDelete("{id}")]

        public async Task<IActionResult> Remove(int deletedUserId,int id)
        {
            var didUserLogin = _service.DidUserLogin(id);

            if(didUserLogin && deletedUserId == id)
            {
                var entity = await _service.GetByIdAsync(deletedUserId);
                await _service.RemoveAsync(entity);
                return CreateActionResult(CustomResponseModel<NoContentModel>.Success(204));
            }
            else if(didUserLogin && deletedUserId!=id)
            {
                throw new Exception("Kullanici bu yetkiye sahip degil");
            }
            else
            {
                throw new Exception("Kullanici login degil");
            }
        }

        [HttpPut("UserAuthentication")]

        public async Task<IActionResult> Login(string username,string password)
        {
            if(await _service.AuthenticateUser(username, password))
            {
                return CreateActionResult(CustomResponseModel<NoContentModel>.Success(204));

            }
            else
            {
                throw new Exception("Giris basarili degil");
            }
        }   
    }
}
