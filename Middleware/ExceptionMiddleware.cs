using System.Net;
using System.Text.Json;

namespace BtgSimuladorCredito.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
{
    try
    {
        await _next(context);
    }
    catch (ArgumentException ex)
    {
        if (context.Request.Path.StartsWithSegments("/api"))
        {
            await HandleExceptionAsync(context, ex.Message, HttpStatusCode.BadRequest);
        }
        else
        {
            throw;
        }
    }
    catch (System.Exception)
    {
        if (context.Request.Path.StartsWithSegments("/api"))
        {
            await HandleExceptionAsync(context, "Erro interno no servidor.", HttpStatusCode.InternalServerError);
        }
        else
        {
            throw;
        }
    }
}

    private static Task HandleExceptionAsync(HttpContext context, string message, HttpStatusCode statusCode)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        var response = new
        {
            success = false,
            message
        };

        var json = JsonSerializer.Serialize(response);

        return context.Response.WriteAsync(json);
    }
}
