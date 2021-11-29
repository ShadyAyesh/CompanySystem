using CompanySystem.Application.Models;
using CompanySystem.Application.Views;
using CompanySystem.Domain.Entities;

namespace CompanySystem.Application.Mappers
{
    public static class UserMapper
    {
        public static User MapModelToEntity(this UserModel model)
        {
            if (model == null) return null;
            return new User()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Password = model.Password
            };
        }

        public static UserView MapEntityToView(this User entity, string companyName = null)
        {
            if (entity == null) return null;
            return new UserView()
            {
                Id = entity.Id,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Email = entity.Email,
                Password = "$$$$",
                CompanyName = companyName
            };
        }
    }
}