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
    public class RestaurantsController : ControllerBase
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(RestaurantsController));

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
            log.Debug("Searching for some restaurants");
            var page = await service.Get(take, skip, count, query);
            return page.ToDTO(r => r.ToDTO());
        }

        // GET restaurants/5
        [HttpGet("{id}", Name = "GetRestaurant")]
        public async Task<ActionResult<RestaurantDTO>> Get(string id)
        {
            log.Debug($"Searching for restaurant {id}"); 
            var restaurant = await service.FindById(id);

            if (restaurant == null)
            {
                log.Debug($"Restaurant {id} was not found");
                return NotFound();
            }
            log.Debug($"Restaurant {id} was found and returned");
            return restaurant.ToDTO();
        }

        // POST restaurants
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CreateRestaurantRequestDTO dto)
        {
            log.Debug("Trying to create a restaurant");
            var createdRestaurant = await service.CreateRestaurant(dto.FromDTO());
            return CreatedAtRoute("GetRestaurant", new { id = createdRestaurant.Id }, createdRestaurant.ToDTO());
        }

        // PUT restaurants/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(string id, [FromBody] UpdateRestaurantRequestDTO dto)
        {
            log.Debug($"Trying to edit restaurant {id}");
            await service.UpdateRestaurant(dto.FromDTO(id));
            return NoContent();
        }

        // DELETE restaurants/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            log.Debug($"Deleting restaurant {id}");
            await service.Delete(id);
            return NoContent();
        }
    }
}
