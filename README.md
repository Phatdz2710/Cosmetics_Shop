# **Cosmetics Shop Application**

## **Thành viên**

- **Châu Ngọc Phát** - 22120260
- **Hồ Nguyễn Ngọc Duy** - 18120340
- **Vũ Thành Công** - 21120419

## **Đánh giá công việc**

| Thành viên            | Số giờ làm | Đánh giá |
|-----------------------|------------|----------|
| **Châu Ngọc Phát**     | 13 giờ     | 10/10    |
| **Hồ Nguyễn Ngọc Duy**| 2 giờ      | 8/10     |
| **Vũ Thành Công**      | 5 giờ      | 9/10     |

## **Hướng dẫn Đăng Nhập và Đăng Ký**

1. **Đăng ký tài khoản**:
   - Trước tiên, người dùng cần tạo tài khoản bằng cách cung cấp thông tin cơ bản (Tên, Email, Mật khẩu) để đăng ký.

2. **Đăng nhập vào ứng dụng**:
   - Sau khi đăng ký, người dùng có thể đăng nhập vào ứng dụng với thông tin tài khoản đã đăng ký. Hệ thống sẽ phân quyền cho người dùng (User hoặc Admin).

## **Chức Năng Ứng Dụng**

### **1. Đăng Nhập và Đăng Ký**
- **Đăng nhập**: 
  - Phân quyền người dùng (Admin và User).
  - Hỗ trợ ghi nhớ đăng nhập cho người dùng đã đăng ký.

- **Đăng ký**: 
  - Tạo tài khoản người dùng mới.

### **2. Trang Dashboard**
- **Danh sách sản phẩm**:
  - Sản phẩm mới nhất.
  - Sản phẩm bán chạy nhất.
  - Sản phẩm mua gần nhất (cá nhân hóa theo người dùng).

### **3. Trang Tìm Kiếm Sản Phẩm**
- **Tìm kiếm sản phẩm**:
  - Cho phép người dùng tìm kiếm sản phẩm theo từ khóa.
  - Gợi ý từ khóa tìm kiếm.
  - Lọc sản phẩm theo:
    - Khoảng giá (min - max).
    - Thương hiệu sản phẩm.
    - Loại sản phẩm.
    - Sắp xếp sản phẩm: Mới nhất, Giá tăng/giảm dần, Sắp xếp theo tên.
  - Thông tin hiển thị trên thumbnail sản phẩm: giá, đánh giá, tên sản phẩm, tồn kho, số lượng đã bán.

### **4. Trang Thông Tin Chi Tiết Sản Phẩm**
- **Các chức năng hiển thị cơ bản**:
  - Hiển thị thông tin chi tiết sản phẩm theo ID.
  - Chức năng tăng số lượng sản phẩm và tính toán chi phí vận chuyển.
- **Khu vực đánh giá**:
  - Hiển thị đánh giá sản phẩm theo ID với số sao tương ứng.
  - Lọc đánh giá theo số sao và có nút **Xem tất cả** để xem toàn bộ đánh giá.
- **Các chức năng chưa hoàn thiện**:
  - Thêm vào giỏ hàng (đã thực hiện hàm, chờ insert vào SQL).
  - Mua ngay (chuyển qua trang thanh toán - chưa có trang thanh toán).
- **Các chức năng sẽ cập nhật sau**:
  - Hiển thị số lượng đánh giá tại nút **Xem tất cả**.
  - Cập nhật số lượng đánh giá ứng với mỗi số sao.

### **5. Trang Giỏ Hàng**
- **Các chức năng**:
  - Hiển thị sản phẩm trong giỏ hàng.
  - Tăng số lượng sản phẩm và tự động tính tiền tương ứng.
  - Tính toán tổng số tiền thanh toán, thay đổi theo:
    - Các sản phẩm được chọn (có nút chọn tất cả).
    - Áp dụng voucher giảm giá.
- **Các chức năng chưa hoàn thiện**:
  - Xóa sản phẩm khỏi giỏ hàng.
  - Mua ngay (chuyển qua trang thanh toán - chưa có trang thanh toán).

### **6. Trang Tài Khoản**
- **Các chức năng**:
  - Thay đổi thông tin cá nhân, avatar.
  - Thay đổi mật khẩu.
  - Thống kê mua hàng và trạng thái tài khoản.
  - Đăng xuất.

### **7. Trang Dành Cho Admin**
- **Quản lý tài khoản**:
  - Thêm, xóa, sửa tài khoản người dùng.
- **Quản lý sản phẩm**:
  - Thêm, xóa, sửa sản phẩm.

## **Dữ Liệu**
- **Sử dụng Database**: Sử dụng **SqlDao** với **EntityFrameworkCore**.

## **Công Nghệ và Kỹ Thuật Đã Sử Dụng**
- **Dependency Injection**: Quản lý và tối ưu hóa việc sử dụng các dịch vụ.
- **MVVM Pattern**: Xây dựng ứng dụng theo mô hình MVVM.
- **Event Aggregator Pattern**: Giao tiếp giữa các ViewModel, View.
- **Bất đồng bộ Async, Await**: Xử lý các tác vụ đồng thời.
- **Entity Framework Core**: Sử dụng để tương tác với Database.
- **SemaphoreSlim**: Đồng bộ hóa các tác vụ đồng thời.
- **Unit Test**: Kiểm thử cho chức năng.

## **NuGet Packages**
- `CommunityToolkit.Mvvm` (8.3.2)
- `Microsoft.EntityFrameworkCore.Design` (9.0.0)
- `Microsoft.EntityFrameworkCore.SqlServer` (9.0.0)
- `Microsoft.EntityFrameworkCore.Tools` (9.0.0)
- `Microsoft.Extensions.DependencyInjection` (9.0.0)

