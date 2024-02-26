using NLayer.Core.DTOs;
using NLayer.Core.Models;

namespace NLayer.Service.Mappers;

public static class CategoryMapper
{
    public static Category ToEntity(CategoryCreateModel model)
    {
        return new Category
        {
            
            Name = model.Name,
            
        };
    }

    public static CategoryViewModel? ToViewModel(Category? model)
    {
        if(model == null)
            return null; 
        
        return new CategoryViewModel
        {
            Id = model.Id,
            Name = model.Name,
            //Books =  model.BookCategories == null ? [] : model.BookCategories.Select(x => BookMapper.ToViewModel(x.Book!)).ToList(),
        };
    }

    public static CategoryWithBooksViewModel? ToViewWithBooksModel(Category? model)
    {
        if (model == null)
            return null;

        return new CategoryWithBooksViewModel
        {
            Id= model.Id,
            Name = model.Name,
            Books =  model.BookCategories == null ? [] : model.BookCategories.Select(x => BookMapper.ToViewModel(x.Book!)).ToList(),
        };

    }


    public static Category ToEntity(CategoryUpdateModel model, Category entity)
    {
        entity.Name = model.Name;
        return entity;
    }

    public static List<CategoryViewModel>? ToViewModelList(IEnumerable<Category>? categories)
    {
        
        if(categories == null) return null;


        return categories.Select(ToViewModel).ToList()!;

        
    }

    public static List<CategoryWithBooksViewModel>? ToViewWithBooksModelList(IEnumerable<Category>? categories)
    {

        if (categories == null) return null;


        return categories.Select(ToViewWithBooksModel).ToList()!;


    }






}


