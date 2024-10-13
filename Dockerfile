# Sử dụng hình ảnh nền .NET SDK để xây dựng ứng dụng
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /app

# Sao chép tệp csproj và phục hồi các phụ thuộc
COPY Se_Capstone_Backend/Capstone.Api/*.csproj Se_Capstone_Backend/Capstone.Api/
RUN dotnet restore Se_Capstone_Backend/Capstone.Api/Capstone.Api.csproj

# Sao chép tất cả tệp còn lại và xây dựng ứng dụng
COPY Se_Capstone_Backend/Capstone.Api/ Se_Capstone_Backend/Capstone.Api/
RUN dotnet publish Se_Capstone_Backend/Capstone.Api/Capstone.Api.csproj -c Release -o out

# Tạo hình ảnh chạy với .NET ASP.NET
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out ./
ENTRYPOINT ["dotnet", "Capstone.Api.dll"]
