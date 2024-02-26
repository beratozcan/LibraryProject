using Microsoft.AspNetCore.Mvc;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Services;
using NLayer.Service.Mappers;

namespace NLayer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : CustomController
    {

        private readonly IGenreService _service;
        private readonly IUserService _userService;

        public GenreController(IGenreService service, IUserService userService)
        {
            _service = service;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
                var genres = await _service.GetAllAsync();
                var genresModel = GenreMapper.ToViewModelList(genres);

                return CreateActionResult(CustomResponseModel<List<GenreViewModel>>.Success(200, genresModel));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id, int userId)
        {
            var didUserLogin = _userService.DidUserLogin(userId);
            if(didUserLogin)
            {
                var genre = await _service.GetByIdAsync(id);
                var genreModel = GenreMapper.ToViewModel(genre);

                return CreateActionResult(CustomResponseModel<GenreViewModel>.Success(200, genreModel));
            }
            else
            {
                throw new Exception("User login degil");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(GenreCreateModel GenreModel,int id)
        {
            var didUserLogin = _userService.DidUserLogin(id);
            
            if(didUserLogin)
            {
                var genre = await _service.AddAsync(GenreMapper.ToEntity(GenreModel));

                var _genreModel = GenreMapper.ToViewModel(genre);

                return CreateActionResult(CustomResponseModel<GenreViewModel>.Success(201, _genreModel));
            }
            else
            {
                throw new Exception("Kullanici login degil");
            }
            
        }

        [HttpPut]
        public async Task<IActionResult> Update(int updatedGenreId, GenreUpdateModel genreModel, int id)
        {
            var didUserLogin = _userService.DidUserLogin(id);

            if(didUserLogin)
            {
                var genreEntity = await _service.GetByIdAsync(updatedGenreId);

                await _service.UpdateAsync(GenreMapper.ToEntity(genreModel, genreEntity));

                return CreateActionResult(CustomResponseModel<NoContentModel>.Success(204));
            }
            else
            {
                throw new Exception("Kullanici login degil");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int deletedUserId, int id)
        {
            var didUserLogin = _userService.DidUserLogin(id);
            if (didUserLogin)
            {
                var entity = await _service.GetByIdAsync(deletedUserId);

                await _service.RemoveAsync(entity);

                return CreateActionResult(CustomResponseModel<NoContentModel>.Success(204));
            }
            else
            {
                throw new Exception("Kullanici login degil");
            }
            
        }
    }
}
