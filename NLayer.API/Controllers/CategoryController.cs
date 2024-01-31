
using Microsoft.AspNetCore.Mvc;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Services;
using NLayer.Service.Mapping;

namespace NLayer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : CustomController
    {
        
        private readonly IService<Category> _service;
        private readonly EntityMapper _entityMapper;

        public CategoryController(IService<Category> service, EntityMapper entityMapper)
        {
            _entityMapper = entityMapper;
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var categories = await _service.GetAllAsync();
            var categoriesModel = categories.Select(category => _entityMapper.CategoryMapEntityToModel(category)).ToList();

            return CreateActionResult(CustomResponseModel<List<CategoryModel>>.Success(200, categoriesModel));

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _service.GetByIdAsync(id);
            var categoryModel = _entityMapper.CategoryMapEntityToModel(category);
            
            return CreateActionResult(CustomResponseModel<CategoryModel>.Success(200, categoryModel));  
        }

        [HttpPost]
        public async Task<IActionResult> Save(CategoryPostModel categoryModel)
        {
            var category = await _service.AddAsync(_entityMapper.CategoryPostMapEntityToModel(categoryModel));

            var _categoryModel = _entityMapper.CategoryMapEntityToModel(category);

            return CreateActionResult(CustomResponseModel<CategoryModel>.Success(201,_categoryModel));
        }

        [HttpPut]
        public async Task<IActionResult> Update(CategoryModel categoryModel)
        {

            var categoryEntity = await _service.GetByIdAsync(categoryModel.Id);

            await _service.UpdateAsync(_entityMapper.CategoryPutMapEntityToModel(categoryModel, categoryEntity));

            return CreateActionResult(CustomResponseModel<NoContentModel>.Success(204));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _service.GetByIdAsync(id);

            await _service.RemoveAsync(category);

            return CreateActionResult(CustomResponseModel<NoContentModel>.Success(204));
        }
    }
}
