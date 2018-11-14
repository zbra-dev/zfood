using System.Threading.Tasks;
using ZFood.Model;

namespace ZFood.Core.API
{
    public interface IRestaurantService
    {
        Task<Restaurant> FindById(string id);

        Task<Page<Restaurant>> Get(int take, int skip, bool count, string query);

        Task Delete(string id);

        Task<Restaurant> CreateRestaurant(CreateRestaurantRequest restaurant);
    }
}
