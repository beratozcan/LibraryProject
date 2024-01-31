using AutoMapper;
using NLayer.Core.DTOs;
using NLayer.Core.Models;

namespace NLayer.Service.Mapping
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<Book,BookModel>().ReverseMap();
            CreateMap<Category,CategoryModel>().ReverseMap();
            CreateMap<User,UserModel>().ReverseMap();
            CreateMap<User, UserPostModel>().ReverseMap();
            CreateMap<UserPostModel, UserModel>().ReverseMap();
            CreateMap<CategoryPostModel, CategoryModel>().ReverseMap();
            CreateMap<CategoryPostModel, Category>().ReverseMap();
            CreateMap<BookPostModel, BookModel>().ReverseMap();
            CreateMap<BookPostModel,Book>().ReverseMap();
            CreateMap<BorrowedBookModel,Book>().ReverseMap();
            CreateMap<BorrowedBookModel,BookModel>().ReverseMap();
            CreateMap<FinishedBookModel, BookModel>().ReverseMap();
            CreateMap<FinishedBookModel, Book>().ReverseMap();
            
        }

    }
}
