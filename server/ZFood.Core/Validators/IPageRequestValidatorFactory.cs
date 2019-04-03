using ZFood.Core.API;

namespace ZFood.Core.Validators
{
    public interface IPageRequestValidatorFactory
    {
        IValidator<PageRequest> CreatePageRequestValidator();
    }
}
