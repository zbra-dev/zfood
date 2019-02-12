using System;
using System.Linq;
using System.Threading.Tasks;
using ZFood.Core.API;
using ZFood.Core.API.Exceptions;
using ZFood.Core.Extensions;
using ZFood.Model;
using ZFood.Persistence.API;

namespace ZFood.Core
{
    public class VisitService : IVisitService
    {
        private readonly IVisitRepository visitRepository;
        private readonly IRestaurantRepository restaurantRepository;
        private readonly IUserRepository userRepository;

        public VisitService(IVisitRepository visitRepository,
                            IRestaurantRepository restaurantRepository,
                            IUserRepository userRepository)
        {
            this.visitRepository = visitRepository;
            this.restaurantRepository = restaurantRepository;
            this.userRepository = userRepository;
        }

        public async Task<Visit> FindById(string id)
        {
            var visitEntity = await visitRepository.FindById(id);
            return visitEntity?.ToModel();
        }

        public async Task<Page<Visit>> Get(int skip, int take, bool count, string query)
        {
            var increasedTake = take++;
            var visitEntities = await visitRepository.Get(skip, increasedTake, query);
            var visits = visitEntities.Select(v => v.ToModel()).ToArray();
            var hasMore = visits.Length == increasedTake;
            int? totalCount = null;

            if (count)
            {
                totalCount = await visitRepository.GetTotalCount();
            }

            return new Page<Visit>
            {
                Items = visits,
                HasMore = hasMore,
                TotalCount = totalCount
            };
        }

        public async Task<Visit> CreateVisit(CreateVisitRequest visitRequest)
        {
            var visitEntity = visitRequest.ToEntity();
            var createdVisit = await visitRepository.CreateVisit(visitEntity);
            return createdVisit.ToModel();
        }

        public async Task UpdateVisit(UpdateVisitRequest visitRequest)
        {
            if (visitRequest == null)
            {
                throw new ArgumentNullException(nameof(visitRequest));
            }

            var visit = await FindById(visitRequest.Id);
            if (visit == null)
            {
                throw new EntityNotFoundException(typeof(Visit), visitRequest.Id);
            }

            var restaurant = await restaurantRepository.FindById(visitRequest.RestaurantId);
            if (restaurant == null)
            {
                throw new EntityNotFoundException(typeof(Restaurant), visitRequest.RestaurantId);
            }

            var user = await userRepository.FindById(visitRequest.UserId);
            if (user == null)
            {
                throw new EntityNotFoundException(typeof(User), visitRequest.UserId);
            }

            await visitRepository.UpdateVisit(visitRequest.ToEntity());
        }

        public async Task DeleteVisit(string id)
        {
            await visitRepository.DeleteVisit(id);
        }
    }
}
