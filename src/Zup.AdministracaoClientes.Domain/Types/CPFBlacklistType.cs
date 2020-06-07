using System.Collections.Generic;

namespace Zup.AdministracaoClientes.Domain.Types
{
    public class CPFBlacklistType
    {
        public const string Key = "CPFBlackList";

        public List<string> CPFs { get; set; } = new List<string>();
    }
}
