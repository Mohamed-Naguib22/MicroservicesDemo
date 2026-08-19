FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ApiGateway/ApiGateway.WebApi/ApiGateway.WebApi.csproj", "ApiGateway.WebApi/"]
RUN dotnet restore "./ApiGateway.WebApi/ApiGateway.WebApi.csproj"
COPY . .
WORKDIR "/src/ApiGateway/ApiGateway.WebApi"
RUN dotnet build "./ApiGateway.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ApiGateway.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ApiGateway.WebApi.dll"]
