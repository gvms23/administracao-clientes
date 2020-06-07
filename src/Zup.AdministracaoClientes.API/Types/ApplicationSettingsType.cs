namespace Zup.AdministracaoClientes.API.Types
{
    public class ApplicationSettingsType
    {
        public const string Key = "ApplicationSettings";

        public short? ApiVersion { get; set; }
        public bool TestInMemoryDatabase { get; set; }
    }
}
