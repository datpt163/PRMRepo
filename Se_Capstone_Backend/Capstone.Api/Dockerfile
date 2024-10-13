# Sử dụng hình ảnh nền .NET SDK để xây dựng ứng dụng
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /app

# Sao chép tệp csproj và phục hồi các phụ thuộc
COPY *.csproj ./
RUN dotnet restore

# Sao chép tất cả tệp còn lại và xây dựng ứng dụng
COPY . ./
RUN dotnet publish -c Release -o out

# Tạo hình ảnh chạy với .NET ASP.NET
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out ./
ENTRYPOINT ["dotnet", "Capstone.Api.dll"]
