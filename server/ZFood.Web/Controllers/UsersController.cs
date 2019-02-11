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
            var createdUser = await service.CreateUser(dto.FromDTO());
            return CreatedAtRoute("GetUser", new { id = createdUser.Id }, createdUser.ToDTO());
        }

        // PUT user/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(string id, [FromBody] UpdateUserRequestDTO dto)
        {
            await service.UpdateUser(dto.FromDTO(id));
            return NoContent();
        }

        // DELETE user/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            await service.DeleteUser(id);
            return NoContent();
        }
    }
}
