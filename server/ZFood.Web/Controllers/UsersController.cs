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

        // GET user/5
        [HttpGet("{id}", Name = "GetUser")]
        public async Task<ActionResult<UserDTO>> Get(string id)
        {
            var user = await service.FindById(id);

            if (user == null)
            {
                return NotFound();
            }
            return user.ToDTO();
        }

        [HttpGet]
        public async Task<ActionResult<PageDTO<UserDTO>>> Get(int skip, int take, bool count, string query)
        {
            var page = await service.Get(skip, take, count, query);
            return page.ToDTO(u => u.ToDTO());
        }

        // POST user
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CreateUserRequestDTO dto)
        {
            try
            {
                var createdUser = await service.CreateUser(dto.FromDTO());
                return CreatedAtRoute("GetUser", new { id = createdUser.Id }, createdUser.ToDTO());
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

        // DELETE user/5
        [HttpDelete("{id}")]
        public async Task Delete(string id)
        {
            await service.DeleteUser(id);
        }
    }
}
