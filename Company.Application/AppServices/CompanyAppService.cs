using CompanySystem.Application.Interface.AppServices;
using CompanySystem.Domain.Entities;
using CompanySystem.Domain.Interfaces.Services;

namespace CompanySystem.Application.AppServices
{
    public class CompanyAppService : BaseAppService<Company>, ICompanyAppService
    {
        private readonly ICompanyService _companyService;

        public CompanyAppService(ICompanyService companyService) : base(companyService)
        {
            _companyService = companyService;
        }
    }
}