using System.Threading.Tasks;
using ZFood.Core.Validators;

namespace ZFood.Core.Utils
{
    public static class ValidatorUtils
    {
        public static async Task ThrowIfNotValid<T> (this IValidator<T> validator, T item)
        {
            var validatorResult = await validator.Validate(item);
            if (!validatorResult.Success)
            {
                throw validatorResult.Exception;
            }
        }
    }
}
