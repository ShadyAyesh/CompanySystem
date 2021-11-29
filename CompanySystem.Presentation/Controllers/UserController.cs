using System.Threading.Tasks;
using CompanySystem.Application.Interface.Managers;
using CompanySystem.Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace CompanySystem.Presentation.Controllers
{
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserManager _manager;

        public UserController(IUserManager manager)
        {
            _manager = manager;
        }

        [HttpGet]
        [Route("Users")]
        public async Task<IActionResult> GetAllUsers()
        {
            return Ok(await _manager.GetAllUsers());
        }

        [HttpGet]
        [Route("User/id")]
        public async Task<IActionResult> GetUserById([FromQuery] int id)
        {
            if (id <= 0)
                return BadRequest("Invalid user id.");

            return Ok(await _manager.GetUserById(id));
        }

        [HttpPost]
        [Route("User")]
        public async Task<IActionResult> CreateUser([FromBody] UserModel model)
        {
            if (model == null)
                return BadRequest("Invalid model.");

            return Ok(await _manager.CreateUser(model));
        }

        [HttpPut]
        [Route("User/id")]
        public async Task<IActionResult> UpdateUser([FromQuery] int id, [FromBody] UserModel model)
        {
            if (id <= 0 || model == null)
                return BadRequest("Invalid user id or model.");

            return Ok(await _manager.UpdateUser(id, model));
        }

        [HttpDelete]
        [Route("User/id")]
        public async Task<IActionResult> DeleteUser([FromQuery] int id)
        {
            if (id <= 0)
                return BadRequest("Invalid user id.");

            await _manager.DeleteUser(id);

            return NoContent();
        }
    }
}