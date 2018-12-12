using System.Threading.Tasks;
using ZFood.Persistence.API.Entity;

namespace ZFood.Persistence.API
{
    public interface IVisitRepository
    {
        Task<VisitEntity> FindById(string id);

        Task<VisitEntity[]> Get(int skip, int take, string query);

        Task<int> GetTotalCount();

        Task<VisitEntity> CreateVisit(VisitEntity visit);

        Task DeleteVisit(string id);
    }
}
