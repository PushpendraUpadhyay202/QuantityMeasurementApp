# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY . ./

# Restore using API project directly
RUN dotnet restore QuantityMeasurementAPI/QuantityMeasurementAPI.csproj

# Publish API project
RUN dotnet publish QuantityMeasurementAPI/QuantityMeasurementAPI.csproj -c Release -o /app/out

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build /app/out ./

ENV ASPNETCORE_URLS=http://+:${PORT:-8080}

EXPOSE 8080

CMD ["dotnet", "QuantityMeasurementAPI.dll"]
