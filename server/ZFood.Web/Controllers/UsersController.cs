using log4net;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ZFood.Core.API;
using ZFood.Core.API.Exceptions;
using ZFood.Web.DTO;
using ZFood.Web.Extensions;

namespace ZFood.Web.Controllers
{
    [Route("[controller]")]
    [Controller]
    public class UsersController : ControllerBase
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(UsersController));

        private readonly IUserService service;

        public UsersController(IUserService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<ActionResult<PageDTO<UserDTO>>> Get(int skip, int take, bool count, string query)
        {
            log.Debug("Searching for some users");
            var page = await service.Get(skip, take, count, query);
            return page.ToDTO(u => u.ToDTO());
        }

        // GET user/5
        [HttpGet("{id}", Name = "GetUser")]
        public async Task<ActionResult<UserDTO>> Get(string id)
        {
            log.Debug($"Searching for user {id}");
            var user = await service.FindById(id);

            if (user == null)
            {
                log.Debug($"User {id} was not found");
                return NotFound();
            }
            log.Debug($"User {id} was found and returned");
            return user.ToDTO();
        }

        // POST user
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CreateUserRequestDTO dto)
        {
            try
            {
                log.Debug("Trying to create user");
                var createdUser = await service.CreateUser(dto.FromDTO());
                return CreatedAtRoute("GetUser", new { id = createdUser.Id }, createdUser.ToDTO());
            }
            catch
            {
                log.Debug("Fail on trying to create a user");
                return BadRequest();
            }
        }

        // PUT user/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(string id, [FromBody] UpdateUserRequestDTO dto)
        {
            log.Debug($"Trying to edit user {id}");
            try
            {
                await service.UpdateUser(dto.FromDTO(id));
                return NoContent();
            }
            catch (EntityNotFoundException exception)
            {
                log.Debug($"Fail on trying to edit user {id}");
                return NotFound(exception.Message);
            }
            catch
            {
                log.Debug($"Fail on trying to edit a user {id}");
                return BadRequest();
            }
        }

        // DELETE user/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            log.Debug($"Deleting user {id}");
            await service.DeleteUser(id);
            return NoContent();
        }
    }
}
