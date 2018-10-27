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

        // GET restaurants
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
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
