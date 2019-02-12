using log4net;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ZFood.Core.API;
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

        /// <summary>
        /// Searches for users
        /// </summary>
        /// <param name="take">Number of users to take</param>
        /// <param name="skip">Number of users to skip</param>
        /// <param name="query">Query</param>
        /// <param name="count">Count users</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<PageDTO<UserDTO>>> Get(int skip, int take, bool count, string query)
        {
            log.Debug("Searching for some users");
            var page = await service.Get(skip, take, count, query);
            return page.ToDTO(u => u.ToDTO());
        }

        // GET user/5
        /// <summary>
        ///  Finds a User by Id. If a User cannot be found, a 404 HTTP Response will be returned
        /// </summary>
        /// <param name="id">Id of the User to be found</param>
        /// <returns></returns>
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
        /// <summary>
        /// Creates a User with the given data
        /// </summary>
        /// <param name="dto">Contains the necessary fields to create a User.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CreateUserRequestDTO dto)
        {
            log.Debug("Trying to create user");
            var createdUser = await service.CreateUser(dto.FromDTO());
            return CreatedAtRoute("GetUser", new { id = createdUser.Id }, createdUser.ToDTO());
        }

        // PUT user/5
        /// <summary>
        /// Updates a User with the given data
        /// </summary>
        /// <param name="id">Id of the User to be updated. Be sure that this User already exists</param>
        /// <param name="dto">Contains the data of the User to be changed</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(string id, [FromBody] UpdateUserRequestDTO dto)
        {
            log.Debug($"Trying to edit user {id}");
            await service.UpdateUser(dto.FromDTO(id));
            return NoContent();
        }

        // DELETE user/5
        /// <summary>
        /// Deletes a User by Id
        /// </summary>
        /// <param name="id">Id of the User to be deleted</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            log.Debug($"Deleting user {id}");
            await service.DeleteUser(id);
            return NoContent();
        }
    }
}
