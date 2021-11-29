using CompanySystem.Application.Interface.AppServices;
using CompanySystem.Domain.Entities;
using CompanySystem.Domain.Interfaces.Services;

namespace CompanySystem.Application.AppServices
{
    public class UserAppService : BaseAppService<User>, IUserAppService
    {
        private readonly IUserService _userService;

        public UserAppService(IUserService userService) : base(userService)
        {
            _userService = userService;
        }
    }
}