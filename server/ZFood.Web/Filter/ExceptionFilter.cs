using Microsoft.AspNetCore.Mvc.Filters;
using ZFood.Core.API.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using ZFood.Web.DTO;

namespace ZFood.Web.Filter
{
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            base.OnException(context);

            context.ExceptionHandled = true;
            var exception = context.Exception;

            if (exception is EntityNotFoundException)
            {
                SetExceptionResult(context, HttpStatusCode.NotFound);
            }
            else if (exception is EntityAlreadyExistsException)
            {
                SetExceptionResult(context, HttpStatusCode.BadRequest);
            }
            else
            {
                SetExceptionResult(context, HttpStatusCode.InternalServerError);
            }
        }

        private static void SetExceptionResult(ExceptionContext context, HttpStatusCode statusCode)
        {
            var message = context.Exception.Message;
            var errorReponse = new ErrorResponseDTO(message);

            context.Result = new JsonResult(errorReponse)
            {
                StatusCode = (int)statusCode
            };
        }
    }
}
