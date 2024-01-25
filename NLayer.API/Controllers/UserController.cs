using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Services;

namespace NLayer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : CustomController
    {
        private readonly IMapper _mapper;
        private readonly IService<User> _service;

        public UserController(IMapper mapper, IService<User> service)
        {
            _mapper = mapper;
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var User = await _service.GetAllAsync();
            
            var userDTO = _mapper.Map<List<UserDTO>>(User.ToList());

            return CreateActionResult(CustomResponseDTO<List<UserDTO>>.Success(200, userDTO));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var User = await _service.GetByIdAsync(id);

            var userDTO = _mapper.Map<UserDTO>(User);

            return CreateActionResult(CustomResponseDTO<UserDTO>.Success(201,userDTO));
        }

        [HttpPost]
        public async Task<IActionResult> Save(UserPostDTO userDTO)
        {
            var user = await _service.AddAsync(_mapper.Map<User>(userDTO));

            var _userDTO = _mapper.Map<UserDTO>(user);

            return CreateActionResult(CustomResponseDTO<UserDTO>.Success(201, _userDTO));
        }

        [HttpPut]
        public async Task<IActionResult> Update(UserDTO userDTO)
        {
            await _service.UpdateAsync(_mapper.Map<User>(userDTO));

            return CreateActionResult(CustomResponseDTO<NoContentDTO>.Success(204));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var user = await _service.GetByIdAsync(id);

            await _service.RemoveAsync(user);
            var userDTO = _mapper.Map<UserDTO>(user);

            return CreateActionResult(CustomResponseDTO<NoContentDTO>.Success(204));
        }
    }
}
