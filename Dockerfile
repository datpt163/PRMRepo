version: '3.8'  # Có thể bỏ qua, nhưng nếu cần, hãy dùng phiên bản 3.8
services:
  api:
    build:
      context: ./Se_Capstone_Backend/Capstone.Api  # Đường dẫn đến thư mục chứa dự án API
      dockerfile: Dockerfile                    # Đường dẫn đến Dockerfile
    ports:
      - "80:80"                                      # Cổng mà dịch vụ sẽ sử dụng
