# Sử dụng hình ảnh nền .NET SDK để xây dựng ứng dụng
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Đặt thư mục làm việc
WORKDIR /app

# Sao chép tất cả tệp .csproj và khôi phục các phụ thuộc
COPY ./Se_Capstone_Backend/Capstone.Api/Capstone.Api.csproj ./Se_Capstone_Backend/Capstone.Api/
COPY ./Se_Capstone_Backend/Capstone.Infrastructure/Capstone.Infrastructure.csproj ./Se_Capstone_Backend/Capstone.Infrastructure/
COPY ./Se_Capstone_Backend/Capstone.Application/Capstone.Application.csproj ./Se_Capstone_Backend/Capstone.Application/
COPY ./Se_Capstone_Backend/Capstone.Domain/Capstone.Domain.csproj ./Se_Capstone_Backend/Capstone.Domain/

# Khôi phục các phụ thuộc cho Capstone.Api
RUN dotnet restore ./Se_Capstone_Backend/Capstone.Api/Capstone.Api.csproj

# Sao chép tất cả mã nguồn vào thư mục làm việc
COPY ./Se_Capstone_Backend/. .

# Xây dựng ứng dụng
RUN dotnet publish ./Se_Capstone_Backend/Capstone.Api/Capstone.Api.csproj -c Release -o out

# Tạo hình ảnh chạy với .NET ASP.NET
FROM mcr.microsoft.com/dotnet/aspnet:8.0

# Đặt thư mục làm việc
WORKDIR /app

# Sao chép thư mục xuất bản từ hình ảnh xây dựng
COPY --from=build /app/out ./

# Chạy ứng dụng
ENTRYPOINT ["dotnet", "Capstone.Api.dll"]
