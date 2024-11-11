# Use the SDK image for building the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy the solution and project files
COPY ./Se_Capstone_Backend/*.sln ./
COPY ./Se_Capstone_Backend/Capstone.Api/*.csproj ./Capstone.Api/
COPY ./Se_Capstone_Backend/Capstone.Infrastructure/*.csproj ./Capstone.Infrastructure/
COPY ./Se_Capstone_Backend/Capstone.Application/*.csproj ./Capstone.Application/
COPY ./Se_Capstone_Backend/Capstone.Domain/*.csproj ./Capstone.Domain/
COPY ./Se_Capstone_Backend/Capstone.Test/*.csproj ./Capstone.Test/
# Restore dependencies
RUN dotnet restore

# Copy the entire project
COPY ./Se_Capstone_Backend/. .

# Build the application
RUN dotnet publish ./Capstone.Api/Capstone.Api.csproj -c Release -o out

# Create the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "Capstone.Api.dll"]
