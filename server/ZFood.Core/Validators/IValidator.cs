using System.Threading.Tasks;

namespace ZFood.Core.Validators
{
    public interface IValidator<T>
    {
        Task<ValidatorResult> Validate(T item);
    }
}
