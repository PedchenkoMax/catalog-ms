FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

COPY ["Catalog.API/Catalog.API.csproj", "Catalog.API/"]
COPY ["Catalog.Domain/Catalog.Domain.csproj", "Catalog.Domain/"]
COPY ["Catalog.Infrastructure/Catalog.Infrastructure.csproj", "Catalog.Infrastructure/"]

RUN dotnet restore "./Catalog.Domain/"
RUN dotnet restore "./Catalog.Infrastructure/"
RUN dotnet restore "./Catalog.API/"

COPY . .

WORKDIR "/src/Catalog.API"
RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Catalog.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app

COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Catalog.API.dll"]