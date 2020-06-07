using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Zup.AdministracaoClientes.API.ViewModels;
using Zup.AdministracaoClientes.Domain.Entities;
using Zup.AdministracaoClientes.Domain.Interfaces.Repositories;
using Zup.AdministracaoClientes.Domain.Interfaces.Services;
using Zup.AdministracaoClientes.Domain.ValueObjects;

namespace Zup.AdministracaoClientes.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly ILogger<ClientesController> _logger;
        private readonly IClienteRepository _clienteRepository;
        private readonly IClienteService _clienteService;

        public ClientesController(
                ILogger<ClientesController> logger,
                IClienteRepository clienteRepository,
                IClienteService clienteService)
        {
            _logger = logger;
            _clienteRepository = clienteRepository;
            _clienteService = clienteService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(List<Cliente>), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateCliente([FromBody] CadastrarClienteViewModel cliente)
        {
            Cliente _cliente = new Cliente(cliente.Nome, cliente.CPF);

            foreach (var endereco in cliente.Enderecos)
                _cliente.AdicionarEnderecos(new Endereco(
                                                    endereco.Rua,
                                                    endereco.Numero,
                                                    endereco.Bairro,
                                                    endereco.Cidade,
                                                    endereco.Estado,
                                                    endereco.Pais,
                                                    endereco.CEP));

            foreach (var telefone in cliente.Telefones)
                _cliente.AdicionarTelefones(new Telefone(telefone));


            Cliente _result = await _clienteService.Cadastrar(_cliente);
            return Ok(_result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<Cliente>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetClientes()
        {
            List<Cliente> _result = await _clienteRepository.Get();

            if (!_result.Any())
                return NoContent();

            return Ok(_result);
        }
    }
}
