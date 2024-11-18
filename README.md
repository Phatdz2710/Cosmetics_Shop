# Cosmetic Shop Application

## Thành viên
- **22120260** - Châu Ngọc Phát
- **18120340** - Hồ Nguyễn Ngọc Duy
- **21120419** - Vũ Thành Công

## Đánh giá công việc
- **Châu Ngọc Phát**:
  - Số giờ làm: 7 giờ
  - Đánh giá: 10/10
- **Hồ Nguyễn Ngọc Duy**:
  - Số giờ làm: 2 giờ
  - Đánh giá: 8/10
- **Vũ Thành Công**:
  - Số giờ làm: 3 giờ
  - Đánh giá: 9/10

## CÁCH ĐĂNG NHẬP VÀO ỨNG DỤNG
- Tài khoản User: 
  - **Username**: `user`
  - **Password**: `user`


- Tài khoản Admin: 
  - **Username**: `admin`
  - **Password**: `admin`

## Chức năng đã hoàn thành
- **Đăng nhập**: 
  - Đăng nhập và ghi nhớ đăng nhập. Phân quyền người dùng (Admin và User).
- **Trang Dashboard**:
  - Danh sách sản phẩm mới nhất.
  - Danh sách sản phẩm bán chạy nhất.
- **Trang tìm kiếm sản phẩm**:
  - Tìm kiếm sản phẩm theo từ khóa (Search).
  - Tổng số sản phẩm tìm kiếm được.
  - Lọc sản phẩm (Filter):
    - Lọc theo khoảng giá (min - max).
    - Lọc theo thương hiệu sản phẩm.
  	- Danh sách thương hiệu hiển thị đúng với các sản phẩm hiện có.
- **Gợi ý khi tìm kiếm**
  - Gợi ý sản phẩm dựa trên Keyword được nhập.
- **Xem chi tiết sản phẩm**
  - Hiển thị thông tin chi tiết của sản phẩm sau khi nhấn vào mua.
- **Trang giỏ hàng**
  - Giao diện trang giỏ hàng.
- **Trang dành cho Admin**:
  - Giao diện trang quản lý.


## Dữ liệu
- Sử dụng **MockDAO** cho dữ liệu giả.

## Công nghệ và kỹ thuật đã sử dụng
- **Dependency Injection**: Inject các thuộc tính cần thiết (Singleton, ...).
- **MVVM Pattern**: Xây dựng ứng dụng theo mô hình MVVM.
- **Event Aggregator Pattern**: Trao đổi thông tin giữa các thành phần mà không cần biết đến nhau.
- **Bất đồng bộ Async, Await**: Áp dụng cho các tính năng đăng nhập và tìm kiếm.
- **Unit Test**: Kiểm thử cho chức năng đăng nhập và tìm kiếm.


