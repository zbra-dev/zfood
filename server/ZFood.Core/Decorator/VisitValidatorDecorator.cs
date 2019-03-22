using System.Threading.Tasks;
using ZFood.Core.API;
using ZFood.Core.Utils;
using ZFood.Core.Validators;
using ZFood.Model;

namespace ZFood.Core.Decorator
{
    public class VisitValidatorDecorator : IVisitService
    {
        private readonly IVisitService visitService;
        private readonly IVisitValidatorFactory visitValidatorFactory;

        public VisitValidatorDecorator(IVisitService visitService, IVisitValidatorFactory visitValidatorFactory)
        {
            this.visitService = visitService;
            this.visitValidatorFactory = visitValidatorFactory;
        }

        public async Task<Visit> CreateVisit(CreateVisitRequest createVisitRequest)
        {
            await visitValidatorFactory.CreateVisitCreationValidator().ThrowIfNotValid(createVisitRequest);
            return await visitService.CreateVisit(createVisitRequest);
        }

        public async Task UpdateVisit(UpdateVisitRequest updateVisitRequest)
        {
            await visitValidatorFactory.CreateUpdateVisitValidator().ThrowIfNotValid(updateVisitRequest);
            await visitService.UpdateVisit(updateVisitRequest);
        }

        public async Task DeleteVisit(string id)
        {
            await visitValidatorFactory.CreateDeleteVisitValidator().ThrowIfNotValid(id);
            await visitService.DeleteVisit(id);
        }

        public async Task<Visit> FindById(string id)
        {
            return await visitService.FindById(id);
        }

        public async Task<Page<Visit>> Get(int skip, int take, bool count, string query)
        {
            return await visitService.Get(skip, take, count, query);
        }
    }
}
