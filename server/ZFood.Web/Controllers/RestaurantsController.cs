using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using ZFood.Core.API;
using ZFood.Model;

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
        public ActionResult<IEnumerable<string>> Get(int? take, int? skip, string query)
        {
            // TODO: Throw error if take or skip are empty
            return new string[] { "value1", "value2" };
        }

        // GET restaurants/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Restaurant>> Get(string id)
        {
            var restaurant = await service.FindById(id);
            return restaurant;
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
