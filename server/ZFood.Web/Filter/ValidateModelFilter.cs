using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System.Text;

namespace ZFood.Web.Filter
{
    public class ValidateModelFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            if (!context.ModelState.IsValid)
            {
                var request = new StringBuilder();
                var body = context.ActionArguments.ContainsKey("request")
                    ? JsonConvert.SerializeObject(context.ActionArguments["request"])
                    : "N/A";
                request
                    .Append($"Method: {context.HttpContext.Request.Method}").AppendLine()
                    .Append($"RequestUri: {context.HttpContext.Request.Path}").AppendLine()
                    .Append($"Body: {body}").AppendLine();

                var errorResponse = new ModelStateErrorResponse(context);
                context.Result = new BadRequestObjectResult(errorResponse);
            }
        }
    }
}
