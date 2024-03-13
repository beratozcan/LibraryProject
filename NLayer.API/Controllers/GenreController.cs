using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Services;
using NLayer.Service.Exceptions;
using NLayer.Service.Mappers;

namespace NLayer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class GenreController : CustomController
    {

        private readonly IGenreService _service;
        private readonly IUserTokenService _userTokenService;

        public GenreController(IGenreService service, IUserTokenService userTokenService)
        {
            _service = service;
            _userTokenService = userTokenService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var didUserAuthenticate = _userTokenService.DidUserAuthenticate(HttpContext);

            if(didUserAuthenticate)
            {
                var accessToken = _userTokenService.TakeAccessToken(HttpContext);
                try
                { 
                    var genres = await _service.GetAllAsync(accessToken);
                    var genresModel = GenreMapper.ToViewModelList(genres);

                    return CreateActionResult(CustomResponseModel<List<GenreViewModel>>.Success(200, genresModel));

                }
                catch (NotFoundException ex)
                {
                    return NotFound($"Genre not found ");
                }
                catch (Exception ex)
                {
                    return StatusCode(500, ex.Message);
                }
            }
            return Unauthorized("User is not authenticated");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {

            var didUserAuthenticate = _userTokenService.DidUserAuthenticate(HttpContext);

            if(didUserAuthenticate)
            {
                var accessToken = _userTokenService.TakeAccessToken(HttpContext);

                try
                {
                    var genre = await _service.GetByIdAsync(id, accessToken);
                    var genreModel = GenreMapper.ToViewModel(genre);

                    return CreateActionResult(CustomResponseModel<GenreViewModel>.Success(200, genreModel));

                }
                catch (NotFoundException ex)
                {
                    return NotFound($"Genre not found: {id}");
                }
                catch (Exception ex)
                {   
                    return StatusCode(500, ex.Message);
                }
            }

            return Unauthorized("User is not authenticated");
            
        }

        [HttpPost]
        public async Task<IActionResult> Create(GenreCreateModel GenreModel)
        {

            var didUserAuthenticate = _userTokenService.DidUserAuthenticate(HttpContext);

            if (didUserAuthenticate)
            {   
                try
                {
                    var genre = await _service.AddAsync(GenreMapper.ToEntity(GenreModel));

                    var _genreModel = GenreMapper.ToViewModel(genre);

                    return CreateActionResult(CustomResponseModel<GenreViewModel>.Success(201, _genreModel));
                }
                
                catch (Exception ex)
                {
                    return StatusCode(500, ex.Message);
                }
            }
            return Unauthorized("User is not authenticated");
        }

        [HttpPut]
        public async Task<IActionResult> Update(int updatedGenreId, GenreUpdateModel genreModel)
        {
            var didUserAuthenticate = _userTokenService.DidUserAuthenticate(HttpContext);

            if(didUserAuthenticate)
            {
                var accessToken = _userTokenService.TakeAccessToken(HttpContext);
                try
                {
                    var genreEntity = await _service.GetByIdAsync(updatedGenreId, accessToken);

                    await _service.UpdateAsync(GenreMapper.ToEntity(genreModel, genreEntity));

                    return CreateActionResult(CustomResponseModel<NoContentModel>.Success(204));

                }
                catch (NotFoundException ex)
                {

                    return NotFound($"Genre not found with ID: {updatedGenreId}");
                }
                catch (Exception ex)
                {
                    return StatusCode(500, ex.Message);
                }
            }

            return Unauthorized("User is not authenticated");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var didUserAuthenticate = _userTokenService.DidUserAuthenticate(HttpContext);

            if(didUserAuthenticate)
            {
                var accessToken = _userTokenService.TakeAccessToken(HttpContext);

                try
                {
                    var entity = await _service.GetByIdAsync(id, accessToken);

                    await _service.RemoveAsync(entity);

                    return CreateActionResult(CustomResponseModel<NoContentModel>.Success(204));

                }
                catch (NotFoundException ex)
                {

                    return NotFound($"Genre not found with ID: {id}");
                }
                catch (Exception ex)
                {
                    return StatusCode(500, ex.Message);
                }
            }

            return Unauthorized("User is not authenticated");
        }
    }
}
