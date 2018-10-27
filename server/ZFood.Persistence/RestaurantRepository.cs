using System;
using System.Threading.Tasks;
using ZFood.Persistence.API;
using ZFood.Persistence.API.Entity;

namespace ZFood.Persistence
{
    public class RestaurantRepository : IRestaurantRepository
    {
        public async Task<RestaurantEntity> FindById(string id)
        {
            // TODO: Actually return it from database
            return await Task.Factory.StartNew(() =>
            {
                return new RestaurantEntity
                {
                    Id = $"test_restaurant_{id}",
                    Name = $"Test Restaurant {id}"
                };
            });
        }
    }
}
