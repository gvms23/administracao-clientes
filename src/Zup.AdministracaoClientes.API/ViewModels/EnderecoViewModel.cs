namespace Zup.AdministracaoClientes.API.ViewModels
{
    public class EnderecoViewModel
    {
        public string Rua { get; set; }
        public short? Numero { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Pais { get; set; }
        public string CEP { get; set; }
    }
}