# Sử dụng image .NET SDK để build ứng dụng
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Sao chép csproj và restore dependencies
COPY *.csproj ./
RUN dotnet restore

# Sao chép tất cả các file còn lại và build ứng dụng
COPY . ./
RUN dotnet publish -c Release -o out

# Sử dụng image .NET runtime để chạy ứng dụng
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out ./

# Chạy ứng dụng
ENTRYPOINT ["dotnet", "Capstone.Api.dll"]
