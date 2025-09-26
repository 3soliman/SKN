FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["SKN.API/SKN.API.csproj", "SKN.API/"]
COPY ["SKN.Core/SKN.Core.csproj", "SKN.Core/"]
COPY ["SKN.Infrastructure/SKN.Infrastructure.csproj", "SKN.Infrastructure/"]
RUN dotnet restore "SKN.API/SKN.API.csproj"
COPY . .
WORKDIR "/src/SKN.API"
RUN dotnet build "SKN.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SKN.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SKN.API.dll"]