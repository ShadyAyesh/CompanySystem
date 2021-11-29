using System.Collections.Generic;
using System.Threading.Tasks;
using CompanySystem.Application.Models;
using CompanySystem.Application.Views;

namespace CompanySystem.Application.Interface.Managers
{
    public interface ICompanyManager
    {
        Task<CompanyView> GetCompanyById(int id);

        Task<List<CompanyView>> GetAllCompanies();

        Task<CompanyView> CreateCompany(CompanyModel model);

        Task<CompanyView> UpdateCompany(int id, CompanyModel model);

        Task<UserView> AttachUserToCompany(int userId, int companyId);

        Task<UserView> DetachUserFromCompany(int userId, int companyId);

        Task DeleteCompany(int id);
    }
}