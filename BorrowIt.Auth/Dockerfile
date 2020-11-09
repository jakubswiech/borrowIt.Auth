FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app

# Copy everything else and build
COPY . ./

# Copy csproj and restore as distinct layers
RUN dotnet restore --configfile ./nuget.config ./BorrowIt.Auth/BorrowIt.Auth.csproj
RUN dotnet restore --configfile ./nuget.config ./BorrowIt.Auth.Application/BorrowIt.Auth.Application.csproj
RUN dotnet restore --configfile ./nuget.config ./BorrowIt.Auth.Domain/BorrowIt.Auth.Domain.csproj
RUN dotnet restore --configfile ./nuget.config ./BorrowIt.Auth.Infrastructure/BorrowIt.Auth.Infrastructure.csproj
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "BorrowIt.Auth.dll"]