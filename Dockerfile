# Stage 1: Build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.sln .
COPY Capstone.Api/*.csproj ./Capstone.Api/
RUN dotnet restore

# Copy everything else and build
COPY . .
WORKDIR /app/Capstone.Api
RUN dotnet publish -c Release -o /app/out

# Stage 2: Create runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "Capstone.Api.dll"]
