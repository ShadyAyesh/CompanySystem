using CompanySystem.Domain.Entities;
using CompanySystem.Domain.Interfaces.Repositories;

namespace CompanySystem.Infrastructure.Repositories
{
    public class CompanyRepository: BaseRepository<Company>, ICompanyRepository
    {
        
    }
}