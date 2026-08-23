FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 7174

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["InventoryManagementSystemAPI/InventoryManagementSystemAPI.csproj", "InventoryManagementSystemAPI/"]
COPY ["Application/Application.csproj", "Application/"]
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]
COPY ["Contracts/Contracts.csproj", "Contracts/"]

RUN dotnet restore "InventoryManagementSystemAPI/InventoryManagementSystemAPI.csproj"

COPY . .

WORKDIR "/src/InventoryManagementSystemAPI"
RUN dotnet build "InventoryManagementSystemAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "InventoryManagementSystemAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "InventoryManagementSystemAPI.dll"]