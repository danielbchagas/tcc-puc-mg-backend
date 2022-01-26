#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Services/Identidade/ECommerce.Identidade.Api/ECommerce.Identidade.Api.csproj", "Services/Identidade/ECommerce.Identidade.Api/"]
RUN dotnet restore "Services/Identidade/ECommerce.Identidade.Api/ECommerce.Identidade.Api.csproj"
COPY . .
WORKDIR "/src/Services/Identidade/ECommerce.Identidade.Api"
RUN dotnet build "ECommerce.Identidade.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ECommerce.Identidade.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ECommerce.Identidade.Api.dll"]