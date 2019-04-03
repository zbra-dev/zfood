using System.Threading.Tasks;
using ZFood.Model;

namespace ZFood.Core.API
{
    public interface IVisitService
    {
        Task<Visit> FindById(string id);

        Task<Page<Visit>> Get(PageRequest pageRequest);

        Task<Visit> CreateVisit(CreateVisitRequest visitRequest);

        Task UpdateVisit(UpdateVisitRequest visitRequest);

        Task DeleteVisit(string id);
    }
}
