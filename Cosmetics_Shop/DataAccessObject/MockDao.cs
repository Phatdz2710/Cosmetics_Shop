using Cosmetics_Shop.Enums;
using Cosmetics_Shop.Models.Enums;
using Cosmetics_Shop.Services;
using Cosmetics_Shop.ViewModels;
using Microsoft.UI.Xaml.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Security.Authentication.Web.Provider;

namespace Cosmetics_Shop.Models.DataService
{
    public class MockDao : IDao
    {
        public async Task<SearchResult> GetListProductThumbnailAsync(
            string keyword = "",
            int pageIndex = 1,
            int productsPerPage = 10,
            SortProduct sortProduct = SortProduct.DateAscending,
            string filterBrand = "",
            string filerCategory = "",
            int minPrice = 0,
            int maxPrice = int.MaxValue)
        {
            //var db = new List<ProductThumbnail>();
            var db = new List<ProductThumbnail>()
            {
                new ProductThumbnail(1, "Moisturizing Cream", null, 250000, "Cetaphil"),
                new ProductThumbnail(2, "Sunscreen SPF 50", null, 300000, "La Roche-Posay"),
                new ProductThumbnail(3, "Vitamin C Serum", null, 400000, "Skinceuticals"),
                new ProductThumbnail(4, "Anti-Aging Night Cream", null, 500000, "Olay"),
                new ProductThumbnail(5, "Lip Balm", null, 100000, "Burt's Bees"),
                new ProductThumbnail(6, "Eye Cream", null, 350000, "Cetaphil"),
                new ProductThumbnail(7, "Makeup Remover", null, 150000, "La Roche-Posay"),
                new ProductThumbnail(8, "Facial Cleanser", null, 200000, "Skinceuticals"),
                new ProductThumbnail(9, "Hydrating Toner", null, 180000, "Olay"),
                new ProductThumbnail(10, "Exfoliating Scrub", null, 220000, "Burt's Bees"),
                new ProductThumbnail(11, "Sheet Mask", null, 50000, "Cetaphil"),
                new ProductThumbnail(12, "Clay Mask", null, 250000, "La Roche-Posay"),
                new ProductThumbnail(13, "BB Cream", null, 320000, "Skinceuticals"),
                new ProductThumbnail(14, "Foundation", null, 350000, "Olay"),
                new ProductThumbnail(15, "Blush", null, 150000, "Burt's Bees"),
                new ProductThumbnail(16, "Lipstick", null, 200000, "Cetaphil"),
                new ProductThumbnail(17, "Concealer", null, 250000, "La Roche-Posay"),
                new ProductThumbnail(18, "Mascara", null, 220000, "Skinceuticals"),
                new ProductThumbnail(19, "Eyeliner", null, 150000, "Olay"),
                new ProductThumbnail(20, "Brow Pencil", null, 120000, "Burt's Bees"),
                new ProductThumbnail(21, "Face Serum", null, 450000, "Cetaphil"),
                new ProductThumbnail(22, "Facial Oil", null, 500000, "La Roche-Posay"),
                new ProductThumbnail(23, "Body Lotion", null, 180000, "Skinceuticals"),
                new ProductThumbnail(24, "Hand Cream", null, 120000, "Olay"),
                new ProductThumbnail(25, "Perfume", null, 600000, "Burt's Bees"),
                new ProductThumbnail(26, "Body Wash", null, 150000, "Cetaphil"),
                new ProductThumbnail(27, "Hair Conditioner", null, 200000, "La Roche-Posay"),
                new ProductThumbnail(28, "Shampoo", null, 180000, "Skinceuticals"),
                new ProductThumbnail(29, "Deodorant", null, 100000, "Olay"),
                new ProductThumbnail(30, "Nail Polish", null, 90000, "Burt's Bees"),
                new ProductThumbnail(31, "Face Serum", null, 450000, "Cetaphil"),
                new ProductThumbnail(32, "Facial Oil", null, 500000, "La Roche-Posay"),
                new ProductThumbnail(33, "Body Lotion", null, 180000, "Skinceuticals"),
                new ProductThumbnail(34, "Hand Cream", null, 120000, "Olay"),
                new ProductThumbnail(35, "Perfume", null, 600000, "Burt's Bees"),
                new ProductThumbnail(36, "Body Wash", null, 150000, "Cetaphil"),
                new ProductThumbnail(37, "Hair Conditioner", null, 200000, "La Roche-Posay"),
                new ProductThumbnail(38, "Shampoo", null, 180000, "Skinceuticals"),
                new ProductThumbnail(39, "Deodorant", null, 100000, "Olay"),
                new ProductThumbnail(40, "Nail Polish", null, 90000, "Burt's Bees"),
            };

            return await Task.Run(() =>
            {
                // Filter by keyword
                if (!string.IsNullOrEmpty(keyword))
                {
                    db = db.Where(p => p.Name.ToLower().Contains(keyword.ToLower())).ToList();
                }

                if (!string.IsNullOrEmpty(filterBrand))
                {
                    db = db.Where(p => p.Brand == filterBrand).ToList();
                }

                db = db.Where(p => p.Price >= minPrice && p.Price <= maxPrice).ToList();

                var resBrands = db.Select(p => p.Brand).Distinct().ToList();

                int totalProduct = db.Count;

                // Paging
                db = db.Skip((pageIndex - 1) * productsPerPage).Take(productsPerPage).ToList();

                int numPages = totalProduct / productsPerPage + (totalProduct % productsPerPage != 0 ? 1 : 0);
                if (numPages == 0) numPages = 1;

                return new SearchResult()
                {
                    Products = db,
                    TotalPages = numPages,
                    TotalProducts = totalProduct,
                    Brands = resBrands
                };
            });

        }

        public List<ProductDetail> GetListProductDetail()
        {
            var db = new List<ProductDetail>()
            {
                new ProductDetail(1, "Moisturizing Cream", null, 250000, 2700, 10000, 1792, "Kem dưỡng ẩm",
                                   "Hà Nội", "La Roche-Posay", "Hà Nội",
                                   "Với sự kết hợp của 3 ceramides thiết yếu và hyaluronic acid, " +
                                   "kem dưỡng giúp duy trì độ ẩm và phục hồi lớp hàng rào bảo vệ cho da mặt và " +
                                   "toàn thân\r\n\r\nSản phẩm từ CeraVe - nhãn hiệu chăm sóc da số 1 tại " +
                                   "Mỹ được các chuyên gia chăm sóc da khuyên dùng.\r\n\r\n\r\n\r\nTHÔNG TIN" +
                                   " THƯƠNG HIỆU\r\n\r\nCeraVe là nhãn hiệu dưỡng ẩm #1 tại Mỹ được các chuyên " +
                                   "gia chăm sóc da khuyên dùng. Vào năm 2005, một hội đồng các bác sĩ, chuyên gia " +
                                   "chăm sóc da đã phát triển một công nghệ mới có chứa Ceramides 1, 3 và 6-II (giống h" +
                                   "ệt với cấu trúc Ceramides được chứng minh thiếu hụt trong một số bệnh về da), axi" +
                                   "t béo và các chất béo khác được tăng cường với hệ thống phân phối đột phá được gọi l" +
                                   "à MultiVesicular Emulsion Technology - MVE. Công thức của 3 loại CERAmides thiết" +
                                   " yếu và công nghệ mVE là sự ra đời của CeraVe" ),
                new ProductDetail(2, "Sunscreen SPF 50", null, 300000, 1000, 5000, 179, "Kem chống nắng",
                                    "Thành phố Hồ Chí Minh", "La Roche-Posay", "Thành phố Hồ Chí Minh",
                                    "Kem chống nắng Sunplay Super Block dùng được cho mặt và toàn thân, " +
                                    "thích hợp đi biển, bơi lội, du lịch, leo núi, thể thao. " +
                                    "Sản phẩm chống nắng dùng cho các hoạt động liên tục ngoài trời hoặc dư" +
                                    "ới nước như: đi biển, bơi lội, dã ngoại, du lịch, leo núi, thể thao,… " +
                                    "với UVA Max Defense tạo màng chắn tia UVA tối đa PA++++, SPF81 chống tia UVB cực mạnh. S" +
                                    "ữa chống nắng không chứa cồn, không gây khô da.\r\n\r\nĐặc tính sản ph" +
                                    "ẩm:\r\n- Kem chống nắng Sunplay Super Block chứa Vitamin C, Vitamin E, Pro " +
                                    "Vitamin B5, Hyaluronic Acid giúp giữ ẩm và nuôi dưỡng làn da, khả năng kháng" +
                                    " nước và mồ hôi cao.\r\n- Sunplay Super Block mới với khả năng chống nắng cực mạnh" +
                                    " và hiệu quả trong nhiều giờ liền, tính kháng nước và mồ hôi cao, bảo đảm hai tác động b" +
                                    "ảo vệ da và nuôi dưỡng da khi phải vận động liên tục ngoài trời hoặc dưới nước.\r\n-" +
                                    " PA ++++ tạo màng chắn tia UVA tối đa, chống mọi loại tia UVA, giúp ngăn đen sạm, nám d" +
                                    "a, lão hóa da sớm, tránh ung thư da; đồng thời SPF81 chống tia UVB cực mạnh, bảo vệ da k" +
                                    "hông bị bỏng rát, cháy nắng trong nhiều giờ liền.\r\n- Kem chống nắng Sunplay còn chứa V" +
                                    "itamin C, Vitamin E, Pro Vitamin B5, Hyaluronic Acid giúp giữ ẩm và nuôi dưỡng làn da, " +
                                    "khả năng kháng nước và mồ hôi cao.\r\n- Sản phẩm dạng sữa cho mặt và toàn thân, thoáng " +
                                    "mịn, không gây khô da, không chứa cồn, kháng nước tốt."
                                    ),

                new ProductDetail(3, "Vitamin C Serum", null, 400000, 500, 2500, 250, "Vitamin C",
                                    "Cần Thơ", "La Roche-Posay", "Cần Thơ",
                                    "Serum trắng da, mờ thâm Balance Active Formula Vitamin " +
                                    "C Brightening 30ml với thành phần chính là Vitamin C nhưng tối " +
                                    "ưu hơn nhờ các dạng thức ổn định và hiệu quả nhất của Vitamin C. Sản" +
                                    " phẩm chứa Vitamin C với nồng độ  an toàn cho da, không gây kích ứng và d" +
                                    "ễ bảo quản.\r\n\r\nCÔNG DỤNG:\r\n\r\n- Làm mờ các vết thâm mụn dù là mới" +
                                    " hay lâu năm\r\n\r\n- Giúp bật tone da trắng hơn, đồng đều màu da.\r\n\r\n- Gi" +
                                    "ảm thiểu các vết thâm và chống oxi hóa\r\n\r\n- Sản sinh collagen giúp da săn " +
                                    "chắc\r\n\r\n- Tăng cường hiệu quả chống nắng khi sử dụng chung với sản phẩm chố" +
                                    "ng nắng\t\r\n\r\nĐIỂM NỔI BẬT:\r\n\r\n- Thiết kế chai bằng nhựa, nhẹ, dễ cầm, " +
                                    "không lo rơi vỡ\r\n\r\n- Không cần bảo quản tủ lạnh, bảo quản nhiệt độ phòng\r\n\r\n- " +
                                    "Nồng độ C an toàn cho da mụn và da tuổi vị thành niên từ 15 tuổi\t"
                                ),
            };

            // Filter by keyword

            return db;
        }

        public ProductDetail GetProductDetail(int idProduct)
        {
            var db = GetListProductDetail();
            for (int i = 0; i < db.Count; i++)
            {
                if (idProduct == db[i].Id)
                {
                    return db[i];
                }
            }
            return null;
        }

        public async Task<List<ProductThumbnail>> GetListNewProductAsync()
        {
            var db = new List<ProductThumbnail>()
            {
                new ProductThumbnail(1, "Moisturizing Cream", null, 250000, "La Roche-Posay"),
                new ProductThumbnail(2, "Sunscreen SPF 50", null, 300000, "La Roche-Posay"),
                new ProductThumbnail(3, "Vitamin C Serum", null, 400000, "La Roche-Posay"),
                new ProductThumbnail(1, "Moisturizing Cream", null, 250000, "La Roche-Posay"),
                new ProductThumbnail(2, "Sunscreen SPF 50", null, 300000, "La Roche-Posay"),
                new ProductThumbnail(3, "Vitamin C Serum", null, 400000, "La Roche-Posay"),
                new ProductThumbnail(1, "Moisturizing Cream", null, 250000, "La Roche-Posay"),
                new ProductThumbnail(2, "Sunscreen SPF 50", null, 300000, "La Roche-Posay"),
                new ProductThumbnail(3, "Vitamin C Serum", null, 400000, "La Roche-Posay"),
                new ProductThumbnail(1, "Moisturizing Cream", null, 250000, "La Roche-Posay"),
                new ProductThumbnail(2, "Sunscreen SPF 50", null, 300000, "La Roche-Posay"),
                new ProductThumbnail(3, "Vitamin C Serum", null, 400000, "La Roche-Posay"),
                new ProductThumbnail(1, "Moisturizing Cream", null, 250000, "La Roche-Posay"),
                new ProductThumbnail(2, "Sunscreen SPF 50", null, 300000, "La Roche-Posay"),
                new ProductThumbnail(3, "Vitamin C Serum", null, 400000, "La Roche-Posay"),
                new ProductThumbnail(1, "Moisturizing Cream", null, 250000, "La Roche-Posay"),
                new ProductThumbnail(2, "Sunscreen SPF 50", null, 300000, "La Roche-Posay"),
                new ProductThumbnail(3, "Vitamin C Serum", null, 400000, "La Roche-Posay"),
            };

            return db;
        }
        public async Task<List<ProductThumbnail>> GetListBestSellerAsync()
        {
            var db = new List<ProductThumbnail>()
            {
                new ProductThumbnail(1, "Moisturizing Cream", null, 250000, "La Roche-Rosay"),
                new ProductThumbnail(2, "Sunscreen SPF 50", null, 300000, "La Roche-Rosay"),
                new ProductThumbnail(3, "Vitamin C Serum", null, 400000, "La Roche-Rosay"),
            };

            return db;
        }

        public async Task<List<ProductThumbnail>> GetListRecentlyViewAsync()
        {
            var db = new List<ProductThumbnail>()
            {
                new ProductThumbnail(1, "Moisturizing Cream", null, 250000, "La Roche-Rosay"),
                new ProductThumbnail(2, "Sunscreen SPF 50", null, 300000, "La Roche-Rosay"),
                new ProductThumbnail(3, "Vitamin C Serum", null, 400000, "La Roche-Rosay"),
            };

            return db;
        }

        public void InsertProduct(ProductThumbnail product)
        {
            throw new NotImplementedException();
        }

        public async Task<LoginResult> CheckLoginAsync(string username, string password)
        {
            List<Tuple<string, string, string, string>> _listAccount = new List<Tuple<string, string, string, string>>()
            {
                new Tuple<string, string, string, string>("admin", "admin", "Admin", "1234"),
                new Tuple<string, string, string, string>("user", "user", "User", "2345")
            };

            bool isExistUsername = _listAccount.Any(account => account.Item1 == username);

            if (!isExistUsername)
            {
                return new LoginResult()
                {
                    LoginStatus = LoginStatus.InvalidUsername,
                     
                };

            }

            bool isCorrectPassword = _listAccount.Any(account => account.Item1 == username && account.Item2 == password);
            if (!isCorrectPassword)
            {
                return new LoginResult()
                {
                    LoginStatus = LoginStatus.InvalidPassword,
                    
                };
            }

            return new LoginResult()
            {
                LoginStatus = LoginStatus.Success,
            };
        }

        public List<CartThumbnail> GetListCartProduct()
        {
            var db = new List<CartThumbnail>()
            {
                new CartThumbnail(1, null, "Tẩy trang loreal", "Tươi mát", 150000, 2, 300000),
                new CartThumbnail(2, null, "Tẩy trang loreal", "Sạch sâu", 150000, 2, 300000),
                new CartThumbnail(3, null, "Tẩy trang Bioderma", "Tươi mát", 150000, 1, 150000),
                new CartThumbnail(4, null, "Tẩy trang Bioderma", "Sạch sâu", 150000, 2, 300000),
                new CartThumbnail(5, null, "Tẩy trang Ganier", "BHA", 130000, 1, 130000),
                new CartThumbnail(6, null, "Kem chống nắng skinaqua", "Vạch vàng", 125000, 1, 125000)
            };

            return db;
        }

        public List<ReviewThumbnail> GetListReviewThumbnail()
        {
            var db = new List<ReviewThumbnail>()
            {
                new ReviewThumbnail(1, "Thnhcng", null, 5),
                new ReviewThumbnail(2, "Thnhcng", null, 4),
                new ReviewThumbnail(3, "Thnhcng", null, 5),
                new ReviewThumbnail(1, "Ngocphat", null, 3),
                new ReviewThumbnail(2, "Ngocphat", null, 4),
                new ReviewThumbnail(3, "Ngocphat", null, 5),
                new ReviewThumbnail(1, "Ciel", null, 4),
                new ReviewThumbnail(2, "Ciel", null, 4),
                new ReviewThumbnail(3, "Ciel", null, 3)
            };
            return db;
        }

        public List<ReviewThumbnail> GetListReviewThumbnailByIDProduct(int idProduct)
        {
            var db = GetListReviewThumbnail();
            var result = new List<ReviewThumbnail>();
            for (int i = 0; i < db.Count; i++)
            {
                if (idProduct == db[i].Id)
                {
                    result.Add(db[i]);
                }
            }
            return result;

        }

        public async Task<List<string>> GetSuggestionsAsync(string keyword)
        {
            var suggestions = new List<string> { "Apple", "Application", "Banana", "Cherry", "Date", "Grape" };

            // Lọc gợi ý dựa trên văn bản nhập
            var filtered = suggestions.Where(s => s.StartsWith(keyword, StringComparison.OrdinalIgnoreCase))
                                        .Take(10).ToList();

            return filtered;
        }

        //// Method to get all vouchers
        public List<Voucher> GetAllVouchers()
        {
            var db = new List<Voucher>
            {
            new Voucher (1,"SAVE10",10,DateTime.Now.AddDays(30), "Giảm 10%"),
            new Voucher (2,"SAVE20",20,DateTime.Now.AddDays(60), "Giảm 20%"),
            new Voucher (3, "SAVE30", 30, DateTime.Now.AddDays(10), "Giảm 30%"),
            new Voucher (4, "WELCOME", 50, DateTime.Now.AddDays(15), "Giảm 50% - Lần đầu mua hàng")
            };

            return db;
        }

        public List<PaymentProductThumbnail> GetAllPaymentProducts()
        {
            var db = new List<PaymentProductThumbnail>
            {
                new PaymentProductThumbnail(1, null, "Tẩy trang loreal", "Tươi mát", 150000, 2),
                new PaymentProductThumbnail(2, null, "Tẩy trang loreal", "Sạch sâu", 150000, 2),
                new PaymentProductThumbnail(3, null, "Tẩy trang Bioderma", "Tươi mát", 150000, 1)
            };

            return db;
        }

        public List<ShippingMethod> GetShippingMethods()
        {
            var db = new List<ShippingMethod>
            {
                new ShippingMethod(1, "Vận chuyển nhanh", 20000),
                new ShippingMethod(2, "Vận chuyển hỏa tốc", 54000),
                new ShippingMethod(3, "Vận chuyển tiết kiệm", 16500)
            };
            return db;
        }

        public Task<SignupStatus> DoSignupAsync(string username, string password, string confirmPassword, string email)
        {
            throw new NotImplementedException();
        }

        public Task<UserDetail> GetUserDetailAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<object> GetInformationAsync(int userId, UserInformationType infoType)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ChangeUserInformationAsync(int userId, UserInformationType infoType, string newValue)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ChangeAllUserInformationAsync(int userId, UserDetail userDetail)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ChangePasswordAsync(int userId, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }
    }
}
