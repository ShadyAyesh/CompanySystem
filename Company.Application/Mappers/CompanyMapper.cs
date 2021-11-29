using CompanySystem.Application.Models;
using CompanySystem.Application.Views;
using CompanySystem.Domain.Entities;

namespace CompanySystem.Application.Mappers
{
    public static class CompanyMapper
    {
        public static Company MapModelToEntity(this CompanyModel model)
        {
            if (model == null) return null;
            return new Company()
            {
                Name = model.Name
            };
        }
        
        public static CompanyView MapEntityToView(this Company entity)
        {
            if (entity == null) return null;
            return new CompanyView()
            {
                Id = entity.Id,
                Name = entity.Name,
                Users = entity.Users
            };
        }
    }
}