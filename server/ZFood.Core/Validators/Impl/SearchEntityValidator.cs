using System.Threading.Tasks;
using ZFood.Core.API;
using ZFood.Core.API.Exceptions;

namespace ZFood.Core.Validators.Impl
{
    class SearchEntityValidator : IValidator<PageRequest>
    {
        private const int MinimumValue = 0;

        public Task<ValidatorResult> Validate(PageRequest pageRequest)
        {
            return Task.Factory.StartNew(() =>
            {
                var validatorResult = new ValidatorResult();

                if (pageRequest.Skip < MinimumValue || pageRequest.Take < MinimumValue)
                {
                    validatorResult.Exception = new LessThanException(MinimumValue);
                    return validatorResult;
                }

                return validatorResult;
            });
        }
    }
}
