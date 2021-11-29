using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompanySystem.Application.Common;
using CompanySystem.Application.Interface.AppServices;
using CompanySystem.Application.Interface.Managers;
using CompanySystem.Application.Mappers;
using CompanySystem.Application.Models;
using CompanySystem.Application.Views;
using CompanySystem.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace CompanySystem.Application.Managers
{
    public class CompanyManager : ICompanyManager
    {
        private readonly ICompanyAppService _companyService;
        private readonly IUserAppService _userService;
        private readonly ILogger<CompanyManager> _logger;

        public CompanyManager(
            ICompanyAppService companyService,
            IUserAppService userService,
            ILogger<CompanyManager> logger)
        {
            _companyService = companyService;
            _userService = userService;
            _logger = logger;
        }

        public async Task<CompanyView> GetCompanyById(int id)
        {
            var company = await _companyService.GetById(id, "Users");
            if (company != null) 
                return company.MapEntityToView();

            _logger.Log(LogLevel.Error, $"Company with Id: {id} is not found");
            throw new ItemNotFoundException(nameof(company), id);

        }

        public async Task<List<CompanyView>> GetAllCompanies()
        {
            return (await _companyService.GetAll("Users"))
                .Select(x => x.MapEntityToView()).ToList();
        }

        public async Task<CompanyView> CreateCompany(CompanyModel model)
        {
            if (!ValidateModel(model))
            {
                _logger.Log(LogLevel.Error, "Invalid Model");
                throw new InvalidModelException();
            }

            var company = (await _companyService.Add(model.MapModelToEntity())).MapEntityToView();
            _logger.Log(LogLevel.Information, "Company Created");

            return company;
        }

        public async Task<CompanyView> UpdateCompany(int id, CompanyModel model)
        {
            if (!ValidateModel(model))
            {
                _logger.Log(LogLevel.Error, "Invalid Model");
                throw new InvalidModelException();
            }

            var company = await _companyService.GetById(id);
            if (company == null)
            {
                _logger.Log(LogLevel.Error, $"Company with Id: {id} is not found.");
                throw new ItemNotFoundException(nameof(company), id);
            }

            company = model.MapModelToEntity();
            company.Id = id;

            _ = _companyService.Update(company);
            _logger.Log(LogLevel.Information, $"Company with Id: {id} is Updated.");

            return company.MapEntityToView();
        }

        public async Task DeleteCompany(int id)
        {
            var company = await _companyService.GetById(id);
            if (company == null)
            {
                _logger.Log(LogLevel.Error, $"Company with Id: {id} is not found.");
                throw new ItemNotFoundException(nameof(company), id);
            }
            
            await _companyService.Delete(company);
            _logger.Log(LogLevel.Information, $"Company with Id: {id} is Deleted.");
        }

        public async Task<UserView> AttachUserToCompany(int userId, int companyId)
        {
            var user = await _userService.GetById(userId);
            var company = await _companyService.GetById(companyId, "Users");

            ValidateAttach(user, company);

            user.CompanyId = companyId;
            user.Id = userId;

            _ = _userService.Update(user);

            var view = user.MapEntityToView();
            view.CompanyName = company.Name;

            _logger.Log(LogLevel.Information, 
                $"User with Id: {userId} is attached to Company {company.Name}.");
            return view;
        }

        public async Task<UserView> DetachUserFromCompany(int userId, int companyId)
        {
            var user = await _userService.GetById(userId);
            var company = await _companyService.GetById(companyId, "Users");
            ValidateDetach(user, company);

            user.CompanyId = null;
            user.Id = userId;

            _ = _userService.Update(user);

            var view = user.MapEntityToView();
            view.CompanyName = null;

            _logger.Log(LogLevel.Information,
                $"User with Id: {userId} is detached from Company {company.Name}.");
            return view;
        }

        private static void ValidateAttach(User user, Company company)
        {
            if (user == null || company == null)
                throw new DefaultException("Either company or user not exist.");

            if (company.Users.Any(u => u.Id == user.Id))
                throw new DefaultException("This user is already attached to the company.");

            if (user.CompanyId != null)
                throw new DefaultException("This user is already attached to another company.");
        }

        private static void ValidateDetach(User user, Company company)
        {
            if (user == null || company == null)
                throw new DefaultException("Either company or user not exist.");

            if (company.Users.All(u => u.Id != user.Id))
                throw new DefaultException("This user is not attached to the company.");
        }

        private static bool ValidateModel(CompanyModel model)
        {
            if (model == null)
                return false;

            return !string.IsNullOrEmpty(model.Name);
        }
    }
}