

# Administração de clientes
Solução para gerenciamento de clientes.
Desenvolvido por [Gabriel Vicente]([https://www.linkedin.com/in/gvms23/](https://www.linkedin.com/in/gvms23/)).

# Sumário
- [Pastas da solução](#pastas-da-solução)
- [Utilização](#utilização)
- [Endpoints](#endpoints)
	- [Collection Postman](#collection-postman)
- [Testes](#testes)
- [Conceitos e Técnicas](#conceitos-e-técnicas)
- [Créditos](#créditos)

## Pastas da solução
* 00 - Test
* 01 - API
* 02 - Domain
* 03 - Infra
* Solution Items

## Utilização:
A soluão é orquestrada em 2 serviços no Docker, sendo um para a base de dados e outro para a API.

### Para rodar o projeto:
* No diretório raiz `cd "administracao-clientes"`
* Abra o powershell e execute: `docker-compose up --build`

Dois containers serão criados:
- **administracao.clientes.api**
- **administracao.clientes.sqlserver**

Acesse `http://localhost/docs` para visualizar os [endpoints](#endpoints) disponíveis.

Para visualizar os dados do SQL Server basta abrir o [SSMS]([https://docs.microsoft.com/pt-br/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver15](https://docs.microsoft.com/pt-br/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver15)) e inserir os dados de acesso:

* Server Name: localhost
* User: sa
* Password: !AsDqWe123@@
* Database Name: AdministracaoClientes


## Endpoints

Acesse `http://localhost/docs` para visualizar o Swagger com os endpoints e seus objetos esperados e retornados em cada request.

As ações dos endpoints variam de acordo aos verbos HTTP
Fonte utilizada: [https://docs.microsoft.com/pt-br/azure/architecture/best-practices/api-design#define-operations-in-terms-of-http-methods](https://docs.microsoft.com/pt-br/azure/architecture/best-practices/api-design#define-operations-in-terms-of-http-methods)

*TODO: Implementar método PUT.*

| Método | Descrição | Exemplo | Params
|------------ |--------|--------|--------  
| `GET` | Obter clientes (parâmetro opcional `id` pode ser enviado) | `/api/v1/clientes/{id}` | `{id: Guid}`
| `POST` | Criar um recurso do tipo cliente | `/api/v1/clientes` | `application/json` 
| `DELETE` | Excluir um recurso da entidade, enviando o `id` | `/api/v1/clientes/{id}` | `{id: Guid}`
     

Exemplo de Request Body para utilização pelo método POST: 
```javascript
    {
      "nome": "John Doe",
      "cpf": "421.144.560-05",
      "email": "john.doe@belo-dominio.com",
      "enderecos": [
        {
          "rua": "Rua Pascoal Pais",
          "numero": 1000,
          "bairro": "Vila Cordeiro",
          "cidade": "São Paulo",
          "estado": "SP",
          "pais": "Brasil",
          "cep": "04581-060"
        }
      ],
      "telefones": [
        12991919191
      ]
    }
```

Exemplo de Response Body após utilização pelo método POST do `json` acima: 
```javascript
    {
      "nome": "John Doe",
      "cpf": {
        "value": "42114456005",
        "formatado": "421.144.560-05",
        "semPontuacao": 42114456005,
        "length": 11,
        "valid": true,
        "invalid": false,
        "empty": false
      },
      "email": {
        "value": "john.doe@belo-dominio.com",
        "length": 25,
        "valid": true,
        "invalid": false,
        "empty": false
      },
      "enderecos": [
        {
          "rua": "Rua Pascoal Pais",
          "numero": 1000,
          "bairro": "Vila Cordeiro",
          "cidade": "São Paulo",
          "estado": "SP",
          "pais": "Brasil",
          "cep": {
            "value": "04581060",
            "formatado": "04581-060",
            "length": 8,
            "valid": true,
            "invalid": false,
            "empty": false
          },
          "empty": false,
          "valid": true,
          "invalid": false
        }
      ],
      "telefones": [
        {
          "value": 12991919191,
          "length": 11,
          "valid": true,
          "invalid": false,
          "empty": false,
          "isMobile": true,
          "ddd": 12,
          "formatado": "(12) 99191-9191"
        }
      ],
      "id": "99a72bf2-02ca-4671-a74e-4a3908c1cf77",
      "isDeleted": false,
      "createdDate": "2020-06-08T05:15:25.8955216+00:00"
    }
```

## Collection Postman
O arquivo para testes pode ser baixado [nesse link](https://github.com/gvms23/administracao-clientes/blob/master/Zup%20Administra%C3%A7%C3%A3o%20Clientes.postman_collection.json), para ser importado ao [Postman](https://www.postman.com/downloads/).

![Collection Postman](https://github.com/gvms23/administracao-clientes/blob/master/assets/collection_postman.png)

## Testes

Testes de unidade e de integração, utilizando a técnica TDD para testar o domínio em conjunto com as regras.
![Painel de testes unitários e de integração com xUnit](https://github.com/gvms23/administracao-clientes/blob/master/assets/testes_evidencia.png)

## Conceitos e Técnicas:
* ASP .NET Core 3.1;
* Docker;
* Docker Compose;
* GIT (atomic commits);
* Abordagem DDD (Design Domain Driven);
* Técnica TDD;
* Unit of Work Pattern;
* FluentValidation Library;
* Clean Architecture ~~(Valeu Uncle Bob!)~~;
* Unit Tests com xUnit;
* Integration Tests com xUnit;
* IoC (camada isolada);
* Repository Pattern;
* Princípios SOLID;
* Value Objects;
* Rich Domain Concepts;
* Domain Services;
* Custom Exception Handler Middleware;
* Swagger Docs;

## Créditos
Desenvolvido com dedicação por Gabriel Vicente.
* [Linkedin](https://linkedin.com/in/gvms23)