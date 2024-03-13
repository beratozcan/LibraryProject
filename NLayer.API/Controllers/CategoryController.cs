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
    [Authorize]
    public class CategoryController : CustomController
    {
        private readonly ICategoryService _service;
        private readonly IUserService _userService;
        private readonly IUserTokenService _userTokenService;
        public CategoryController(ICategoryService service, IUserService userservice, IUserTokenService userTokenService)
        {
            _service = service;
            _userService = userservice;
            _userTokenService = userTokenService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var didUserAuthenticate = _userTokenService.DidUserAuthenticate(HttpContext);

            if(didUserAuthenticate)
            {
                var accessToken = _userTokenService.TakeAccessToken(HttpContext);

                var categories = await _service.GetAllAsync(accessToken);
                var categoriesModel = CategoryMapper.ToViewWithBooksModelList(categories);

                return CreateActionResult(CustomResponseModel<List<CategoryWithBooksViewModel>>.Success(200, categoriesModel));
            }

            return Unauthorized("User authenticate degil");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var didUserAuthenticate = _userTokenService.DidUserAuthenticate(HttpContext);

            if (didUserAuthenticate)
            {
                try
                {
                    var accessToken = _userTokenService.TakeAccessToken(HttpContext);
                    var category = await _service.GetByIdAsync(id, accessToken);
                    var categoryModel = CategoryMapper.ToViewWithBooksModel(category);

                    return CreateActionResult(CustomResponseModel<CategoryWithBooksViewModel>.Success(200, categoryModel));
                }
                catch (NotFoundException ex)
                {
                    return NotFound(ex.Message);
                }
                catch (UnauthorizedAccessException ex)
                {
                    return Unauthorized(ex.Message);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, ex.Message);
                }
            }
            else
            {
                return Unauthorized("User is not authenticated.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryCreateModel categoryModel)
        {

            var didUserAuthenticate = _userTokenService.DidUserAuthenticate(HttpContext);

            if(didUserAuthenticate)
            {
                var accessToken = _userTokenService.TakeAccessToken(HttpContext);

                var userId = _userService.GetAuthenticatedUserId(accessToken);

                var category = CategoryMapper.ToEntity(categoryModel);

                category.UserId = userId;

                await _service.AddAsync(category);

                var _categoryModel = CategoryMapper.ToViewModel(category);

                return CreateActionResult(CustomResponseModel<CategoryViewModel>.Success(201, _categoryModel));
                
            }
            return Unauthorized("User is not authenticated");


        }

        [HttpPut]
        public async Task<IActionResult> Update(CategoryUpdateModel categoryModel,int updatedCategoryId)
        {
            var didUserAuthenticate = _userTokenService.DidUserAuthenticate(HttpContext);
            if(didUserAuthenticate)
            {
                try
                {
                    var accessToken = _userTokenService.TakeAccessToken(HttpContext);
                    var categoryEntity = await _service.GetByIdAsync(updatedCategoryId, accessToken);

                    await _service.UpdateAsync(CategoryMapper.ToEntity(categoryModel, categoryEntity));

                    return CreateActionResult(CustomResponseModel<NoContentModel>.Success(204));
                }
                catch(UnauthorizedAccessException ex)
                {
                    return Unauthorized(ex.Message);
                }
                catch (NotFoundException ex)
                {
                    return NotFound(ex.Message);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, ex.Message);
                }
            }

            return Unauthorized("User is not authenticated");
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int deletedCategoryId)
        {
            var didUserAuthenticated = _userTokenService.DidUserAuthenticate(HttpContext);

            if(didUserAuthenticated)
            {
                var accessToken = _userTokenService.TakeAccessToken(HttpContext);
                try
                {
                    _service.RemoveCategory(deletedCategoryId,accessToken);

                    return CreateActionResult(CustomResponseModel<NoContentModel>.Success(204));
                }
                catch(NotFoundException ex)
                {
                    return NotFound(ex.Message);
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
