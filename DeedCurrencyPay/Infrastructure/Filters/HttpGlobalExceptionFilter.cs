using DeedCurrencyPay.Infrastructure.ActionResults;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;
using System.Net;

namespace DeedCurrencyPay.Infrastructure.Filters
{
    public class HttpGlobalExceptionFilter : IExceptionFilter
    {
        private readonly IWebHostEnvironment env;

        public HttpGlobalExceptionFilter(IWebHostEnvironment env)
        {
            this.env = env;
        }

        public void OnException(ExceptionContext context)
        {
            //todo add domainException section

            var json = new JsonErrorResponse
            {
                Messages = new[] { "An error occur.Try it again." }
            };

            if (env.IsDevelopment())
            {
                json.DeveloperMessage = context.Exception;
            }

            context.Result = new InternalServerErrorObjectResult(json);
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.ExceptionHandled = true;
        }
    }
}