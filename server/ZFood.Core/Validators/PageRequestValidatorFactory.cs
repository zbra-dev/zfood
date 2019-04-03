using ZFood.Core.API;
using ZFood.Core.Validators.Impl;

namespace ZFood.Core.Validators
{
    public class PageRequestValidatorFactory : IPageRequestValidatorFactory
    {
        public IValidator<PageRequest> CreatePageRequestValidator()
        {
            return new PageRequestValidator();
        }
    }
}
