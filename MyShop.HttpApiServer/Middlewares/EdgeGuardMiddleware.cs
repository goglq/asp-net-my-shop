﻿using System.Text;
using System.Text.RegularExpressions;

namespace MyShop.HttpApiServer.Middlewares;

public class EdgeGuardMiddleware : IMiddleware
{
    
    
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var edgeRegex = new Regex("Edg");
        if (edgeRegex.IsMatch(context.Request.Headers.UserAgent))
        {
            await next(context);
        }
        else
        {
            context.Response.StatusCode = 403;

            var bytes = Encoding.UTF8.GetBytes("<h1>Use Edge</h1>");
            await context.Response.Body.WriteAsync(bytes);
        }
    }
}