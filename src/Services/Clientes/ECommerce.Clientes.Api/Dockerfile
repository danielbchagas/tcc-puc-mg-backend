#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Service/ECommerce.Clientes.Api/ECommerce.Clientes.Api.csproj", "Service/ECommerce.Clientes.Api/"]
RUN dotnet restore "Service/ECommerce.Clientes.Api/ECommerce.Clientes.Api.csproj"
COPY . .
WORKDIR "/src/Service/ECommerce.Clientes.Api"
RUN dotnet build "ECommerce.Clientes.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ECommerce.Clientes.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ECommerce.Clientes.Api.dll"]