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

        public VisitRepository(ZFoodDbContext context)
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
            await context.SaveChangesAsync();
            return await FindById(createdVisit.Entity.Id);
        }

        public async Task UpdateVisit(VisitEntity visit)
        {
            var entry = context.Entry(visit);
            entry.State = EntityState.Modified;
            await context.SaveChangesAsync();
        }

        public async Task DeleteVisit(string id)
        {
            var entry = context.Entry(new VisitEntity() { Id = id });
            entry.State = EntityState.Deleted;
            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // TODO: Log error
            }
        }
    }
}
