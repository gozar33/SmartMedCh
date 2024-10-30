using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Web.Http.ExceptionHandling;

namespace SmartMed.API.ErrorHandling
{
    public sealed class GlobalExceptionHandler 
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionHandler(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                //log exception here
                await _next(context);
            }
            catch (ValidationException ex)
            {
                await HandleExceptionAsync(context, HttpStatusCode.BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, HttpStatusCode.InternalServerError, "An unexpected error occurred.",ex.InnerException?.Message);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, HttpStatusCode statusCode, string message,string? detail="")
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            var response = new ErrorResponse(message, detail);

            return context.Response.WriteAsJsonAsync(response);
        }
    }
}
