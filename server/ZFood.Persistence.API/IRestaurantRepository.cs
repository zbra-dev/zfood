using System.Collections.Generic;
using System.Threading.Tasks;
using ZFood.Persistence.API.Entity;

namespace ZFood.Persistence.API
{
    public interface IRestaurantRepository
    {
        Task<RestaurantEntity> FindById(string id);

        Task<RestaurantEntity[]> Get(int take, int skip, string query);
    }
}
