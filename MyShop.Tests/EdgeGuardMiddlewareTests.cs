using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using MyShop.HttpApiServer.Middlewares;
using Xunit;

namespace MyShop.Tests;

public class EdgeGuardMiddlewareTests
{
    [Fact]
    public async Task Request_with_UserAgent_Windows_responses_with_code_403_Forbidden()
    {
        const int expectedStatusCode = 403;
        var middleware = new EdgeGuardMiddleware(_ => Task.CompletedTask);
        var context = new DefaultHttpContext();
        context.Request.Headers.UserAgent = "Windows";

        await middleware.InvokeAsync(context);
        var actualStatusCode = context.Response.StatusCode;
        
        Assert.Equal(expectedStatusCode, actualStatusCode);
    }
    
    [Fact]
    public async Task Request_with_UserAgent_Edge_invokes_next_middleware()
    {
        var hasNextInvoked = false;
        var middleware = new EdgeGuardMiddleware(_ =>
        {
            hasNextInvoked = true;
            return Task.CompletedTask;
        });
        var context = new DefaultHttpContext();
        context.Request.Headers.UserAgent = "Edge";

        await middleware.InvokeAsync(context);

        Assert.True(hasNextInvoked);
    }
    
    [Fact]
    public async Task Request_with_UserAgent_Windows_responses_with_body_h1_Use_Edge()
    {
        const string expectedBodyAsString = "<h1>Use Edge</h1>";
        
        var middleware = new EdgeGuardMiddleware(_ => Task.CompletedTask);
        var context = new DefaultHttpContext();
        
        await using var memory = new MemoryStream();
        context.Response.Body = memory;
        context.Request.Headers.UserAgent = "Windows";
        
        await middleware.InvokeAsync(context);
        memory.Seek(0, SeekOrigin.Begin);
        var actualBodyAsString = await new StreamReader(memory).ReadToEndAsync();

        Assert.Equal(expectedBodyAsString, actualBodyAsString);
    }
}