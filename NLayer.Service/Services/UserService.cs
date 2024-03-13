using NLayer.Core;
using NLayer.Core.Models;
using NLayer.Core.Repositories;
using NLayer.Core.Services;

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

        public void CreateUser(string username, string password)
        {
            _repository.CreateUser(username, password);
        }

        public int GetAuthenticatedUserId(string token)
        {
            return _repository.GetAuthenticatedUserId(token);
        }

        public void RemoveUser(int id, string token)
        {
            _repository.RemoveUser(id, token);
        }

        public void UpdateUser(int id, string username, string password)
        {
            _repository.UpdateUser(id, username, password);
        }
    }
}
