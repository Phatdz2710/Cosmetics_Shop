# **Cosmetics Shop Application**
Ứng dụng mua mỹ phẩm với các chức năng cơ bản của một ứng dụng mua sắm trực tuyến.

## 👥 **Thành viên**

| Tên            | MSSV | 
|-----------------------|------------|
| **Hồ Nguyễn Ngọc Duy**| 18120340     | 
| **Vũ Thành Công**      | 21120419     | 
| **Châu Ngọc Phát**     | 22120260 |
## 🌟 **Đánh giá chung công việc**

### 🕒 Giờ làm và tự đánh giá (Milestone 3)
| Thành viên            | Số giờ làm | Đánh giá |
|-----------------------|------------|----------|
| **Hồ Nguyễn Ngọc Duy**| 3.5 giờ      | 10/10     |
| **Vũ Thành Công**      | 3.5 giờ      | 10/10     |
| **Châu Ngọc Phát**     | 3.5 giờ     | 10/10    |


### 📝 **Bảng công việc**
| Thành viên            | Công việc                                                                                                     |
|-----------------------|---------------------------------------------------------------------------------------------------------------------|
| **Châu Ngọc Phát**     | <b>Hoàn thiện các trang:</b> <br> + Trang đăng ký, đăng nhập <br> + Trang dashboard <br> + Trang tìm kiếm <br> + Trang đơn hàng <br> + Trang tài khoản cá nhân <br> + Trang quản lý tài khoản (Admin) <br> + Trang quản lý sản phẩm (Admin) <br> + Chức năng đổi Light/Dark mode.|
| **Vũ Thành Công**      | <b>Hoàn thiện các trang:</b> <br> + Trang chi tiết sản phẩm <br> + Trang thanh toán <br> + Trang giỏ hàng <br> + Trang đánh giá sản phẩm|
| **Hồ Nguyễn Ngọc Duy** | <b>Hoàn thiện các trang:</b> <br> + Trang quản lý đơn hàng (Admin) <br> <b>Hỗ trợ các trang:</b> <br> + Trang quản lý tài khoản (Admin) <br> + Trang quản lý sản phẩm (Admin)|

### **Minh chứng Teamwork:** [Click here](https://docs.google.com/spreadsheets/d/17md4-uG1S-eth66OMy1M5-doLW-ViATCUC3QJ-IMxs0/edit?gid=0#gid=0)
### **Link Github:** 
- Source code: [Click here](https://github.com/Phatdz2710/Cosmetics_Shop)
- Migration: [Click here](https://github.com/Phatdz2710/Cosmetics_Shop)
<br> <br>

## **Hướng dẫn sử dụng**
### 🌟 Tạo database bằng migration.

#### 🛠️ Xây dựng container `cosmetic_sql` từ image `mssql/server`:

```bash
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=SqlServer@123" -p 1433:1433 --name cosmetic_sql --hostname cosmetic_sql -d mcr.microsoft.com/mssql/server
```

💡 **Chú ý**: 
- Tên container: `cosmetic_sql`
- Mật khẩu đăng nhập: `SqlServer@123`
- Port: `1433`
- Tên image: `mcr.microsoft.com/mssql/server`

#### 🛠️  Cài đặt file `.env`:

```bash
NODE_ENV=development
SQLSERVER_HOST=localhost
SQLSERVER_PORT=1433
SQLSERVER_USERNAME=sa
SQLSERVER_PASSWORD=SqlServer@123
SQLSERVER_DATABASE=COSMETIC_SHOP
```

💡 **Chú ý**: 
- Server host: `localhost`
- Mật khẩu đăng nhập: `SqlServer@123`
- Port: `1433`
- Tên Database: `COSMETIC_SHOP`

#### 🛠️ Chạy migration:

Tạo database:

Tạo 1 database có tên `COSMETIC_SHOP` trên SQL Server.

Lệnh tạo tất cả bảng cho Database:
```bash
knex migrate:latest
```
Tạo dữ liệu mẫu:
```bash
knex seed:run
```
Rollback migration:
```bash
knex migrate:rollback
```

#### Cài đặt file `appsettings.json` trong Source:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost,1433;Database=COSMETIC_SHOP;User Id=sa;Password=SqlServer@123;TrustServerCertificate=True;"
  }
}
```
💡 **Chú ý**: 
- Server host: `localhost`
- Tên Database: `COSMETIC_SHOP`
- Id: `sa`
- Mật khẩu: `SqlServer@123`
- Port: `1433`
- TrustServerCertificate: `True`
<br> <br>
### 🔑 Vào ứng dụng

1. **Đăng ký tài khoản**:
   - Tạo tài khoản mới để sử dụng ứng dụng.

2. **Đăng nhập vào ứng dụng**:
   - Đăng nhập vào tài khoản mới tạo hoặc tài khoản đã được tạo sẵn dưới đây:
   
   **Tài khoản dành cho Admin**:
    - Username: `admin`
    - Password: `123`

   **Tài khoản dành cho User**:
    - Username: `ngocphat`
    - Password: `123`
<br> <br>
# ⚙️ **Chức Năng Ứng Dụng**
1. **🔑 Trang đăng ký, đăng nhập**: <br>
    - Đăng ký tài khoản mới.
    - Đăng nhập.
    - Hiển thị thông báo lỗi và thông báo thành công.

2. **📊 Trang Dashboard**
    - Hiển thị danh sách sản phẩm mới nhất.
    - Hiển thị danh sách sản phẩm bán chạy nhất.
    - Hiển thị danh sách sản phẩm người dùng mua gần đây.

3. **🛠️ Thanh công cụ**
    - Chọn trang muốn xem.
    - Thanh tìm kiếm sản phẩm (có thể tìm kiếm dù ở trang nào).
    - Hiển thị đề xuất tìm kiếm.
    - Thay đổi chế độ Light/Dark Mode.

4. **🔍 Trang tìm kiếm sản phẩm**
    - Hiển thị danh sách sản phẩm tìm kiếm theo từ khóa.
    - Hiển thị thông tin sản phẩm (hình ảnh, tên, giá, thương hiệu, đánh giá, tồn kho, đã bán).
    - Hiển thị tổng số lượng sản phẩm.
    - Hiển thị thông báo không tìm thấy sản phẩm.
    - Lọc sản phẩm theo:
       * Thương hiệu
       * Loại sản phẩm
       * Giá
    - Sắp xếp sản phẩm theo: 
        - Giá tăng dần, giảm dần
        - Tên sản phẩm A-Z, Z-A
        - Theo ngày đăng sản phẩm
5. **🛍️ Trang chi tiết sản phẩm**
    - Hiển thị toàn bộ thông tin sản phẩm (hình ảnh, tên, giá, thương hiệu, đánh giá, tồn kho, đã bán, mô tả).
    - Chọn số lượng sản phẩm để mua.
    - Chọn hình thức vận chuyển.
    - Thêm sản phẩm vào giỏ hàng.
    - Hiển thị thông tin đánh giá sản phẩm (người đánh giá, số sao).
    - Lọc đánh giá theo số sao.
6. **💳 Trang thanh toán**
    - Thay đổi thông tin địa chỉ.
    - Chọn hình thức vận chuyển.
    - Chọn voucher.
    - Hiển thị danh sách sản phẩm mua.
    - Hiển thị tổng giá tiền.
    - Mua sản phẩm.
7. **🛒 Trang giỏ hàng**
    - Hiển thị danh sách sản phẩm trong giỏ hàng.
    - Thay đổi số lượng sản phẩm.
    - Xóa sản phẩm khỏi giỏ hàng.
    - Hiển thị tổng giá tiền.
    - Chọn voucher.
8. **📦 Trang đơn hàng**
    - Hiển thị danh sách đơn hàng đang chờ duyệt, đã hoàn thành, đã bị hủy.
	- Hiển thị thông tin đơn hàng (ngày mua, tổng giá tiền, trạng thái đơn hàng).
	- Hiển thị thông tin sản phẩm trong đơn hàng.
    - Xác nhận nhận hàng.
    - Đánh giá sản phẩm nếu sản phẩm hoàn thành.
      - Đánh giá số sao cho từng sản phẩm trong đơn hàng.
9. **👤 Trang quản lý tài khoản cá nhân**
    - Thay đổi ảnh đại diện.
    - Thay đổi thông tin cá nhân (tên, email, số điện thoại, địa chỉ).
    - Thay đổi mật khẩu.
    - Đăng xuất.
    - Hiển thị tổng quan tài khoản (tổng đơn hàng, tổng tiền, tổng sản phẩm đã mua, level).
    - Hiển thị ngày tạo tài khoản.
10. **🛡️ Trang quản lý tài khoản (Admin)**
	- Hiển thị danh sách tài khoản người dùng.
	- Hiển thị thông tin tài khoản người dùng (tên, email, số điện thoại, địa chỉ, level).
	- Xóa tài khoản người dùng.
	- Tạo tài khoản người dùng.
11. **🛡️ Trang quản lý sản phẩm (Admin)**
	- Hiển thị danh sách sản phẩm.
	- Hiển thị thông tin sản phẩm (hình ảnh, tên, giá, thương hiệu, tồn kho, đã bán).
	- Sửa sản phẩm (hình ảnh, tên, giá, thương hiệu, tồn kho).
    - Thêm sản phẩm mới.
12. **🛡️ Trang quản lý đơn hàng (Admin)**
	- Hiển thị danh sách đơn hàng.
	- Hiển thị thông tin đơn hàng (ngày mua, tổng giá tiền, địa chỉ, trạng thái đơn hàng).
	- Hiển thị thông tin sản phẩm trong đơn hàng.
	- Xác nhận đơn hàng.
	- Hủy đơn hàng.

<br> <br>
## 🚀 **Công Nghệ và Kỹ Thuật Đã Sử Dụng**
- **Dependency Injection**: Quản lý và tối ưu hóa việc sử dụng các dịch vụ.
- **MVVM Pattern**: Xây dựng ứng dụng theo mô hình MVVM.
- **Event Aggregator Pattern**: Giao tiếp giữa các ViewModel, View.
- **Bất đồng bộ `async`, `await`**: Xử lý các tác vụ đồng thời.
- **SemaphoreSlim**: Đồng bộ hóa tránh xung đột với `SemaphoreSlim`.
- **Entity Framework Core**: Sử dụng để tương tác với Database.
- **Unit Test**: Kiểm thử cho chức năng.
- **Doxygen**: Tạo tài liệu cho mã nguồn.

## 📦 **NuGet Packages**
- `CommunityToolkit.Mvvm` (8.3.2)
- `Microsoft.EntityFrameworkCore` (9.0.0)
- `Microsoft.EntityFrameworkCore.Design` (9.0.0)
- `Microsoft.EntityFrameworkCore.SqlServer` (9.0.0)
- `Microsoft.EntityFrameworkCore.Tools` (9.0.0)
- `Microsoft.Extensions.DependencyInjection` (9.0.0)

## 📌 **Thông tin phiên bản**
- **.NET**: 9.0
- **Target OS:** Windows
- **Target OS Version:** 10.0.19041.0
