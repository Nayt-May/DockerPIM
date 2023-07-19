using LincolnAPI.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LincolnAPI.Products
{
    [Authorize(Roles = "admin")]
    [Route("products")]
    public class ProductsController : ControllerBase
    {
        [HttpGet]
        [RequiresRoleFromClaim("admin")]
        public IActionResult getAll()
        {
            return Ok("Productstesting");
        }
    }
}
