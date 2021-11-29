using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompanySystem.Application.Common;
using CompanySystem.Application.Interface.AppServices;
using CompanySystem.Application.Interface.Managers;
using CompanySystem.Application.Mappers;
using CompanySystem.Application.Models;
using CompanySystem.Application.Views;
using Microsoft.Extensions.Logging;

namespace CompanySystem.Application.Managers
{
    public class UserManager : IUserManager
    {
        private readonly IUserAppService _service;
        private readonly ICompanyAppService _companyService;
        private readonly ILogger<UserManager> _logger;

        public UserManager(
            IUserAppService service, 
            ICompanyAppService companyService,
            ILogger<UserManager> logger)
        {
            _service = service;
            _companyService = companyService;
            _logger = logger;
        }

        public async Task<UserView> GetUserById(int id)
        {
            var user = await _service.GetById(id);
            if (user == null)
                throw new ItemNotFoundException(nameof(user), id);

            var company = (await _companyService.GetAll("Users"))
                .FirstOrDefault(x => x.Users.Any(e => e.Id == user.Id));

            return user.MapEntityToView(company?.Name);
        }

        public async Task<List<UserView>> GetAllUsers()
        {
            var users = (await _service.GetAll()).ToList();
            var companies = (await _companyService.GetAll("Users")).ToList();

            var userViews = new List<UserView>();
            if (users.Count <= 0) return userViews;

            userViews
                .AddRange(from user in users let company = companies
                    .FirstOrDefault(x => x.Users.Any(e => e.Id == user.Id))
                    select user.MapEntityToView(company?.Name));

            return userViews;
        }

        public async Task<UserView> CreateUser(UserModel model)
        {
            if (!ValidateModel(model))
            {
                _logger.Log(LogLevel.Error, "Invalid Model.");
                throw new InvalidModelException();
            }

            model.Password = Security.Encrypt(model.Password);
            var user = (await _service.Add(model.MapModelToEntity())).MapEntityToView();

            _logger.Log(LogLevel.Information, "User created.");
            return user;
        }

        public async Task<UserView> UpdateUser(int id, UserModel model)
        {
            if (!ValidateModel(model))
            {
                _logger.Log(LogLevel.Error, "Invalid Model.");
                throw new InvalidModelException();
            }

            var user = await _service.GetById(id);
            if (user == null)
            {
                _logger.Log(LogLevel.Error, $"User with Id: {id} is not found.");
                throw new ItemNotFoundException(nameof(user), id);
            }

            user = model.MapModelToEntity();
            user.Id = id;

            var company = (await _companyService.GetAll("Users"))
                .FirstOrDefault(x => x.Users.Any(e => e.Id == user.Id));

            model.Password = Security.Encrypt(model.Password);
            _ = _service.Update(user);

            _logger.Log(LogLevel.Information, $"User with Id: {id} is updated.");
            return user.MapEntityToView(company?.Name);
        }

        public async Task DeleteUser(int id)
        {
            var user = await _service.GetById(id);
            if (user == null)
            {
                _logger.Log(LogLevel.Error, $"User with Id: {id} is not found.");
                throw new ItemNotFoundException(nameof(user), id);
            }

            await _service.Delete(user);
            _logger.Log(LogLevel.Information, $"User with Id: {id} is deleted.");
        }

        private static bool ValidateModel(UserModel model)
        {
            return !string.IsNullOrEmpty(model.FirstName) && 
                   !string.IsNullOrEmpty(model.LastName) && 
                   !string.IsNullOrEmpty(model.Email) && 
                   !string.IsNullOrEmpty(model.Password);
        }
    }
}