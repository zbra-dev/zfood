using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Text;

namespace ZFood.Web.Filter
{
    public class ModelStateErrorResponse
    {
        private const string ErrorMessage = "The request is invalid.";

        public string Message { get; private set; }

        public ModelStateErrorResponse(ActionExecutingContext context)
        {
            var messageBuilder = new StringBuilder(ErrorMessage);
            foreach (var item in context.ModelState)
            {
                foreach (var error in item.Value.Errors)
                {
                    messageBuilder.Append($" {item.Key}: {error.ErrorMessage}");
                }
            }
            Message = messageBuilder.ToString();
        }
    }
}