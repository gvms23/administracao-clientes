using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using Newtonsoft.Json.Linq;
using Xunit;
using Zup.AdministracaoClientes.API;
using Zup.AdministracaoClientes.API.ViewModels;

namespace Zup.AdministracaoClientes.Tests.IntegrationTests
{
    public class ClienteIntegrationTest
    {
        private readonly HttpClient _httpClient;

        public ClienteIntegrationTest()
        {
            DirectoryInfo _directoryInfo = new DirectoryInfo(typeof(ClienteIntegrationTest).Assembly.Location).Parent;

            TestServer _server = new TestServer(new WebHostBuilder()
            .UseEnvironment("Development")
            .UseContentRoot(_directoryInfo?.FullName)
            .UseConfiguration(
                new ConfigurationBuilder()
                    .SetBasePath(_directoryInfo?.FullName)
                    .AddJsonFile("appsettings.json")
                    .Build()
            )
            .UseStartup<Startup>());

            _httpClient = _server.CreateClient();
        }

        [Fact(DisplayName = "Cadastrar Cliente (Válido)")]
        public async Task CadastrarClientesAsync_Retorna201Created()
        {
            //Arrange
            var _enderecos = new List<EnderecoViewModel>()
            {
                new EnderecoViewModel("Av. São Jorge",
                    50,
                    "Cidade Salvador",
                    "Jacareí",
                    "SP",
                    "Brasil",
                    "12312000")
            };

            var _telefones = new List<long>
            {
                12985654585
            };

            var _cliente = new CadastrarClienteViewModel(
                nome: "João da Silva",
                email: "joao.dasilva@gmail.com",
                cpf: "455.249.280-23",
                _enderecos,
                _telefones);

            var _content = new StringContent(
                JsonConvert.SerializeObject(_cliente),
                Encoding.UTF8,
                MediaTypeNames.Application.Json);


            HttpRequestMessage _request = new HttpRequestMessage(
                HttpMethod.Post, "/api/v1/clientes")
            {
                Content = _content
            };

            // Act
            HttpResponseMessage _response = await _httpClient.SendAsync(_request);

            if (!_response.IsSuccessStatusCode)

                // Assert
                _response.EnsureSuccessStatusCode();

            Assert.Equal(HttpStatusCode.Created, _response.StatusCode);
        }


        [Fact(DisplayName = "Cadastrar Cliente (CPF Blacklist)")]
        public async Task CadastrarClienteCPFBlacklistAsync_Retorna403Forbidden()
        {

            //Arrange
            var _enderecos = new List<EnderecoViewModel>()
            {
                new EnderecoViewModel("Av. São Jorge",
                    50,
                    "Cidade Salvador",
                    "Jacareí",
                    "SP",
                    "Brasil",
                    "12312000")
            };

            var _telefones = new List<long>
            {
                12985654585
            };

            var _cliente = new CadastrarClienteViewModel(
                nome: "Manoel da Silva",
                email: "manoel.dasilva@gmail.com",
                cpf: "775.945.550-07",
                _enderecos,
                _telefones);

            var _content = new StringContent(
                JsonConvert.SerializeObject(_cliente),
                Encoding.UTF8,
                MediaTypeNames.Application.Json);


            HttpRequestMessage _request = new HttpRequestMessage(
                HttpMethod.Post, "/api/v1/clientes")
            {
                Content = _content
            };

            // Act
            HttpResponseMessage _response = await _httpClient.SendAsync(_request);

            // Assert
            Assert.Equal(HttpStatusCode.Forbidden, _response.StatusCode);
        }

        [Fact(DisplayName = "Cadastrar Cliente (CPF Duplicado)")]
        public async Task CadastrarClienteCPFDuplicadoAsync_Retorna409Conflict()
        {

            //Arrange
            var _enderecos = new List<EnderecoViewModel>()
            {
                new EnderecoViewModel("Av. São Jorge",
                    50,
                    "Cidade Salvador",
                    "Jacareí",
                    "SP",
                    "Brasil",
                    "12312000")
            };

            var _telefones = new List<long>
            {
                12985654585
            };

            var _cliente = new CadastrarClienteViewModel(
                nome: "Joana da Silva",
                email: "joana.dasilva@gmail.com",
                cpf: "939.553.610-12",
                _enderecos,
                _telefones);


            HttpRequestMessage _requestOne = new HttpRequestMessage(
                HttpMethod.Post, "/api/v1/clientes")
            {
                Content = new StringContent(
                    JsonConvert.SerializeObject(_cliente),
                    Encoding.UTF8,
                    MediaTypeNames.Application.Json)
            };

            // Executa a primeira vez
            (await _httpClient.SendAsync(_requestOne)).EnsureSuccessStatusCode();

            // Executa a segunda vez (força conflito)

            _cliente.Email = "outro-email-sem-conflitos@gmail.com";

            // CPF continua o mesmo
            HttpRequestMessage _requestDuplicated = new HttpRequestMessage(
                HttpMethod.Post, "/api/v1/clientes")
            {
                Content = new StringContent(
                    JsonConvert.SerializeObject(_cliente),
                    Encoding.UTF8,
                    MediaTypeNames.Application.Json)
            };
            HttpResponseMessage _responseDuplicated = await _httpClient.SendAsync(_requestDuplicated);

            // Assert
            Assert.Equal(HttpStatusCode.Conflict, _responseDuplicated.StatusCode);
        }

        [Fact(DisplayName = "Cadastrar Cliente (E-mail Duplicado)")]
        public async Task CadastrarClienteEmailDuplicadoAsync_Retorna409Conflict()
        {

            //Arrange
            var _enderecos = new List<EnderecoViewModel>()
            {
                new EnderecoViewModel("Av. São Jorge",
                    50,
                    "Cidade Salvador",
                    "Jacareí",
                    "SP",
                    "Brasil",
                    "12312000")
            };

            var _telefones = new List<long>
            {
                12985654585
            };

            var _cliente = new CadastrarClienteViewModel(
                nome: "Ana da Silva",
                email: "ana.dasilva@gmail.com",
                cpf: "640.335.790-52",
                _enderecos,
                _telefones);


            HttpRequestMessage _requestOne = new HttpRequestMessage(
                HttpMethod.Post, "/api/v1/clientes")
            {
                Content = new StringContent(
                    JsonConvert.SerializeObject(_cliente),
                    Encoding.UTF8,
                    MediaTypeNames.Application.Json)
            };

            // Executa a primeira vez
            (await _httpClient.SendAsync(_requestOne)).EnsureSuccessStatusCode();

            // Executa a segunda vez (força conflito)
            _cliente.CPF = "871.232.500-79";

            // E-mail continua o mesmo
            HttpRequestMessage _requestDuplicated = new HttpRequestMessage(
                HttpMethod.Post, "/api/v1/clientes")
            {
                Content = new StringContent(
                    JsonConvert.SerializeObject(_cliente),
                    Encoding.UTF8,
                    MediaTypeNames.Application.Json)
            };
            HttpResponseMessage _responseDuplicated = await _httpClient.SendAsync(_requestDuplicated);

            // Assert
            Assert.Equal(HttpStatusCode.Conflict, _responseDuplicated.StatusCode);
        }


        [Fact(DisplayName = "Obter Clientes Async")]
        public async Task ObterClientesAsync_Retorna200Ok()
        {
            // Arrange
            HttpRequestMessage _request = new HttpRequestMessage(HttpMethod.Get, "/api/v1/clientes");

            // Act
            HttpResponseMessage _response = await _httpClient.SendAsync(_request);

            // Assert
            _response.EnsureSuccessStatusCode();

            Assert.Equal(HttpStatusCode.OK, _response.StatusCode);
        }


        [Fact(DisplayName = "Obter Cliente por Id Async")]
        public async Task ObterClientePorIdAsync_Retorna200Ok()
        {

            //Arrange
            #region Pre-arrange
            var _enderecos = new List<EnderecoViewModel>()
            {
                new EnderecoViewModel("Av. São Jorge",
                    50,
                    "Cidade Salvador",
                    "Jacareí",
                    "SP",
                    "Brasil",
                    "12312000")
            };

            var _telefones = new List<long>
            {
                12985654585
            };

            var _cliente = new CadastrarClienteViewModel(
                nome: "Júnior da Silva",
                email: "junior.dasilva@gmail.com",
                cpf: "035.123.050-59",
                _enderecos,
                _telefones);

            var _content = new StringContent(
                JsonConvert.SerializeObject(_cliente),
                Encoding.UTF8,
                MediaTypeNames.Application.Json);


            HttpRequestMessage _requestCreate = new HttpRequestMessage(
                HttpMethod.Post, "/api/v1/clientes")
            {
                Content = _content
            };

            HttpResponseMessage _responseCreate = await _httpClient.SendAsync(_requestCreate);

            _responseCreate.EnsureSuccessStatusCode();
            #endregion

            #region Arrange

            string _resultCreate = await _responseCreate.Content.ReadAsStringAsync();

            JObject _clienteObject = JObject.Parse(_resultCreate);

            HttpRequestMessage _requestGetById = new HttpRequestMessage(HttpMethod.Get, $"/api/v1/clientes/{_clienteObject["id"]}");

            #endregion

            // Act
            HttpResponseMessage _responseGetById = await _httpClient.SendAsync(_requestGetById);

            // Assert
            _responseGetById.EnsureSuccessStatusCode();

            Assert.Equal(HttpStatusCode.OK, _responseGetById.StatusCode);
        }


        [Fact(DisplayName = "Excluir Cliente por Id Async")]
        public async Task ExcluirClientePorIdAsync_Retorna200Ok()
        {

            //Arrange
            #region Pre-arrange
            var _enderecos = new List<EnderecoViewModel>()
            {
                new EnderecoViewModel("Av. São Jorge",
                    50,
                    "Cidade Salvador",
                    "Jacareí",
                    "SP",
                    "Brasil",
                    "12312000")
            };

            var _telefones = new List<long>
            {
                12985654585
            };

            var _cliente = new CadastrarClienteViewModel(
                nome: "Manoel da Silva",
                email: "manoel.dasilva@gmail.com",
                cpf: "419.696.370-64",
                _enderecos,
                _telefones);

            var _content = new StringContent(
                JsonConvert.SerializeObject(_cliente),
                Encoding.UTF8,
                MediaTypeNames.Application.Json);


            HttpRequestMessage _requestCreate = new HttpRequestMessage(
                HttpMethod.Post, "/api/v1/clientes")
            {
                Content = _content
            };

            HttpResponseMessage _responseCreate = await _httpClient.SendAsync(_requestCreate);

            _responseCreate.EnsureSuccessStatusCode();
            #endregion

            #region Arrange

            string _resultCreate = await _responseCreate.Content.ReadAsStringAsync();

            JObject _clienteObject = JObject.Parse(_resultCreate);

            HttpRequestMessage _requestDeleteById = new HttpRequestMessage(HttpMethod.Delete, $"/api/v1/clientes/{_clienteObject["id"]}");

            #endregion

            // Act
            HttpResponseMessage _responseGetById = await _httpClient.SendAsync(_requestDeleteById);

            // Assert
            _responseGetById.EnsureSuccessStatusCode();

            Assert.Equal(HttpStatusCode.OK, _responseGetById.StatusCode);
        }
    }
}
