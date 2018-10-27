using System.Threading.Tasks;
using ZFood.Persistence.API.Entity;

namespace ZFood.Persistence.API
{
    public interface IRestaurantRepository
    {
        Task<RestaurantEntity> FindById(string id);
    }
}
