using NLayer.Core;
using NLayer.Core.Entities;
using NLayer.Core.Models;
using NLayer.Core.Repositories;
using NLayer.Core.Services;

namespace NLayer.Service.Services
{
    public class AuthenticateService : Service<User>, IAuthenticateService
    {
        private readonly IAuthenticateRepository _repository;

        public AuthenticateService(IUnitOfWork unitOfWork, IAuthenticateRepository authenticateRepository)
            : base(authenticateRepository, unitOfWork)
        {
            _repository = authenticateRepository;
        }

        public Task<UserToken> AuthenticateUser(string username, string password)
        {
           return  _repository.AuthenticateUser(username, password);
        }
    }
}
