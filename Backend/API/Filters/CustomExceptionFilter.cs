﻿using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;

namespace API.Filters
{
    public class CustomExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            // Handle the exception and generate a response
            var response = new { error = context.Exception.Message };
            var payload = JsonConvert.SerializeObject(response);
            context.Result = new ContentResult
            {
                Content = payload,
                ContentType = "application/json",
                StatusCode = (int)HttpStatusCode.InternalServerError
            };
            context.ExceptionHandled = true;
        }
    }
}
