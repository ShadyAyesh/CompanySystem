using System.Threading.Tasks;
using CompanySystem.Application.Interface.Managers;
using CompanySystem.Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace CompanySystem.Presentation.Controllers
{
    public class CompanyController : Controller
    {
        private readonly ICompanyManager _manager;

        public CompanyController(ICompanyManager manager)
        {
            _manager = manager;
        }

        [HttpGet]
        [Route("Companies")]
        public async Task<IActionResult> GetAllCompanies()
        {
            return Ok(await _manager.GetAllCompanies());
        }

        [HttpGet]
        [Route("Company/id")]
        public async Task<IActionResult> GetCompanyById([FromQuery] int id)
        {
            if (id <= 0)
                return BadRequest("Invalid company id.");

            return Ok(await _manager.GetCompanyById(id));
        }

        [HttpPost]
        [Route("Company")]
        public async Task<IActionResult> CreateCompany([FromBody] CompanyModel model)
        {
            if (model == null)
                return BadRequest("Invalid company model.");

            return Ok(await _manager.CreateCompany(model));
        }

        [HttpPut]
        [Route("Company/id")]
        public async Task<IActionResult> UpdateCompany([FromQuery] int id, [FromBody] CompanyModel model)
        {
            if (id <= 0 || model == null)
                return BadRequest("Invalid company id or model.");

            return Ok(await _manager.UpdateCompany(id, model));
        }

        [HttpDelete]
        [Route("Company/id")]
        public async Task<IActionResult> DeleteCompany([FromQuery] int id)
        {
            if (id <= 0)
                return BadRequest("Invalid company id.");

            await _manager.DeleteCompany(id);
            return NoContent();
        }

        [HttpPut]
        [Route("Company/AttachUser")]
        public async Task<IActionResult> AttachUsersToCompany([FromQuery] int userId, [FromQuery] int companyId)
        {
            if (userId <= 0 || companyId <= 0)
                return BadRequest("Invalid company id or user id.");

            return Ok(await _manager.AttachUserToCompany(userId, companyId));
        }

        [HttpPut]
        [Route("Company/DetachUser")]
        public async Task<IActionResult> DetachUsersToCompany([FromQuery] int userId, [FromQuery] int companyId)
        {
            if (userId <= 0 || companyId <= 0)
                return BadRequest("Invalid company id or user id.");

            return Ok(await _manager.DetachUserFromCompany(userId, companyId));
        }
    }
}