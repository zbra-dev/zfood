using System.Linq;
using System.Threading.Tasks;
using ZFood.Core.API;
using ZFood.Core.Extensions;
using ZFood.Model;
using ZFood.Persistence.API;

namespace ZFood.Core
{
    public class VisitService : IVisitService
    {
        private readonly IVisitRepository repository;

        public VisitService(IVisitRepository repository)
        {
            this.repository = repository;
        }

        public async Task<Page<Visit>> Get(int skip, int take, bool count, string query)
        {
            var increasedTake = take++;
            var visitEntities = await repository.Get(skip, increasedTake, query);
            var visits = visitEntities.Select(v => v.ToModel()).ToArray();
            var hasMore = visits.Length == increasedTake;
            int? totalCount = null;

            if (count)
            {
                totalCount = await repository.GetTotalCount();
            }

            return new Page<Visit>
            {
                Items = visits,
                HasMore = hasMore,
                TotalCount = totalCount
            };
        }
    }
}
