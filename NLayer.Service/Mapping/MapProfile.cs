using AutoMapper;
using NLayer.Core.DTOs;
using NLayer.Core.Models;

namespace NLayer.Service.Mapping
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<Book,BookDTO>().ReverseMap();
            CreateMap<Category,CategoryDTO>().ReverseMap();
            CreateMap<User,UserDTO>().ReverseMap();
            CreateMap<User, UserPostDTO>().ReverseMap();
            CreateMap<UserPostDTO, UserDTO>().ReverseMap();
            CreateMap<CategoryPostDTO, CategoryDTO>().ReverseMap();
            CreateMap<CategoryPostDTO, Category>().ReverseMap();
            CreateMap<BookPostDTO, BookDTO>().ReverseMap();
            CreateMap<BookPostDTO,Book>().ReverseMap();
            CreateMap<BorrowedBookDTO,Book>().ReverseMap();
            CreateMap<BorrowedBookDTO,BookDTO>().ReverseMap();
        }

    }
}
