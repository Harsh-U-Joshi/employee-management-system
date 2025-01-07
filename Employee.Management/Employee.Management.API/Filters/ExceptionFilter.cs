using Employee.Management.API.Common;
using Employee.Management.Application.Contracts.Persistence;
using Employee.Management.Application.Exceptions;
using Employee.Management.Domain.Entities.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace Employee.Management.API.Filters
{
    public class ExceptionFilter : IAsyncExceptionFilter
    {
        private readonly GenericResponse response;

        private readonly IErrorLogger _errorLogger;

        public ExceptionFilter(IErrorLogger errorLogger)
        {
            response = new GenericResponse();

            _errorLogger = errorLogger;
        }

        public async Task OnExceptionAsync(ExceptionContext context)
        {
            int statusCode = HttpStatusCode.InternalServerError.GetHashCode();

            string errorMessage = "Sorry, we were unable to complete your request. Please contact Support";

            Exception exception = context.Exception;

            if (context.Exception is GenericException genericException)
            {
                statusCode = genericException.StatusCode;
                errorMessage = genericException.Message;
            }
            else
            {
                var errorLog = new ErrorLog
                {
                    Message = exception.Message,
                    StackTrace = exception.StackTrace,
                    Timestamp = DateTime.UtcNow,
                    Context = context.HttpContext?.Request.Path
                };

                await _errorLogger.SaveErrorLogs(errorLog);
            }

            response.Code = statusCode;
            response.Message = errorMessage;
            response.Success = false;

            context.Result = new OkObjectResult(response);
        }
    }
}
