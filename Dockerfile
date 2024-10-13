# Dockerfile bên ngoài (nếu bạn cần)

# Sử dụng hình ảnh nền .NET SDK để xây dựng tất cả các ứng dụng
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Đặt thư mục làm việc
WORKDIR /app

# Sao chép tất cả các dự án
COPY ./Capstone.Api/Capstone.Api.csproj ./Capstone.Api/
COPY ./Capstone.Application/Capstone.Application.csproj ./Capstone.Application/
COPY ./Capstone.Domain/Capstone.Domain.csproj ./Capstone.Domain/

# Khôi phục các phụ thuộc
RUN dotnet restore ./Capstone.Api/Capstone.Api.csproj

# Sao chép mã nguồn
COPY . .

# Xây dựng ứng dụng
RUN dotnet publish ./Capstone.Api/Capstone.Api.csproj -c Release -o out

# Tạo hình ảnh chạy với .NET ASP.NET
FROM mcr.microsoft.com/dotnet/aspnet:8.0

# Đặt thư mục làm việc
WORKDIR /app

# Sao chép thư mục xuất bản từ hình ảnh xây dựng
COPY --from=build /app/out ./

# Chạy ứng dụng
ENTRYPOINT ["dotnet", "Capstone.Api.dll"]
