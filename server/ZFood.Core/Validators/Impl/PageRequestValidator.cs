using System.Threading.Tasks;
using ZFood.Core.API;
using ZFood.Core.API.Exceptions;

namespace ZFood.Core.Validators.Impl
{
    public class PageRequestValidator : IValidator<PageRequest>
    {
        private const int MinimumValue = 0;

        public Task<ValidatorResult> Validate(PageRequest pageRequest)
        {
            var validatorResult = new ValidatorResult();

            if (pageRequest.Skip < MinimumValue || pageRequest.Take < MinimumValue)
            {
                validatorResult.Exception = new LessThanException(MinimumValue);
                return Task.FromResult(validatorResult);
            }
            return Task.FromResult(validatorResult);
        }
    }
}
