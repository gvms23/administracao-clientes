namespace Zup.AdministracaoClientes.API.Types
{
    public class ApplicationSettingsType
    {
        public const string KEY = "ApplicationSettings";

        public short? ApiVersion { get; set; }
        public bool TestInMemoryDatabase { get; set; }
    }
}
