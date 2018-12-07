using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using ZFood.Persistence.API;
using ZFood.Persistence.API.Entity;

namespace ZFood.Persistence
{
    public class VisitRepository : IVisitRepository
    {
        private readonly ZFoodDbContext context;

        public VisitRepository (ZFoodDbContext context)
        {
            this.context = context;
        }

        public Task<VisitEntity[]> Get(int skip, int take, string query)
        {
            var items = context.Visits.Skip(skip).Take(take);
            if (!string.IsNullOrEmpty(query))
            {
                items = items.Where(i => i.Id.StartsWith(query));
            }

            return items.Include(v => v.Restaurant).Include(v => v.User).ToArrayAsync();
        }

        public async Task<int> GetTotalCount()
        {
            return await context.Visits.CountAsync();
        }
    }
}
