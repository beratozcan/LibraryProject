using NLayer.Core.DTOs;
using NLayer.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Service.Mapping
{
    public class EntityMapper
    {
        public Book BookMapPostModelToEntity(BookPostModel model)
        {
            return new Book
            {
                
                Name = model.Name,
                Author = model.Author,
                Publisher = model.Publisher,
                
                Page = model.Page,
                HaveRead = model.haveRead,
                IsBorrowed = model.isBorrowed,
                CategoryId=model.CategoryId,
                UserId = model.UserId

    };


            
        }

        public Book BookMapPutModelToEntity(BookModel model, Book entity)
        {
            entity.Name = model.Name ?? entity.Name;
            entity.Author = model.Author ?? entity.Author;
            entity.Publisher = model.Publisher ?? entity.Publisher;
            entity.Page = model.Page;

            entity.IsBorrowed = model.IsBorrowed;
            entity.HaveRead = model.HaveRead;
            entity.CategoryId = model.CategoryId;
            entity.UserId = model.UserId;

            return entity;



        }

        public BookModel BookMapEntityToModel(Book book)
        {
            return new BookModel
            {
                Id = book.Id,
                Name = book.Name,
                Author = book.Author,
                Publisher = book.Publisher,
                PublishDate = book.PublishDate,
                Page = book.Page,
                HaveRead = book.HaveRead,
                IsBorrowed = book.IsBorrowed,
                CategoryId = book.CategoryId,
                UserId = book.UserId

            };

        }

        public User UserMapModelToEntity(UserPostModel model)
        {
            return new User
            {

                
                UserName = model.UserName,
                Password = model.Password
            };

           
        }

        public UserModel UserMapEntitytoModel(User user)
        {
            return new UserModel
            {
                Id = user.Id,
                UserName = user.UserName,
                Password = user.Password
            };

 
        }

        public Category CategoryMapModelToEntity(CategoryModel model)
        {
            return new Category
            {
                Id = model.Id,
                Name = model.Name
            };

            
        }

        public CategoryModel CategoryMapEntityToModel(Category model)
        {
            return new CategoryModel
            {
                Id = model.Id,
                Name = model.Name,
            };
        }

        public Category CategoryPostMapEntityToModel(CategoryPostModel model)
        {
            return new Category
            {
                Name = model.Name
        };

            
        }

        public Category CategoryPutMapEntityToModel(CategoryModel model, Category entity)
        {
            entity.Name = model.Name;
            return entity;
        }

        

        public User UserPostMapEntityToModel(UserPostModel model)
        {
            return new User
            {
                UserName = model.UserName,
                Password = model.Password
                   
            };


        }

        public User UserPutMapEntityToModel(UserPutModel model, User entity)
        {
            entity.UserName = model.UserName;
            entity.Password = model.Password;

            return entity;
        }



    }
}
