using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Net.Mime;
using Zup.AdministracaoClientes.Infra.CrossCutting.ExceptionHandler.Extensions;
using Zup.AdministracaoClientes.Infra.CrossCutting.Helpers;

namespace Zup.AdministracaoClientes.Infra.CrossCutting.ExceptionHandler.Providers
{
    public static class ExceptionHandlerMiddleware
    {
        public static IApplicationBuilder UseExceptionHandlerMiddleware(
            this IApplicationBuilder builder)
        {
            return builder.UseExceptionHandler(new ExceptionHandlerOptions
            {
                ExceptionHandler = async context =>
                {
                    IExceptionHandlerPathFeature _throwedException = context.Features.Get<IExceptionHandlerPathFeature>();

                    Exception _exception;

                    if (_throwedException.Error is ApiException)
                        _exception = _throwedException.Error;
                    else if (_throwedException.Error?.InnerException != null)
                        _exception = _throwedException.Error.InnerException;
                    else
                        _exception = _throwedException.Error;

                    if (_exception == null)
                        return;

                    IEnumerable<string> _exceptionMessages;

                    int _httpStatusCode = StatusCodes.Status500InternalServerError;

                    if (_exception is ApiException _apiException)
                    {
                        _httpStatusCode = _apiException.StatusCode;
                        _exceptionMessages = _apiException.Messages ?? new List<string> { _apiException.Message };
                    }
                    else
                        _exceptionMessages = new List<string> { _exception.Message };

                    context.Response.StatusCode = _httpStatusCode;
                    context.Response.ContentType = MediaTypeNames.Application.Json;

                    string _jsonResponse = new
                    {
                        HttpStatusCode = _httpStatusCode,
                        Messages = _exceptionMessages
                    }.ToJson();

                    await context.Response.WriteAsync(_jsonResponse);
                }
            });
        }
    }
}
