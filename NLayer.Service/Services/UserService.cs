using NLayer.Core;
using NLayer.Core.Models;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Repository.Repositories;

namespace NLayer.Service.Services
{
    public class UserService : Service<User>, IUserService
    {

        private readonly IUserRepository _repository;

        public UserService(IUnitOfWork unitOfWork, IUserRepository userRepository)
            :base(userRepository, unitOfWork)
        {
            _repository = userRepository;
        }

        public Task<bool> AuthenticateUser(string username, string password)
        {
            return _repository.AuthenticateUser(username, password);
        }

        public void CreateUser(string username, string password)
        {
            _repository.CreateUser(username, password);
        }

        public bool DidUserLogin(int userId)
        {
            return _repository.DidUserLogin(userId);
        }

        public void UpdateUser(int id, string username, string password)
        {
            _repository.UpdateUser(id, username, password);
        }
    }
}
