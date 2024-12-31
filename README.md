# **Cosmetics Shop Application**

## **Thành viên**

- **Hồ Nguyễn Ngọc Duy** - 18120340
- **Vũ Thành Công** - 21120419
- **Châu Ngọc Phát** - 22120260

## **Đánh giá chung công việc**

| Thành viên            | Số giờ làm | Đánh giá |
|-----------------------|------------|----------|
| **Hồ Nguyễn Ngọc Duy**| 2 giờ      | 8/10     |
| **Vũ Thành Công**      | 5 giờ      | 9/10     |
| **Châu Ngọc Phát**     | 8 giờ     | 10/10    |

## **Hướng dẫn sử dụng**
### Tạo database bằng migration.
#### Container: cosmetic_sql
#### Host: localhost,1433
#### Database name: COSMETIC_SHOP

### Video demo: [Click here](https://youtu.be/L31Z-UMT6LQ)

### Vào ứng dụng

1. **Đăng ký tài khoản**:
   - Trước tiên, người dùng cần tạo tài khoản bằng cách cung cấp thông tin cơ bản (Tên, Email, Mật khẩu) để đăng ký.

2. **Đăng nhập vào ứng dụng**:
   - Sau khi đăng ký, người dùng có thể đăng nhập vào ứng dụng với thông tin tài khoản đã đăng ký. Hệ thống sẽ phân quyền cho người dùng (User hoặc Admin).
   
   **Tài khoản dành cho Admin**:
    - Username: admin
    - Password: 123

# **Chức Năng Ứng Dụng**

## MILESTONE 1

### 0. Dữ liệu
- Sử dụng **MockDao**.

### **1. Đăng Nhập**
- **Đăng nhập**: 
  - Phân quyền người dùng (Admin và User).
  - Hỗ trợ ghi nhớ đăng nhập cho người dùng đã đăng nhập (tích chọn `Remember me`).

### **2. Trang Dashboard**
- **Danh sách sản phẩm**:
  - Sản phẩm mới nhất.
  - Sản phẩm bán chạy nhất.

### **3. Trang Tìm Kiếm Sản Phẩm**
  - Tìm kiếm sản phẩm theo từ khóa (Search).
  - Lọc sản phẩm (Filter):
    - Lọc theo khoảng giá (min - max).
    - Lọc theo thương hiệu sản phẩm.
### **4. Trang chi tiết sản phẩm**
  - Giao diện chi tiết sản phẩm sau khi ấn vào mua hàng.
### **5. Trang giỏ hàng**
- Giao diện trang giỏ hàng.
### **6. Trang Admin**
- Giao diện trang quản lý.

## MILESTONE 2
### 0. Dữ liệu
- Sử dụng **Microsoft SQL Server**, môi trường chạy trên **Docker**.
- Sử dụng **Knexjs** xây dựng Database, Migration.
- Kết nối bằng **Entity Framework Core** thông qua lớp **SqlDao**.

### 1. Đăng nhập và Đăng ký
- **Đăng ký**:
   - Tạo tài khoản mới (Tài khoản User)
- Báo lỗi nếu kết nối đến Server xảy ra lỗi khi đăng nhập và đăng ký.

### 2. Nâng cấp Thumbnail sản phẩm
- Hiển thị thêm:
   - Đánh giá sản phẩm.
   - Số lượng hàng đã bán.
   - Số lượng hàng tồn kho.
   - Hiệu ứng khi Hover vào sản phẩm.
   - Có hình ảnh của sản phẩm được tải từ Database.

### 2. Trang Dashboard
- Thêm mục sản phẩm mua gần đây (cá nhân hóa theo người dùng).

### 3. Trang tìm kiếm sản phẩm
- Thêm gợi ý tìm kiếm khi nhập từ khóa.
- Thêm tùy chọn lọc theo loại sản phẩm (Category).
- Thêm tùy chọn sắp xếp sản phẩm theo:
   - Mới nhất
   - Giá (thấp đến cao, cao đến thấp)
   - Tên sản phẩm (A-Z, Z-A)

### 4. Trang chi tiết sản phẩm
- Thêm khu vực hiển thị lượt đánh giá của sản phẩm, tùy chọn xem các lượt đánh giá theo số lượng sao

### 5. Trang giỏ hàng
- Chức năng tính toán tổng giá tiền với các sản phẩm đã chọn dựa theo:
   - Giá tiền x Số lượng
   - Giá tiền sau khi số lượng thay đổi
   - Giá tiền sau khi áp dụng voucher

### 6. Trang thanh toán
- Giao diện trang thanh toán
- Chức năng tính toán tổng giá tiền phải trả:
   - Giá tiền x Số lượng
   - Giá tiền sau khi áp dụng voucher

### 7. Trang tài khoản
- Tính toán thông tin mua hàng:
   - Tổng số tiền đã mua
   - Tổng số sản phẩm đã mua
   - Tổng số đơn hàng đã tạo
- Thông tin cá nhân (Tên, SDT, Email, Địa chỉ)
- Chức năng thay đổi thông tin cá nhân như:
    - Avatar
    - Tên, Số điện thoại, Email, Địa chỉ.
- Đổi mật khẩu.
- Đăng xuất.

### 8. Trang Admin
- Quản lý toàn bộ tài khoản (thêm, xóa, sửa).
- Quản lý toàn bộ sản phẩm (thêm, sửa).

### 9. Unit Test
- Kiểm thử chức năng đăng nhập, đăng ký.
- Kiểm thử chức năng tìm kiếm sản phẩm.
- Kiểm thử chức năng thay đổi mật khẩu.
- Kiểm thử chức năng thêm, xóa tài khoản.

## **Phân Công Công Việc**

| Thành viên            | Công việc chính                                                                                                     |
|-----------------------|---------------------------------------------------------------------------------------------------------------------|
| **Châu Ngọc Phát**     | - Xây dựng cửa sổ và trang: Đăng nhập, Đăng ký, Dashboard, Tìm kiếm sản phẩm, Tài khoản, và Trang Admin.  <br>- Xây dựng lớp **Dao** (MockDao, SqlDao).  <br>- Triển khai: **MVVM Pattern**, **Dependency Injection**, **Event Aggregator**, và **Entity Framework Core**, xử lý logic bất đồng bộ bằng **Async/Await**, tối ưu với **SemaphoreSlim**.  <br>- Cài đặt **Unit Test**.|
| **Vũ Thành Công**      | - Xây dựng và hoàn thiện các trang: Giỏ hàng, Chi tiết sản phẩm, Thanh toán.  <br>- Tích hợp các tính năng như: Tăng số lượng sản phẩm trong giỏ hàng, tính tổng tiền theo số lượng và voucher giảm giá. <br>- Xây dựng lớp **Dao** (MockDao, SqlDao). <br>- Áp dụng các triển khai ở trên, xử lý logic bất đồng bộ bằng **Async/Await**.|
| **Hồ Nguyễn Ngọc Duy** | - Phát triển và cải thiện giao diện Trang Admin, Trang tài khoản.|

## **Công Nghệ và Kỹ Thuật Đã Sử Dụng**
- **Dependency Injection**: Quản lý và tối ưu hóa việc sử dụng các dịch vụ.
- **MVVM Pattern**: Xây dựng ứng dụng theo mô hình MVVM.
- **Event Aggregator Pattern**: Giao tiếp giữa các ViewModel, View.
- **Bất đồng bộ Async, Await**: Xử lý các tác vụ đồng thời.
- **SemaphoreSlim**: Đồng bộ hóa việc tìm kiếm sản phẩm.
- **Entity Framework Core**: Sử dụng để tương tác với Database.
- **Unit Test**: Kiểm thử cho chức năng.
- **Doxygen**: Tạo tài liệu cho mã nguồn.

## **NuGet Packages**
- `CommunityToolkit.Mvvm` (8.3.2)
- `Microsoft.EntityFrameworkCore.Design` (9.0.0)
- `Microsoft.EntityFrameworkCore.SqlServer` (9.0.0)
- `Microsoft.EntityFrameworkCore.Tools` (9.0.0)
- `Microsoft.Extensions.DependencyInjection` (9.0.0)
