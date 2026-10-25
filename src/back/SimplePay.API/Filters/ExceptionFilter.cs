using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SimplePay.Communication.Responses;
using SimplePay.Exception;
using SimplePay.Exception.ExceptionsBase;

namespace SimplePay.API.Filters;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is SimplePayException simplePayException)
        {
            context.HttpContext.Response.StatusCode = simplePayException.StatusCode();

            context.Result = new ObjectResult(new ErrorResponse(simplePayException.GetErrorMessages()));
        }
        else
        {
            context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

            context.Result = new ObjectResult(new ErrorResponse(MessagesExceptionResource.UNKNOWN_ERROR));
        }
    }
}