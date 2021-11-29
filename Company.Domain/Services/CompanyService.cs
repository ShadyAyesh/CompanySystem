using CompanySystem.Domain.Entities;
using CompanySystem.Domain.Interfaces.Repositories;
using CompanySystem.Domain.Interfaces.Services;

namespace CompanySystem.Domain.Services
{
    public class CompanyService : BaseService<Company>, ICompanyService
    {
        private readonly ICompanyRepository _repository;
        public CompanyService(ICompanyRepository repository) : base(repository)
        {
            _repository = repository;
        }

    }
}