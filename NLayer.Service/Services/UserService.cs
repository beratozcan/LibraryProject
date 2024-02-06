using NLayer.Core;
using NLayer.Core.Models;
using NLayer.Core.Repositories;
using NLayer.Core.Services;

namespace NLayer.Service.Services
{
    public class UserService : Service<User>, IUserService
    {

        private readonly IUserRepository _repository;

        public UserService(IGenericRepository<User> repository, IUnitOfWork unitOfWork, IUserRepository userRepository)
            :base(repository, unitOfWork)
        {
            _repository = userRepository;
        }

        public async Task<IEnumerable<User>> GetUserWithBooks(int id)
        {
            return await _repository.GetUserWithBooks(id);
        }
    }
}
