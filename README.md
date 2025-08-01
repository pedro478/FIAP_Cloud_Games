Esta é uma API RESTful desenvolvida em C# 8 utilizando o .NET 8, seguindo os princípios do Domain-Driven Design (DDD).
A arquitetura implementa separação clara entre Domínio, Aplicação, Infraestrutura, além de aplicar boas práticas como Injeção de Dependência, Repository Pattern, e Service.
Além de utilizar xUnit para testes.

As tecnologias utilizadas foram:
.NET 8
C# 8
Entity Framework Core
ASP.NET Core Web API
Injeção de Dependência (DI)
Swagger / OpenAPI
Banco de dados SQLITE
xUnit 

Para rodar o projeto:
Tenha o .NET 8 SDK instalado e Visual Studio 2022 / VS Code ou IDE
Clone o repositorio 
Configure a string de conexão no arquivo appsettings.json 
Aplique as migrações 
Rode a aplciação e acessa a API na página Swagger

Boas práticas aplicadas
Segregação clara de responsabilidades
Uso de interfaces para abstração e injeção de dependências
DTOs para entrada e saída, evitando exposição do modelo de domínio
Tratamento de erros padronizado e logging 
