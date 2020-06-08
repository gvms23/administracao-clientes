using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Zup.AdministracaoClientes.Infra.CrossCutting.ExceptionHandler.Extensions;

namespace Zup.AdministracaoClientes.Infra.CrossCutting.ExceptionHandler.Providers
{
    public class ApiVersionExceptionHandler : DefaultErrorResponseProvider
    {
        private const string UnsupportedApiVersionError = "UnsupportedApiVersion";

        public override IActionResult CreateResponse(ErrorResponseContext context)
        {
            if (context.ErrorCode == UnsupportedApiVersionError)
                throw new ApiException(StatusCodes.Status400BadRequest, "Versão da API não suportada");

            throw new ApiException(StatusCodes.Status400BadRequest, "Erro no versionamento da API");
        }
    }
}