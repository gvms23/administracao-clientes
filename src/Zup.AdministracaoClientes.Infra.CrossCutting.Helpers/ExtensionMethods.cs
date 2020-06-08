using FluentValidation.Results;
using Newtonsoft.Json;
using System.Linq;
using Newtonsoft.Json.Serialization;

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


        public static string ToJson(this object obj)
        {
            return JsonConvert.SerializeObject(obj, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Formatting = Formatting.Indented,
                PreserveReferencesHandling = PreserveReferencesHandling.None,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
        }
    }
}
