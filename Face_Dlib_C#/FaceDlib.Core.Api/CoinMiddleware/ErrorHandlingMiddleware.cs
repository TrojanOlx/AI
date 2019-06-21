using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace FaceDlib.Core.Api
{
    public class ErrorHandlingMiddleware
    {
        static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private readonly RequestDelegate next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context /* other dependencies */)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                HandleExceptionAsync(context, ex);
            }
        }

        private static void HandleExceptionAsync(HttpContext context, Exception error)
        {
            var statusCode = (int)HttpStatusCode.InternalServerError;
            if (error is UnauthorizedAccessException)
            {
                // to prevent login prompt in IIS
                // which will appear when returning 401.
                statusCode = (int)HttpStatusCode.Forbidden;
            }
            if (statusCode != 404 && statusCode != 403)
            {
                logger.Error(error);
            }
        }
    }
}
