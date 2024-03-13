using NLayer.Core.DTOs;
using NLayer.Core.Models;

namespace NLayer.Service.Mappers;

public static class UserMapper
{ 
    public static User ToEntity(UserCreateModel model)
    {
        return new User
        {
            UserName = model.UserName, 
        };
    }

    public static UserViewModel ToViewModel(User user)
    {
        
        return new UserViewModel
        {
            Id = user.Id,
            UserName = user.UserName,
            Password = BitConverter.ToString(user.PasswordHash).Replace("-",""),
           Books = user.OwnedBooks == null ? new List<BookViewModel>() : BookMapper.ToViewModelList(user.OwnedBooks)
        };
    }

    public static User ToEntity(UserUpdateModel model, User entity)
    {
        entity.UserName = model.UserName;

        return entity;
    }

    public static List<UserViewModel> ToViewModelList(IEnumerable<User> users)
    {

        return users.Select(ToViewModel).ToList();
    }
}

