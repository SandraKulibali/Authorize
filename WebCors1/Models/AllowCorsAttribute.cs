using Microsoft.AspNetCore.Mvc.Filters;

namespace WebCors1.Models
{
    public class AllowCorsAttribute : Attribute, IActionFilter
    {
        private List<string> _domains = new List<string>()
        {"https://localhost:7075" };
        public AllowCorsAttribute(params string[] domains)
        {
            _domains.AddRange(domains);
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
          
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var data = context.HttpContext.Request;
            var referUrl = data.Headers["Referer"].ToString();
            if(!string.IsNullOrWhiteSpace(referUrl) && _domains.Contains(referUrl))
            {
                context.HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", referUrl);
            }
            else
            {
                context.HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");
            }
            
        }
    }
}
