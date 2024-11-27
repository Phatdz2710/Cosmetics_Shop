# Cosmetics Shop Application

## Thành viên
- **21120419** - Vũ Thành Công

## Chức năng đã hoàn thành
- **Trang thông tin chi tiết sản phẩm**: 
  - ***Các chức năng***
    - Hiển thị được thông tin chi tiết cơ bản của sản phẩm theo ID
    - Các chức năng tăng số lượng sản phẩm, xem chi phí vận chuyển hoàn thành
    - Chức năng mua ngay (chuyển qua trang thanh toán)
    - Chức năng quay lại trang trước đó.
  - ***Khu vực đánh giá***
    - Hiển thị được các đánh giá theo ID sản phẩm với số sao tương ứng
    - Lọc các đánh giá theo số sao, sau đó có button Alls để xem lại toàn bộ đánh giá
  - ***Các chức năng chưa làm***
    - Thêm vào giỏ hàng (Đã thực hiện các hàm, chờ insert vô Sql)
  - ***Các chức năng sẽ update sau***
    - Khu vực đánh giá: Hiển thị thêm số lượng đánh giá tại button Alls, số lượng đánh giá ứng với mỗi số lượng sao.

- **Trang giỏ hàng**:
  - ***Các chức năng***
    - Hiển thị được các sản phẩm có trong giỏ hàng
    - Các chức năng tăng số lượng thì tiền tương ứng tăng theo hoàn thành
    - Tổng số tiền thanh toán được tính và thay đổi theo:
      - Các sản phẩm nào được chọn (có nút chọn tất cả sản phẩm)
      - Áp dụng voucher
    - Chức năng mua ngay (chuyển qua trang thanh toán)
    - Chức năng quay lại trang trước đó.
  - ***Các chức năng chưa làm***
    - Xóa sản phẩm khỏi Cart

- **Trang thanh toán**:
  - ***Các chức năng***
    - Hiển thị được các sản phẩm cần thanh toán (mới lớp MockDao, chưa thực sự chuyển thông tin từ Cart hay Product Detail qua)
    - Tổng số tiền thanh toán được tính và thay đổi theo:
      - Các sản phẩm nào được chọn (có nút chọn tất cả sản phẩm)
      - Áp dụng voucher giảm giá
      - Áp dụng phí vận chuyển (tùy chọn)
    - Chức năng quay lại trang trước đó.
  - ***Các chức năng chưa làm***
    - Chức năng đặt hàng chưa hoàn thành
    - Tên, sdt, địa chỉ người dùng còn truyền thẳng, chưa binding (do chưa có dữ liệu)
