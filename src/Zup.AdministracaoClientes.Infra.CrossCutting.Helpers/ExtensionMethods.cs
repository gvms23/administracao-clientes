using System.Linq;
using FluentValidation.Results;

namespace Zup.AdministracaoClientes.Infra.CrossCutting.Helpers
{
    public static class ExtensionMethods
    {
        /// <summary>
        /// Retorna as mensagens separadas por vírgula.
        /// </summary>
        /// <param name="validationResult"></param>
        /// <returns></returns>
        public static string GetValidationMessage(this ValidationResult validationResult)
            => string.Join(", ", validationResult.Errors.Select(s => s.ErrorMessage));
    }
}
