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
using Xunit;
using Zup.AdministracaoClientes.API;
using Zup.AdministracaoClientes.API.ViewModels;

namespace Zup.AdministracaoClientes.Tests.IntegrationTests
{
    [Obsolete("[WIP] Work in progress - não utilizar por ora")]
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

        [Fact(DisplayName = "Cadastrar Cliente Async")]
        public async Task CadastrarClientesAsync_Retorna201Created()
        {
            // Arrange
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
                nome: "João",
                email: "123.456.555-87",
                cpf: "joao@gmail.com",
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


            string _responseContent = await _response.Content.ReadAsStringAsync();

            // Assert
            _response.EnsureSuccessStatusCode();

            Assert.Equal(HttpStatusCode.Created, _response.StatusCode);
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
            // Arrange
            HttpRequestMessage _request = new HttpRequestMessage(HttpMethod.Get, "/api/v1/clientes");

            // Act
            HttpResponseMessage _response = await _httpClient.SendAsync(_request);

            // Assert
            _response.EnsureSuccessStatusCode();

            Assert.Equal(HttpStatusCode.OK, _response.StatusCode);
        }
    }
}
