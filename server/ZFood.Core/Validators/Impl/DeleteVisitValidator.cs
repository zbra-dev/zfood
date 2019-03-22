using System;
using System.Threading.Tasks;
using ZFood.Persistence.API;

namespace ZFood.Core.Validators.Impl
{
    class DeleteVisitValidator : IValidator<string>
    {
        private readonly IVisitRepository visitRepository;

        public DeleteVisitValidator(IVisitRepository visitRepository)
        {
            this.visitRepository = visitRepository;
        }

        public async Task<ValidatorResult> Validate(string id)
        {
            var validationResult = new ValidatorResult();
            if (string.IsNullOrWhiteSpace(id))
            {
                validationResult.Exception = new ArgumentNullException(nameof(id));
                return validationResult;
            }
            return await Task.FromResult(validationResult);
        }
    }
}