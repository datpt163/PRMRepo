# Sử dụng hình ảnh nền .NET SDK để xây dựng ứng dụng
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Đặt thư mục làm việc
WORKDIR /app

# Sao chép tất cả tệp .csproj và khôi phục các phụ thuộc
COPY ./Capstone.Api/*.csproj ./Capstone.Api/
COPY ./Capstone.Application/*.csproj ./Capstone.Application/
COPY ./Capstone.Domain/*.csproj ./Capstone.Domain/
RUN dotnet restore ./Capstone.Api/Capstone.Api.csproj

# Sao chép tất cả tệp còn lại
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
