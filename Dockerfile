# Use the SDK image for building the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy the project files and restore dependencies
COPY ./Se_Capstone_Backend/*.sln ./
COPY ./Se_Capstone_Backend/Capstone.Api/*.csproj ./Capstone.Api/
RUN dotnet restore

# Copy the rest of the files and build the app
COPY ./Se_Capstone_Backend/Capstone.Api/. ./Capstone.Api/
WORKDIR /app/Capstone.Api
RUN dotnet publish -c Release -o out

# Use the runtime image for running the app
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/Capstone.Api/out ./
ENTRYPOINT ["dotnet", "Capstone.Api.dll"]
