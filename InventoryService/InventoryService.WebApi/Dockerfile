FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 5100

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["InventoryService/InventoryService.WebApi/InventoryService.WebApi.csproj", "InventoryService.WebApi/"]

RUN dotnet restore "./InventoryService.WebApi/InventoryService.WebApi.csproj"
COPY . .
WORKDIR "/src/InventoryService/InventoryService.WebApi"
RUN dotnet build "./InventoryService.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./InventoryService.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "InventoryService.WebApi.dll"]