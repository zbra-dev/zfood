using System;
using System.Threading.Tasks;
using ZFood.Core.API;

namespace ZFood.Core.Validators.Impl
{
    public class CreateRestaurantValidator : IValidator<CreateRestaurantRequest>
    {
        public Task<ValidatorResult> Validate(CreateRestaurantRequest request)
        {
            return Task.Factory.StartNew(() =>
            {
                var validationResult = new ValidatorResult();
                if (request == null)
                {
                    validationResult.Exception = new ArgumentNullException(nameof(request));
                }
                return validationResult;
            });
        }
    }
}
