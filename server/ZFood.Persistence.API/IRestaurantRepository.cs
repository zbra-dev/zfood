using System.Collections.Generic;
using System.Threading.Tasks;
using ZFood.Persistence.API.Entity;

namespace ZFood.Persistence.API
{
    public interface IRestaurantRepository
    {
        Task<RestaurantEntity> FindById(string id);

        Task<RestaurantEntity[]> Get(int take, int skip, string query);

        Task<int> GetTotalCount();

        Task Delete(string id);

        Task<RestaurantEntity> CreateRestaurant(RestaurantEntity restaurant);

        Task UpdateRestaurant(RestaurantEntity restaurant);
    }
}
