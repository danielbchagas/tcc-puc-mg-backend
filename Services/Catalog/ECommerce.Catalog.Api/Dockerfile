#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Services/Produtos/ECommerce.Produtos.Api/ECommerce.Produtos.Api.csproj", "Services/Produtos/ECommerce.Produtos.Api/"]
COPY ["Services/Produtos/ECommerce.Produtos.Infrastructure/ECommerce.Produtos.Infrastructure.csproj", "Services/Produtos/ECommerce.Produtos.Infrastructure/"]
COPY ["Services/Produtos/ECommerce.Produtos.Domain/ECommerce.Produtos.Domain.csproj", "Services/Produtos/ECommerce.Produtos.Domain/"]
RUN dotnet restore "Services/Produtos/ECommerce.Produtos.Api/ECommerce.Produtos.Api.csproj"
COPY . .
WORKDIR "/src/Services/Produtos/ECommerce.Produtos.Api"
RUN dotnet build "ECommerce.Produtos.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ECommerce.Produtos.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ECommerce.Produtos.Api.dll"]