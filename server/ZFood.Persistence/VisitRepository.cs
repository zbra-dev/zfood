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

        public async Task<VisitEntity> FindById(string id)
        {
            return await context.Visits
                .Include(v => v.Restaurant)
                .Include(v => v.User)
                .FirstOrDefaultAsync(v => v.Id == id);
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

        public async Task<VisitEntity> CreateVisit(VisitEntity visit)
        {
            var createdVisit = await context.Visits.AddAsync(visit);
            context.SaveChanges();
            return await context.Visits
                .Include(v => v.Restaurant)
                .Include(v => v.User)
                .FirstOrDefaultAsync(v => v.Id == createdVisit.Entity.Id);
        }
    }
}
