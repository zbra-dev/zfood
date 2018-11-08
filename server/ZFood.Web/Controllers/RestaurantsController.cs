using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using ZFood.Core.API;
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
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<PageDTO<RestaurantDTO>>> Get(int take, int skip, string query = null)
        {
            var page = await service.Get(take, skip, query);
            return page.ToDTO(r => r.ToDTO());
        }

        // GET restaurants/5
        [HttpGet("{id}")]
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
        public void Post([FromBody] string value)
        {
        }

        // PUT restaurants/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE restaurants/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
