FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ArcaneCompendium.API/ArcaneCompendium.API.csproj", "ArcaneCompendium.API/"]
RUN dotnet restore "ArcaneCompendium.API/ArcaneCompendium.API.csproj"
COPY . .
WORKDIR "/src/ArcaneCompendium.API"
RUN dotnet build "ArcaneCompendium.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "ArcaneCompendium.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ArcaneCompendium.API.dll"]
