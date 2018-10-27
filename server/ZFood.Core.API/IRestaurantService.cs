using System.Threading.Tasks;
using ZFood.Model;

namespace ZFood.Core.API
{
    public interface IRestaurantService
    {
        Task<Restaurant> FindById(string id);
    }
}
