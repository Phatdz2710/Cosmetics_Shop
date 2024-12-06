﻿using Cosmetics_Shop.DataAccessObject.Data;
using Cosmetics_Shop.DBModels;
using Cosmetics_Shop.Enums;
using Cosmetics_Shop.Models.Enums;
using Cosmetics_Shop.Services;
using Cosmetics_Shop.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.ApplicationModel.Appointments;
using Windows.ApplicationModel.Background;
using Windows.Networking.NetworkOperators;
using Windows.Security.Authentication.Web.Provider;

namespace Cosmetics_Shop.Models.DataService
{
    public class SqlDao : IDao
    {
        private readonly IServiceProvider _serviceProvider = null;
        #region Get Product Thumbnails
        public SqlDao(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<SearchResult> GetListProductThumbnailAsync(
            string  keyword     = "",
            int     pageIndex   = 1,
            int     productsPerPage = 10,
            SortProduct sortProduct = SortProduct.DateAscending,
            string  filterBrand     = "",
            string  filterCategory  = "",
            int     minPrice    = 0,
            int     maxPrice    = int.MaxValue)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                try
                {
                    var _databaseContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
                    var query = _databaseContext.Products.AsQueryable();

                    if (!string.IsNullOrEmpty(keyword))
                    {
                        query = query.Where(p => p.Name.Contains(keyword));
                    }

                    if (!string.IsNullOrEmpty(filterBrand))
                    {
                        query = query.Where(p => p.Brand == filterBrand);
                    }

                    if (!string.IsNullOrEmpty(filterCategory))
                    {
                        query = query.Where(p => p.Category == filterCategory);
                    }

                    query = query.Where(p => p.Price >= minPrice && p.Price <= maxPrice); 

                    query = sortProduct switch
                    {
                        SortProduct.DateAscending   => query.OrderByDescending(p => p.CreatedAt),
                        SortProduct.PriceAscending  => query.OrderBy(p => p.Price),
                        SortProduct.PriceDescending => query.OrderByDescending(p => p.Price),
                        SortProduct.NameAscending   => query.OrderBy(p => p.Name),
                        SortProduct.NameDescending  => query.OrderByDescending(p => p.Name),
                        _ => query,
                    };

                    var totalProduct = await query.CountAsync();
                    var numPages = (int)Math.Ceiling((double)totalProduct / productsPerPage);

                    var db = await query.Skip((pageIndex - 1) * productsPerPage)
                                  .Take(productsPerPage)
                                  .Select(p => new ProductThumbnail(p.Id,
                                        p.Name,
                                        p.ImagePath,
                                        p.Price,
                                        p.Brand,
                                        p.Category,
                                        p.AverageRating,
                                        p.Sold,
                                        p.Stock))
                                  .ToListAsync();

                    var resBrands = await _databaseContext.Products
                                        .Where(p => query.Select(q => q.Brand)
                                        .Contains(p.Brand))
                                        .Select(p => p.Brand)
                                        .Distinct()
                                        .ToListAsync();

                    var resCategories = await _databaseContext.Products
                                        .Where(p => query.Select(q => q.Category)
                                        .Contains(p.Category))
                                        .Select(p => p.Category)
                                        .Distinct()
                                        .ToListAsync();

                    return new SearchResult()
                    {
                        Products        = db,
                        TotalPages      = numPages == 0 ? 1 : numPages,
                        TotalProducts   = totalProduct,
                        Brands          = resBrands,
                        Categories      = resCategories
                    };
                }
                catch (Exception)
                {
                    return new SearchResult()
                    {
                        Products        = new List<ProductThumbnail>(),
                        TotalPages      = 1,
                        TotalProducts   = 0,
                        Brands          = new List<string>(),
                        Categories      = new List<string>()
                    };
                }
            }
        }

        public async Task<List<ProductThumbnail>> GetListNewProductAsync()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _databaseContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
                try
                {
                    var db = await _databaseContext.Products
                        .OrderByDescending(p => p.CreatedAt)
                        .Take(6)
                        .Select(p => new ProductThumbnail(p.Id,
                                p.Name,
                                p.ImagePath,
                                p.Price,
                                p.Brand,
                                p.Category,
                                p.AverageRating,
                                p.Sold,
                                p.Stock))
                        .ToListAsync();

                    return db;
                }
                catch (Exception)
                {
                    return new List<ProductThumbnail>();
                }
            }

        }
        public async Task<List<ProductThumbnail>> GetListBestSellerAsync()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _databaseContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
                try
                {
                    var db = await _databaseContext.Products
                        .OrderByDescending(p => p.AverageRating)
                        .Take(6)
                        .Select(p => new ProductThumbnail(p.Id,
                                p.Name,
                                p.ImagePath,
                                p.Price,
                                p.Brand,
                                p.Category,
                                p.AverageRating,
                                p.Sold,
                                p.Stock))
                        .ToListAsync();

                    return db;
                }
                catch (Exception)
                {
                    return new List<ProductThumbnail>();
                }
            }
        }

        public async Task<List<ProductThumbnail>> GetListRecentlyViewAsync()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _databaseContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
                var userSession = _serviceProvider.GetService(typeof(UserSession)) as UserSession;
                try
                {
                    var query = _databaseContext.Orders.AsQueryable();

                    var db = await query.Where(p => p.UserId == userSession.GetId())
                        .OrderByDescending(p => p.OrderDate)
                        .SelectMany(p => p.OrderItems)
                        .Select(p => p.Product)
                        .Distinct()
                        .Take(6)
                        .Select(p => new ProductThumbnail(p.Id,
                                p.Name,
                                p.ImagePath,
                                p.Price,
                                p.Brand,
                                p.Category,
                                p.AverageRating,
                                p.Sold,
                                p.Stock))
                        .ToListAsync();

                    return db;
                }
                catch (Exception)
                {
                    return new List<ProductThumbnail>();
                }
            }
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

        #endregion

        #region Login, Signup

        public async Task<LoginResult> CheckLoginAsync(string username, string password)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _databaseContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
                try
                {
                    var account = await _databaseContext.Accounts.FirstOrDefaultAsync(p => p.Username == username);

                    if (account == null)
                    {

                        return new LoginResult
                        {
                            LoginStatus = LoginStatus.InvalidUsername,
                            UserInfo    = new User(0, "", "")
                        };
                    }

                    if (account.Password != password)
                    {
                        return new LoginResult
                        {
                            LoginStatus = LoginStatus.InvalidPassword,
                            UserInfo    = new User(0, "", "")
                        };
                    }

                    return new LoginResult
                    {
                        LoginStatus = LoginStatus.Success,
                        UserInfo    = new User(account.UserId, account.Token, account.Role)
                    };
                }
                catch (Exception)
                {
                    return new LoginResult
                    {
                        LoginStatus = LoginStatus.ConnectServerFailed,
                        UserInfo    = new User(0 ,"", "")
                    };
                }
            }
        }

        public async Task<SignupStatus> DoSignupAsync(string username,
                                                    string password,
                                                    string confirmPassword,
                                                    string email)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _databaseContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
                try
                {
                    if (username.IsNullOrEmpty()) return SignupStatus.EmptyUsername;
                    if (password.IsNullOrEmpty()) return SignupStatus.EmptyPassword;
                    if (password != confirmPassword) return SignupStatus.ConfirmPasswordWrong;

                    var user = await _databaseContext.Accounts.FirstOrDefaultAsync(p => p.Username == username);

                    if (user != null) return SignupStatus.UsernameAlreadyExists;

                    var newUser = new DBModels.User()
                    {
                        Name    = username,
                        Email   = email,
                        Address = null,
                        Phone   = null,
                    };
                    await _databaseContext.Users.AddAsync(newUser);
                    await _databaseContext.SaveChangesAsync();

                    var newAccount = new DBModels.Account()
                    {
                        Username    = username,
                        Password    = password,
                        Token       = Guid.NewGuid().ToString(),
                        Role        = "User",
                        UserId      = newUser.Id
                    };

                    await _databaseContext.Accounts.AddAsync(newAccount);
                    await _databaseContext.SaveChangesAsync();


                    // Return if signup was successful
                    return SignupStatus.Success;
                }
                catch (Exception)
                {
                    return SignupStatus.ConnectServerFailed;
                }
            }
        }

        #endregion

        #region User Informations

        public async Task<UserDetail> GetUserDetailAsync(int userId)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _databaseContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
                try
                {
                    var user = await _databaseContext.Users.FirstOrDefaultAsync(p => p.Id == userId);

                    var totalProducts = await _databaseContext.Orders
                        .Where(p => p.UserId == userId)
                        .SelectMany(p => p.OrderItems)
                        .SumAsync(p => p.Quantity);

                    var totalBills = await _databaseContext.Orders
                        .Where(p => p.UserId == userId)
                        .CountAsync();

                    var totalMoneySpent = await _databaseContext.Orders
                        .Where(p => p.UserId == userId)
                        .SelectMany(p => p.OrderItems)
                        .SumAsync(p => p.Quantity * p.Product.Price);

                    if (user == null)
                    {
                        return new UserDetail
                        {
                            Name        = "",
                            Email       = "",
                            Phone       = "",
                            Address     = "",
                            AvatarPath  = null,
                            TotalMoneySpent = 0,
                            TotalBills      = 0,
                            TotalProducts   = 0
                        };
                    }

                    return new UserDetail
                    {
                        Name        = user.Name,
                        Email       = user.Email,
                        Phone       = user.Phone,
                        Address     = user.Address,
                        AvatarPath  = user.AvatarPath,
                        TotalMoneySpent = totalMoneySpent,
                        TotalBills      = totalBills,
                        TotalProducts   = totalProducts
                    };
                }
                catch (Exception)
                {
                    return new UserDetail
                    {
                        Name        = "",
                        Email       = "",
                        Phone       = "",
                        Address     = "",
                        AvatarPath  = null,
                        TotalMoneySpent = 0,
                        TotalBills      = 0,
                        TotalProducts   = 0
                    };
                }
            }
        }

        public async Task<object> GetInformationAsync(int userId, UserInformationType infoType)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _databaseContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();

                try
                {
                    switch(infoType)
                    {
                        // Get user's name
                        case UserInformationType.Name:
                            var name = await _databaseContext.Users
                                .Where(p => p.Id == userId)
                                .Select(p => p.Name)
                                .FirstOrDefaultAsync();
                            return name;

                        // Get user's email
                        case UserInformationType.Email:
                            var email = await _databaseContext.Users
                                .Where(p => p.Id == userId)
                                .Select(p => p.Email)
                                .FirstOrDefaultAsync();
                            return email;

                        // Get user's phone number
                        case UserInformationType.Phone:
                            var phone = await _databaseContext.Users
                                .Where(p => p.Id == userId)
                                .Select(p => p.Phone)
                                .FirstOrDefaultAsync();
                            return phone;

                        // Get user's address
                        case UserInformationType.Address:
                            var address = await _databaseContext.Users
                                .Where(p => p.Id == userId)
                                .Select(p => p.Address)
                                .FirstOrDefaultAsync();
                            return address;
                        
                        // Get user's avatar path
                        case UserInformationType.AvatarPath:
                            var avatarPath = await _databaseContext.Users
                                .Where(p => p.Id == userId)
                                .Select(p => p.AvatarPath)
                                .FirstOrDefaultAsync();
                            return avatarPath;

                        // Get total money spent by user
                        case UserInformationType.TotalMoneySpent:
                            var totalMoneySpent = await _databaseContext.Orders
                                .Where(p => p.UserId == userId)
                                .SelectMany(p => p.OrderItems)
                                .SumAsync(p => p.Quantity * p.Product.Price);
                            return totalMoneySpent;

                        // Get total bills of user
                        case UserInformationType.TotalBills:
                            var totalBills = await _databaseContext.Orders
                                .Where(p => p.UserId == userId)
                                .CountAsync();
                            return totalBills;

                        // Get total products bought by user
                        case UserInformationType.TotalProducts:
                            var totalProducts = await _databaseContext.Orders
                                .Where(p => p.UserId == userId)
                                .SelectMany(p => p.OrderItems)
                                .SumAsync(p => p.Quantity);
                            return totalProducts;

                        default:
                            return null;
                    }
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public async Task<bool> ChangeUserInformationAsync (int userId, UserInformationType infoType, string newValue)  
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _databaseContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();

                try
                {
                    var user = await _databaseContext.Users.FirstOrDefaultAsync(p => p.Id == userId);

                    if (user == null)
                    {
                        return false;
                    }

                    switch (infoType)
                    {
                        case UserInformationType.Name:
                            user.Name = newValue;
                            break;

                        case UserInformationType.Email:
                            user.Email = newValue;
                            break;

                        case UserInformationType.Phone:
                            user.Phone = newValue;
                            break;

                        case UserInformationType.Address:
                            user.Address = newValue;
                            break;

                        case UserInformationType.AvatarPath:
                            user.AvatarPath = newValue;
                            break;

                        default:
                            return false;
                    }

                    await _databaseContext.SaveChangesAsync();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
        
        public async Task<bool> ChangeAllUserInformationAsync(int userId, UserDetail userDetail)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _databaseContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();

                try
                {
                    var user = await _databaseContext.Users.FirstOrDefaultAsync(p => p.Id == userId);

                    if (user == null)
                    {
                        return false;
                    }

                    user.Name       = userDetail.Name;
                    user.Email      = userDetail.Email;
                    user.Phone      = userDetail.Phone;
                    user.Address    = userDetail.Address;
                    user.AvatarPath = userDetail.AvatarPath;

                    await _databaseContext.SaveChangesAsync();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public async Task<bool> ChangePasswordAsync(int userId, string oldPassword, string newPassword)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _databaseContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();

                try
                {
                    var account = await _databaseContext.Accounts.FirstOrDefaultAsync(p => p.UserId == userId);

                    if (account == null)
                    {
                        return false;
                    }

                    if (account.Password != oldPassword)
                    {
                        return false;
                    }

                    account.Password = newPassword;
                    await _databaseContext.SaveChangesAsync();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }

            }
        }
        #endregion

        #region For Admin

        public async Task<AccountSearchResult> GetListAccountAsync()
        {
            try
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var _databaseContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();

                    var db = await _databaseContext.Accounts
                        .Select(p => new Account(p.UserId, p.Username, p.Password, p.Role))
                        .ToListAsync();

                    return new AccountSearchResult
                    {
                        ListAccounts = db,
                        TotalPages = 1,
                        TotalAccounts = db.Count
                    };
                }
            }
            catch (Exception)
            {
                return new AccountSearchResult
                {
                    ListAccounts = new List<Account>(),
                    TotalPages = 1,
                    TotalAccounts = 0
                };

            }
        }

        public async Task<bool> ChangeAccountInfoAsync(int id, string newUsername, string newPassword, string newRole)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _databaseContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();

                try
                {
                    var account = await _databaseContext.Accounts.FirstOrDefaultAsync(p => p.UserId == id);

                    if (account == null)
                    {
                        return false;
                    }

                    // Check new username
                    var user = await _databaseContext.Accounts.FirstOrDefaultAsync(p => p.Username == newUsername && p.Id != id);
                    if (user != null)
                    {
                        return false;
                    }

                    account.Username = newUsername;
                    account.Password = newPassword;
                    account.Role = newRole;

                    await _databaseContext.SaveChangesAsync();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public Task<bool> DeleteAccount(int id)
        {
            return Task.Run(() =>
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var _databaseContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();

                    try
                    {
                        var account = _databaseContext.Accounts.FirstOrDefault(p => p.UserId == id);

                        if (account == null)
                        {
                            return false;
                        }

                        _databaseContext.Accounts.Remove(account);
                        _databaseContext.SaveChanges();
                        return true;
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                }
            });
        }

        public async Task<bool> CreateAccountAsync(string username, string password, string role)
        {
            try
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var _databaseContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();

                    var user = await _databaseContext.Accounts.FirstOrDefaultAsync(p => p.Username == username);

                    if (user != null)
                    {
                        return false;
                    }

                    var newUser = new DBModels.User()
                    {
                        Name = username,
                        Email = null,
                        Address = null,
                        Phone = null,
                    };
                    await _databaseContext.Users.AddAsync(newUser);
                    await _databaseContext.SaveChangesAsync();

                    var newAccount = new DBModels.Account()
                    {
                        Username = username,
                        Password = password,
                        Token = Guid.NewGuid().ToString(),
                        Role = role,
                        UserId = newUser.Id
                    };

                    await _databaseContext.Accounts.AddAsync(newAccount);
                    await _databaseContext.SaveChangesAsync();

                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> ChangeProductInfoAsync(int id, string newName, string newBrand, string newCategory, int newPrice, int newSold, int newInventory, string newImagePath)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _databaseContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();

                try
                {
                    var product = await _databaseContext.Products.FirstOrDefaultAsync(p => p.Id == id);

                    if (product == null)
                    {
                        return false;
                    }

                    product.Name = newName;
                    product.Brand = newBrand;
                    product.Category = newCategory;
                    product.Price = newPrice;
                    product.Stock = newInventory;
                    product.Sold = newSold;
                    product.ImagePath = newImagePath;

                    await _databaseContext.SaveChangesAsync();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public async Task<bool> CreateProductAsync(string name, string brand, string category, int price, int sold, int inventory, string imagePath)
        {
            try
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var _databaseContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();

                    var newProduct = new DBModels.Product()
                    {
                        Name = name,
                        Brand = brand,
                        Category = category,
                        Price = price,
                        Stock = inventory,
                        Sold = sold,
                        ImagePath = imagePath,
                        AverageRating = 0,
                        CreatedAt = DateTime.Now
                    };

                    await _databaseContext.Products.AddAsync(newProduct);
                    await _databaseContext.SaveChangesAsync();

                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion

        public void InsertProduct(ProductThumbnail product)
        {
            throw new NotImplementedException();
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
            return await Task.Run(async () =>
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
                    var query = context.Products.AsQueryable();

                    if (!string.IsNullOrEmpty(keyword))
                    {
                        query = query.Where(p => p.Name.Contains(keyword));
                    }

                    var suggestions = await query.Select(p => p.Name).Distinct().ToListAsync();
                    return suggestions;
                }

            });
        }

        //private List<Voucher> vouchers;

        //// Method to get all vouchers
        //public List<Voucher> GetAllVouchers()
        //{
        //    var db = new List<Voucher>
        //    {
        //    new Voucher (1,"SAVE10",10,DateTime.Now.AddDays(30)),
        //    new Voucher (2,"SAVE20",20,DateTime.Now.AddDays(60)),
        //    new Voucher (3, "SAVE30", 30, DateTime.Now.AddDays(10)),
        //    new Voucher (4, "WELCOME", 50, DateTime.Now.AddDays(15))
        //    };

        //    return db;
        //}

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

    }
}
