using CompanySystem.Domain.Entities;
using CompanySystem.Domain.Interfaces.Repositories;
using CompanySystem.Domain.Interfaces.Services;

namespace CompanySystem.Domain.Services
{
    public class UserService : BaseService<User>, IUserService
    {
        private readonly IUserRepository _repository;
        public UserService(IUserRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}