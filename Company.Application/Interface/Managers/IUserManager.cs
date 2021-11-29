using System.Collections.Generic;
using System.Threading.Tasks;
using CompanySystem.Application.Models;
using CompanySystem.Application.Views;

namespace CompanySystem.Application.Interface.Managers
{
    public interface IUserManager
    {
        Task<UserView> GetUserById(int id);

        Task<List<UserView>> GetAllUsers();

        Task<UserView> CreateUser(UserModel model);

        Task<UserView> UpdateUser(int id, UserModel model);

        Task DeleteUser(int id);
    }
}