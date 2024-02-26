using Microsoft.AspNetCore.Mvc;
using NLayer.Core.DTOs;
using NLayer.Core.Services;
using NLayer.Service.Mappers;

namespace NLayer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : CustomController
    {
        
        private readonly ICategoryService _service;
        private readonly IUserService _userService;
        
        public CategoryController(ICategoryService service, IUserService userservice)
        {
            
            _service = service;
            _userService = userservice;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {

                var categories = await _service.GetAllAsync();
                var categoriesModel = CategoryMapper.ToViewWithBooksModelList(categories);

                return CreateActionResult(CustomResponseModel<List<CategoryWithBooksViewModel>>.Success(200, categoriesModel));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id, int getCategoryId)
        {

            var didUserLogin = _userService.DidUserLogin(id);

            if(didUserLogin)
            {
                var category = await _service.GetByIdAsync(getCategoryId);
                var categoryModel = CategoryMapper.ToViewWithBooksModel(category);

                return CreateActionResult(CustomResponseModel<CategoryWithBooksViewModel>.Success(200, categoryModel));
            }
            else
            {
                throw new Exception("Kullanici login degil");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryCreateModel categoryModel, int id)
        {
            var didUserLogin = _userService.DidUserLogin(id);

            if(didUserLogin)
            {
                var category = await _service.AddAsync(CategoryMapper.ToEntity(categoryModel));

                var _categoryModel = CategoryMapper.ToViewModel(category);

                return CreateActionResult(CustomResponseModel<CategoryViewModel>.Success(201, _categoryModel));
            }
            else
            {
                throw new Exception("Kullanici login degil");
            }  
        }

        [HttpPut]
        public async Task<IActionResult> Update(int id, CategoryUpdateModel categoryModel,int updatedCategoryId)
        {
            var didUserLogin = _userService.DidUserLogin(id);

            if(didUserLogin)
            {
                var categoryEntity = await _service.GetByIdAsync(updatedCategoryId);

                await _service.UpdateAsync(CategoryMapper.ToEntity(categoryModel, categoryEntity));

                return CreateActionResult(CustomResponseModel<NoContentModel>.Success(204));
            }
            else
            {
                throw new Exception("Kullanici login degil");
            }    
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, int deletedCategoryId)
        {
            var didUserLogin = _userService.DidUserLogin(id);

            if (didUserLogin)
            {
                var entity = await _service.GetByIdAsync(deletedCategoryId);

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
