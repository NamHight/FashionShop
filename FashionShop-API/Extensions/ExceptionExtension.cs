using System.Net;
using FashionShop_API.Exceptions;
using FashionShop_API.Exceptions.Base;
using FashionShop_API.Models.Errors;
using FashionShop_API.Services.ServiceLogger;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace FashionShop_API.Extensions;

public static class ExceptionExtension
{
    public static void ConfigureException(this WebApplication app, ILoggerManager logger)
    {
        app.UseExceptionHandler(optError 
            => optError.Run(async context =>
            {
                context.Response.ContentType = "application/json";
                var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                if (contextFeature != null)
                {
                    context.Response.StatusCode = contextFeature.Error switch
                    {
                        NotFoundException => StatusCodes.Status404NotFound,
                        ExistedException => StatusCodes.Status409Conflict,
                        UnauthorizedException => StatusCodes.Status401Unauthorized,
                        BadRequestException => StatusCodes.Status400BadRequest,
                        ManyRequestException => StatusCodes.Status429TooManyRequests,
                        ForbiddenException => StatusCodes.Status403Forbidden,
                        _ => StatusCodes.Status500InternalServerError
                    };
                    logger.LogError($"Something went wrong: {contextFeature.Error}");
                    await context.Response.WriteAsync(new ErrorDetails
                    {
                        StatusCode = context.Response.StatusCode,
                        Message = contextFeature.Error.Message
                    }.ToString());
                }
            }) );
    }
}