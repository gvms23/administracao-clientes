using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace Zup.AdministracaoClientes.Infra.CrossCutting.ExceptionHandler.Extensions
{
    /// <summary>
    /// API exception customizada para a aplicação, podendo guardar o tipo da exception através do recurso {System.Net.HttpStatusCode}.
    /// <para>Através dessa exception, o retorno da API será com o status em questão.</para>
    /// </summary>
    /// <inheritdoc />
    public class ApiException : Exception
    {
        public ApiException() { }

        public ApiException(
            int statusCode = StatusCodes.Status500InternalServerError,
            params string[] messages
            )
        {
            Messages = messages;
            StatusCode = statusCode;
        }

        public int StatusCode { get; set; }

        public IEnumerable<string> Messages { get; set; }
    }
}
