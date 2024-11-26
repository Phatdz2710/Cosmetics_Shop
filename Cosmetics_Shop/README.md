# Cosmetic Shop Application

## Thành viên

- **22120260** - Châu Ngọc Phát
- **18120340** - Hồ Nguyễn Ngọc Duy
- **21120419** - Vũ Thành Công

## Đánh giá công việc
- **Châu Ngọc Phát**:
  - Số giờ làm: 11 giờ
  - Đánh giá: 10/10
- **Hồ Nguyễn Ngọc Duy**:
  - Số giờ làm: 2 giờ
  - Đánh giá: 8/10
- **Vũ Thành Công**:
  - Số giờ làm: 5 giờ
  - Đánh giá: 9/10

## CÁCH ĐĂNG NHẬP VÀO ỨNG DỤNG
- Tạo tài khoản trước tiên sau đó đăng nhập bằng thông tin vừa đăng ký.

## Chức năng đã hoàn thành
- **Đăng nhập**: 
  - Đăng nhập và ghi nhớ đăng nhập. Phân quyền người dùng (Admin và User).

- **Đăng ký**: 
  - Tạo tài khoản người dùng.

- **Trang Dashboard**:
  - Danh sách sản phẩm mới nhất.
  - Danh sách sản phẩm bán chạy nhất.
  - Danh sách sản phẩm mua gần nhất.

- **Trang tìm kiếm sản phẩm**:
  - Tìm kiếm sản phẩm theo từ khóa (Search).
  - Tổng số sản phẩm tìm kiếm được.
  - Lọc sản phẩm (Filter):
    - Lọc theo khoảng giá (min - max).
    - Lọc theo thương hiệu sản phẩm.
    - Lọc theo loại sản phẩm.
    - Sắp xếp sản phẩm.
      + Mới nhất
      + Giá tăng/giảm dần
      + Sắp xếp theo tên sản phẩm.
  	- Danh sách thương hiệu và loại sản phẩm hiển thị đúng với các sản phẩm hiện có.

- **Gợi ý khi tìm kiếm**
  - Gợi ý sản phẩm dựa trên Keyword được nhập.

- **Trang thông tin chi tiết sản phẩm**: 
  - ***Các chức năng hiển thị cơ bản***
    - Hiển thị được thông tin chi tiết cơ bản của sản phẩm theo ID
    - Các chức năng tăng số lượng sản phẩm, xem chi phí vận chuyển hoàn thành
  - ***Khu vực đánh giá***
    - Hiển thị được các đánh giá theo ID sản phẩm với số sao tương ứng
    - Lọc các đánh giá theo số sao, sau đó có button Alls để xem lại toàn bộ đánh giá
  - ***Các chức năng chưa làm***
    - Thêm vào giỏ hàng (Đã thực hiện các hàm, chờ insert vô Sql)
    - Chức năng mua ngay (chuyển qua trang thanh toán): do chưa có trang thanh toán
  - ***Các chức năng sẽ update sau***
    - Khu vực đánh giá: Hiển thị thêm số lượng đánh giá tại button Alls, số lượng đánh giá ứng với mỗi số lượng sao.

- **Trang giỏ hàng**:
  - ***Các chức năng***
    - Hiển thị được các sản phẩm có trong giỏ hàng
    - Các chức năng tăng số lượng thì tiền tương ứng tăng theo hoàn thành
    - Tổng số tiền thanh toán được tính và thay đổi theo:
      - Các sản phẩm nào được chọn (có nút chọn tất cả sản phẩm)
      - Áp dụng voucher
  - ***Các chức năng chưa làm***
    - Xóa sản phẩm khỏi Cart
    - Chức năng mua ngay (chuyển qua trang thanh toán): do chưa có trang thanh toán.

- **Trang dành cho Admin**:
  - Giao diện trang quản lý.


## Dữ liệu
- Sử dụng Database với **SqlDao**.

## Công nghệ và kỹ thuật đã sử dụng
- **Dependency Injection**: Inject các thuộc tính cần thiết (Singleton, ...).
- **MVVM Pattern**: Xây dựng ứng dụng theo mô hình MVVM.
- **Event Aggregator Pattern**: Trao đổi thông tin giữa các thành phần mà không cần biết đến nhau.
- **Bất đồng bộ Async, Await**: Áp dụng cho các tính năng đăng nhập và tìm kiếm.
- **Unit Test**: Kiểm thử cho chức năng đăng nhập và tìm kiếm.

## NuGet Packages 
- `CommunityToolkit.Mvvm` (8.3.2)
- `Microsoft.EntityFrameworkCore.Design` (9.0.0)
- `Microsoft.EntityFrameworkCore.SqlServer` (9.0.0)
- `Microsoft.EntityFrameworkCore.Tools` (9.0.0)
- `Microsoft.Extensions.DependencyInjection` (9.0.0)