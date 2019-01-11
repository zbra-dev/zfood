using ZFood.Core.API;

namespace ZFood.Core.Validators
{
    public interface IVisitValidatorFactory
    {
        IValidator<CreateVisitRequest> CreateVisitCreationValidator();

        IValidator<UpdateVisitRequest> CreateUpdateVisitValidator();
    }
}
