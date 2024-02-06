using NLayer.Core.DTOs;
using NLayer.Core.Models;

namespace NLayer.Service.Mappers;

public static class CategoryMapper
{
    public static Category ToEntity(CategoryViewModel model)
    {
        return new Category
        {
            Id = model.Id,
            Name = model.Name,
            UserId = model.UserId,
        };
    }

    public static CategoryViewModel ToViewModel(Category model)
    {
        return new CategoryViewModel
        {
            Id = model.Id,
            Name = model.Name,
            UserId = model.UserId,
            Books = model.Books == null ? new List<BookViewModel>() : BookMapper.ToViewModelList(model.Books)
        };
    }

    public static Category ToEntity(CategoryCreateModel model)
    {
        return new Category
        {
            Name = model.Name,
            UserId = model.UserId

        };
    }

    public static Category ToEntity(CategoryUpdateModel model, Category entity)
    {
        entity.Name = model.Name;
        return entity;
    }

    public static List<CategoryViewModel> ToViewModelList(IEnumerable<Category> categories)
    {
        return categories.Select(ToViewModel).ToList();
    }

}
