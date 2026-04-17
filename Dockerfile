# Base image
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 10000

# Build image
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore "WebApiLayer/WebApiLayer.csproj"
RUN dotnet build "WebApiLayer/WebApiLayer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebApiLayer/WebApiLayer.csproj" -c Release -o /app/publish

# Final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebApiLayer.dll"]