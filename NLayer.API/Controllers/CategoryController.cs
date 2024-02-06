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
        
        public CategoryController(ICategoryService service)
        {
            
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var categories = await _service.GetSoftRemovedAllAsync();
            var categoriesModel = CategoryMapper.ToViewModelList(categories);

            return CreateActionResult(CustomResponseModel<List<CategoryViewModel>>.Success(200, categoriesModel));

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _service.GetByIdAsync(id);
            var categoryModel = CategoryMapper.ToViewModel(category);
            
            return CreateActionResult(CustomResponseModel<CategoryViewModel>.Success(200, categoryModel));  
        }

        [HttpPost]
        public async Task<IActionResult> Save(CategoryCreateModel categoryModel)
        {
            var category = await _service.AddAsync(CategoryMapper.ToEntity(categoryModel));

            var _categoryModel = CategoryMapper.ToViewModel(category);

            return CreateActionResult(CustomResponseModel<CategoryViewModel>.Success(201,_categoryModel));
        }

        [HttpPut]
        public async Task<IActionResult> Update(int id,CategoryUpdateModel categoryModel)
        {

            var categoryEntity = await _service.GetByIdAsync(id);

            await _service.UpdateAsync(CategoryMapper.ToEntity(categoryModel, categoryEntity));

            return CreateActionResult(CustomResponseModel<NoContentModel>.Success(204));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            

            await _service.SoftDeleteAsync(id);

            return CreateActionResult(CustomResponseModel<NoContentModel>.Success(204));
        }

        [HttpGet("GetCategoryWithBooks")]

        public async Task<IActionResult> GetCategoryWithBooks()
        {
            var categoriesWithBooks = await _service.GetCategoryWithBooksAsync();
            var categories = CategoryMapper.ToViewModelList(categoriesWithBooks);

            return CreateActionResult(CustomResponseModel<List<CategoryViewModel>>.Success(200, categories));
        }
    }
}
