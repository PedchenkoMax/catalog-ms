FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

COPY ["Catalog.Domain/*.csproj", "./Catalog.Domain/"]
COPY ["Catalog.Infrastructure/*.csproj", "./Catalog.Infrastructure/"]
COPY ["Catalog.API/*.csproj", "./Catalog.API/"]

RUN dotnet restore "./Catalog.Domain/"
RUN dotnet restore "./Catalog.Infrastructure/"
RUN dotnet restore "./Catalog.API/"

WORKDIR "/src/Catalog.Domain"
RUN dotnet build -c Release -o /app
WORKDIR "/src/Catalog.Infrastructure"
RUN dotnet build -c Release -o /app
WORKDIR "/src/Catalog.API"
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Catalog.API.dll"]