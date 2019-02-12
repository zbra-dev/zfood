using Microsoft.AspNetCore.Mvc.Filters;
using ZFood.Core.API.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using ZFood.Web.DTO;
using log4net;

namespace ZFood.Web.Filter
{
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(ExceptionFilter));

        public override void OnException(ExceptionContext context)
        {
            base.OnException(context);

            context.ExceptionHandled = true;
            var exception = context.Exception;
            var message = context.Exception.Message;

            if (exception is EntityNotFoundException entityNotFoundException)
            {
                log.Error($"{entityNotFoundException.Message}", exception);
                SetExceptionResult(context, HttpStatusCode.NotFound);
            }
            else if (exception is ValidationException validationException)
            {
                log.Error($"{validationException.Message}", exception);
                SetExceptionResult(context, HttpStatusCode.BadRequest);
            }
            else
            {
                log.Error("Internal Server Error", exception);
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
