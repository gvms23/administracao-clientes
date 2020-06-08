namespace Zup.AdministracaoClientes.API.ViewModels
{
    public class EnderecoViewModel
    {
        public EnderecoViewModel(
            string rua, 
            short? numero, 
            string bairro, 
            string cidade, 
            string estado, 
            string pais, 
            string cep)
        {
            Rua = rua;
            Numero = numero;
            Bairro = bairro;
            Cidade = cidade;
            Estado = estado;
            Pais = pais;
            CEP = cep;
        }
        public string Rua { get; set; }
        public short? Numero { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Pais { get; set; }
        public string CEP { get; set; }
    }
}