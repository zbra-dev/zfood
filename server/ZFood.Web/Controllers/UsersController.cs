using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ZFood.Core.API;
using ZFood.Web.DTO;
using ZFood.Web.Extensions;

namespace ZFood.Web.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService service;

        public UsersController(IUserService service)
        {
            this.service = service;
        }

        // POST user
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CreateUserRequestDTO dto)
        {
            try
            {
                var createdUser = await service.CreateUser(dto.FromDTO());
                // return CreatedAtRoute("GetUser", new { id = createdUser.Id }, createdUser.ToDTO());
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
