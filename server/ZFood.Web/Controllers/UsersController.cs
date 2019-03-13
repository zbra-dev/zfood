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
        /// <response code="200">
        /// Returned code when a page, with the given parameters, can be build successfully
        /// </response>
        /// <response code="400">
        /// Returned code when skip and take parameters are invalid. Note that skip and take must be greater than zero
        /// </response>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<PageDTO<UserDTO>>> Get(int skip, int take, bool count = false, string query = null)
        {
            if (take < 0 || skip < 0)
            {
                return BadRequest("Skip and Take must be greater than zero"); // TODO: Figure out a better way to validate this.
            }
            var page = await service.Get(skip, take, count, query);
            return page.ToDTO(u => u.ToDTO());
        }

        // GET user/5
        /// <summary>
        /// Finds a User by Id
        /// </summary>
        /// <param name="id">Id of the User to be found</param>
        /// <response code="200">
        /// Returned code when a User with the specified Id can be found
        /// </response>
        /// <response code="404">
        /// Returned code when a User with the specified Id cannot be found
        /// </response>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetUser")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<UserDTO>> Get(string id)
        {
            var user = await service.FindById(id);

            if (user == null)
            {
                log.Debug($"User {id} was not found");
                return NotFound();
            }
            return user.ToDTO();
        }

        // POST user
        /// <summary>
        /// Creates a User with the given data
        /// </summary>
        /// <param name="dto">Contains the necessary fields to create a User.</param>
        /// <response code="201">
        /// Returned code when the User can be created successfully
        /// </response>
        /// <response code="400">
        /// Returned code when trying to create a new User with the same properties of an already existing User
        /// or when creating a User with an invalid provider
        /// </response>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> Post([FromBody] CreateUserRequestDTO dto)
        {
            var createdUser = await service.CreateUser(dto.FromDTO());
            return CreatedAtRoute("GetUser", new { id = createdUser.Id }, createdUser.ToDTO());
        }

        // PUT user/5
        /// <summary>
        /// Updates a User with the given data
        /// </summary>
        /// <param name="id">Id of the User to be updated.</param>
        /// <param name="dto">Contains the data of the User to be changed</param>
        /// <response code="204">
        /// Returned code when the User can be updated successfully
        /// </response>
        /// <response code="400">
        /// Returned code when trying to update a User with the same Email of an another existing User  
        /// </response>
        /// <response code="404">
        /// Returned code when cannot find the User to be updated with the given Id
        /// </response>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> Put(string id, [FromBody] UpdateUserRequestDTO dto)
        {
            await service.UpdateUser(dto.FromDTO(id));
            return NoContent();
        }

        // DELETE user/5
        /// <summary>
        /// Deletes a User by Id
        /// </summary>
        /// <param name="id">Id of the User to be deleted</param>
        /// <response code="204">
        /// Returned code when a User can be deleted successfully
        /// </response>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        public async Task<ActionResult> Delete(string id)
        {
            await service.DeleteUser(id);
            return NoContent();
        }
    }
}
