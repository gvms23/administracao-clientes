#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/Zup.AdministracaoClientes.API/Zup.AdministracaoClientes.API.csproj", "src/Zup.AdministracaoClientes.API/"]
COPY ["src/Zup.AdministracaoClientes.Domain/Zup.AdministracaoClientes.Domain.csproj", "src/Zup.AdministracaoClientes.Domain/"]
COPY ["src/Zup.AdministracaoClientes.Infra.CrossCutting.Helpers/Zup.AdministracaoClientes.Infra.CrossCutting.Helpers.csproj", "src/Zup.AdministracaoClientes.Infra.CrossCutting.Helpers/"]
COPY ["src/Zup.AdministracaoClientes.Infra.CrossCutting.IoC/Zup.AdministracaoClientes.Infra.CrossCutting.IoC.csproj", "src/Zup.AdministracaoClientes.Infra.CrossCutting.IoC/"]
COPY ["src/Zup.AdministracaoClientes.Infra.CrossCutting.Swagger/Zup.AdministracaoClientes.Infra.CrossCutting.Swagger.csproj", "src/Zup.AdministracaoClientes.Infra.CrossCutting.Swagger/"]
COPY ["src/Zup.AdministracaoClientes.Data/Zup.AdministracaoClientes.Data.csproj", "src/Zup.AdministracaoClientes.Data/"]
RUN dotnet restore "src/Zup.AdministracaoClientes.API/Zup.AdministracaoClientes.API.csproj"
COPY . .
WORKDIR "/src/src/Zup.AdministracaoClientes.API"
RUN dotnet build "Zup.AdministracaoClientes.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Zup.AdministracaoClientes.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Zup.AdministracaoClientes.API.dll"]