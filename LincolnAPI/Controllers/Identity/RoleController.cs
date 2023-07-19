using LincolnAPI.Database;
using LincolnAPI.DBModels.Identity;
using LincolnAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LincolnAPI.Controllers.Identity
{
    [Route("roles")]
    public class RoleController : ControllerBase
    {

        private readonly IdentityRepository _repo;
        public RoleController(IdentityRepository context)
        {
            _repo = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetRoles()
        {
            return Ok(await _repo.GetAllRolesAsync());
        }

        [HttpPost]
        public async Task<IActionResult> AddRole([FromBody] Role role)
        {
            var returnValue = await _repo.InsertRoleAsync(role);
            await _repo.SaveChangesAsync();
            return Ok(returnValue);
        }
    }
}
