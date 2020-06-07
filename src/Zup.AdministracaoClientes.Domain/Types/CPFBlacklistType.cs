using System.Collections.Generic;

namespace Zup.AdministracaoClientes.Domain.Types
{
    public class CPFBlacklistType
    {
        public const string KEY = "CPFBlackList";

        public List<string> CPFs { get; set; } = new List<string>();
    }
}
