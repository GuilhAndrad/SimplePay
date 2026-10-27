# Build
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["src/back/SimplePay.API/SimplePay.API.csproj", "src/back/SimplePay.API/"]
COPY ["src/back/SimplePay.Application/SimplePay.Application.csproj", "src/back/SimplePay.Application/"]
COPY ["src/back/SimplePay.Domain/SimplePay.Domain.csproj", "src/back/SimplePay.Domain/"]
COPY ["src/back/SimplePay.Infrastructure/SimplePay.Infrastructure.csproj", "src/back/SimplePay.Infrastructure/"]
COPY ["src/shared/SimplePay.Communication/SimplePay.Communication.csproj", "src/shared/SimplePay.Communication/"]
COPY ["src/shared/SimplePay.Exception/SimplePay.Exception.csproj", "src/shared/SimplePay.Exception/"]

RUN dotnet restore "src/back/SimplePay.API/SimplePay.API.csproj"

COPY . .

WORKDIR /src/src/back/SimplePay.API
RUN dotnet publish -c Release -o /app/publish --no-restore

# Runtime
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "SimplePay.API.dll"]