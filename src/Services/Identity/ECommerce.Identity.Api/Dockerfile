#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Services/Identity/ECommerce.Identity.Api/ECommerce.Identity.Api.csproj", "Services/Identity/ECommerce.Identity.Api/"]
RUN dotnet restore "Services/Identity/ECommerce.Identity.Api/ECommerce.Identity.Api.csproj"
COPY . .
WORKDIR "/src/Services/Identity/ECommerce.Identity.Api"
RUN dotnet build "ECommerce.Identity.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ECommerce.Identity.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ECommerce.Identity.Api.dll"]