#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/runtime:3.1-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["tests/Zup.AdministracaoClientes.Tests/Zup.AdministracaoClientes.Tests.csproj", "tests/Zup.AdministracaoClientes.Tests/"]
COPY ["src/Zup.AdministracaoClientes.Domain/Zup.AdministracaoClientes.Domain.csproj", "src/Zup.AdministracaoClientes.Domain/"]
COPY ["src/Zup.AdministracaoClientes.Infra.CrossCutting.Helpers/Zup.AdministracaoClientes.Infra.CrossCutting.Helpers.csproj", "src/Zup.AdministracaoClientes.Infra.CrossCutting.Helpers/"]
COPY ["src/Zup.AdministracaoClientes.Infra.CrossCutting.ExceptionHandler/Zup.AdministracaoClientes.Infra.CrossCutting.ExceptionHandler.csproj", "src/Zup.AdministracaoClientes.Infra.CrossCutting.ExceptionHandler/"]
COPY ["src/Zup.AdministracaoClientes.API/Zup.AdministracaoClientes.API.csproj", "src/Zup.AdministracaoClientes.API/"]
COPY ["src/Zup.AdministracaoClientes.Infra.CrossCutting.Swagger/Zup.AdministracaoClientes.Infra.CrossCutting.Swagger.csproj", "src/Zup.AdministracaoClientes.Infra.CrossCutting.Swagger/"]
COPY ["src/Zup.AdministracaoClientes.Infra.CrossCutting.IoC/Zup.AdministracaoClientes.Infra.CrossCutting.IoC.csproj", "src/Zup.AdministracaoClientes.Infra.CrossCutting.IoC/"]
COPY ["src/Zup.AdministracaoClientes.Data/Zup.AdministracaoClientes.Data.csproj", "src/Zup.AdministracaoClientes.Data/"]
RUN dotnet restore "tests/Zup.AdministracaoClientes.Tests/Zup.AdministracaoClientes.Tests.csproj"
COPY . .
WORKDIR "/src/tests/Zup.AdministracaoClientes.Tests"
RUN dotnet build "Zup.AdministracaoClientes.Tests.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Zup.AdministracaoClientes.Tests.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Zup.AdministracaoClientes.Tests.dll"]