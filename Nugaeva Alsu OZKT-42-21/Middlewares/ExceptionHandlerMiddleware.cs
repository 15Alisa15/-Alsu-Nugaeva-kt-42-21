using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Nugaeva_Alsu_OZKT_42_21.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;

        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Unhandled exception occurred");

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var response = new ResponseModel<object>
                {
                    Succeeded = false,
                    Message = "An unexpected error occurred",
                    Errors = new List<string> { exception.Message }
                };

                switch (exception)
                {
                    case ApplicationException appEx:
                        response.Errors = new List<string> { appEx.Message };
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    case UnauthorizedAccessException unauthorizedEx:
                        response.Errors = new List<string> { unauthorizedEx.Message };
                        response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        break;
                }

                await context.Response.WriteAsJsonAsync(response);
            }
        }
    }

    public class ResponseModel<T>
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; }
        public int StatusCode { get; set; }
        public T Data { get; set; }

        public ResponseModel()
        {
        }

        public ResponseModel(T data, string message = null)
        {
            Succeeded = true;
            Message = message;
            Data = data;
        }

        public ResponseModel(string message)
        {
            Succeeded = true;
            Message = message;
        }
    }
}