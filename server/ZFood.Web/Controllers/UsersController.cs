using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ZFood.Core.API;
using ZFood.Core.API.Exceptions;
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

        // PUT user/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(string id, [FromBody] UpdateUserRequestDTO dto)
        {
            try
            {
                await service.UpdateUser(dto.FromDTO(id));
                return NoContent();
            }
            catch (EntityNotFoundException exception)
            {
                return NotFound(exception.Message);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
