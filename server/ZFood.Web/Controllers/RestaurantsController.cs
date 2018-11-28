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
    public class RestaurantsController : ControllerBase
    {
        private readonly IRestaurantService service;

        public RestaurantsController(IRestaurantService service)
        {
            this.service = service;
        }

        /// <summary>
        /// Searches for restaurants
        /// </summary>
        /// <param name="take">Number of restaurants to take</param>
        /// <param name="skip">Number of restaurants to skip</param>
        /// <param name="query">Query</param>
        /// <param name="count">Count restaurants</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<PageDTO<RestaurantDTO>>> Get(int take, int skip, bool count = false, string query = null)
        {
            var page = await service.Get(take, skip, count, query);
            return page.ToDTO(r => r.ToDTO());
        }

        // GET restaurants/5
        [HttpGet("{id}", Name = "GetRestaurant")]
        public async Task<ActionResult<RestaurantDTO>> Get(string id)
        {
            var restaurant = await service.FindById(id);

            if (restaurant == null)
            {
                return NotFound();
            }

            return restaurant.ToDTO();
        }

        // POST restaurants
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CreateRestaurantRequestDTO dto)
        {
            try
            {
                var createdRestaurant = await service.CreateRestaurant(dto.FromDTO());
                return CreatedAtRoute("GetRestaurant", new { id = createdRestaurant.Id }, createdRestaurant.ToDTO());
            }
            catch
            {
                return BadRequest();
            }
        }

        // PUT restaurants/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(string id, [FromBody] UpdateRestaurantRequestDTO dto)
        {
            try
            {
                await service.UpdateRestaurant(dto.FromDTO(id));
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

        // DELETE restaurants/5
        [HttpDelete("{id}")]
        public async Task Delete(string id)
        {
            await service.Delete(id);
        }
    }
}
