using Cosmetics_Shop.DataAccessObject.Data;
using Cosmetics_Shop.DataAccessObject.Interfaces;
using Cosmetics_Shop.DBModels;
using Cosmetics_Shop.Enums;
using Cosmetics_Shop.Models;
using Cosmetics_Shop.Models.Enums;
using Cosmetics_Shop.Services;
using Cosmetics_Shop.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.UI.Xaml.Documents;
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
using Windows.System;

namespace Cosmetics_Shop.DataAccessObject
{
    /// <summary>
    /// Data Access Object for SQL Server
    /// </summary>
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
                                        p.Stock,
                                        p.Sold))
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
                                p.Stock,
                                p.Sold))
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
                        .OrderByDescending(p => p.Sold)
                        .Take(5)
                        .Select(p => new ProductThumbnail(p.Id,
                                p.Name,
                                p.ImagePath,
                                p.Price,
                                p.Brand,
                                p.Category,
                                p.AverageRating,
                                p.Stock,
                                p.Sold))
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
                                p.Stock,
                                p.Sold))
                        .ToListAsync();

                    return db;
                }
                catch (Exception)
                {
                    return new List<ProductThumbnail>();
                }
            }
        }
       
        public async Task<List<ProductDetail>> GetProductDetailsAsync()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _databaseContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
                try
                {
                    var productDetails = await _databaseContext.Products
                        .Select(p => new ProductDetail(
                            p.Id,
                            p.Name,
                            p.ImagePath,
                            p.Price,
                            p.AverageRating,
                            p.Sold,
                            p.Stock,
                            p.Category,
                            "Default Warehouse",
                            p.Brand,
                            "Default Shipping",
                            p.Description))
                        .ToListAsync();

                    return productDetails;
                }
                catch (Exception)
                {
                    // Handle exceptions as needed
                    return new List<ProductDetail>();
                }
            }
        }

        public async Task<ProductDetail> GetProductDetailAsync(int idProduct)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _databaseContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
                try
                {
                    // Fetch the product detail directly from the database
                    var productDetail = await _databaseContext.Products
                        .Where(p => p.Id == idProduct)
                        .Select(p => new ProductDetail
                        (
                            p.Id,
                            p.Name,
                            p.ImagePath, 
                            p.Price,
                            p.AverageRating, 
                            p.Sold,
                            p.Stock,
                            p.Category,
                            "Default Warehouse", // Replace with actual warehouse logic if needed
                            p.Brand,
                            "Default Shipping", // Replace with actual shipping logic if needed
                            p.Description
                        ))
                        .FirstOrDefaultAsync(); // Use FirstOrDefaultAsync to get a single product detail

                    return productDetail; // This will return null if no product is found
                }
                catch (Exception)
                {
                    // Handle exceptions as needed
                    return null; // Return null in case of an error
                }
            }
        }
       
        public async Task<List<Models.Order>> GetListOrderAsync(int userId, OrderStatus orderStatus)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _databaseContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
                try
                { 
                    if (orderStatus == OrderStatus.InProcess)
                    {
                        var orders = await _databaseContext.Orders
                        .Where(p => p.UserId == userId && (p.OrderStatus == 0 || p.OrderStatus == 1))
                        .Select(p => new Models.Order
                        {
                            Id = p.Id,
                            UserId = p.UserId,
                            OrderStatus = p.OrderStatus,
                            OrderDate = p.OrderDate,
                            ShippingMethod = p.ShippingMethod,
                            PaymentMethod = p.PaymentMethod,
                            VoucherId = p.VoucherId,
                            ShippingAddress = p.ShippingAddress,
                            TotalPrice = p.TotalPrice
                        })
                        .ToListAsync();

                        return orders;
                    }
                    else
                    {
                        var orders = await _databaseContext.Orders
                            .Where(p => p.UserId == userId && p.OrderStatus == (int)orderStatus)
                            .Select(p => new Models.Order
                            {
                                Id = p.Id,
                                UserId = p.UserId,
                                OrderStatus = p.OrderStatus,
                                OrderDate = p.OrderDate,
                                ShippingMethod = p.ShippingMethod,
                                PaymentMethod = p.PaymentMethod,
                                VoucherId = p.VoucherId,
                                ShippingAddress = p.ShippingAddress,
                                TotalPrice = p.TotalPrice
                            })
                            .ToListAsync();

                        return orders;
                    }

                    
                }
                catch (Exception)
                {
                    return new List<Models.Order>();
                }
            }
        }

        public async Task<List<Models.OrderItem>> GetListOrderItemAsync(int orderId)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _databaseContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
                try
                {
                    var orderItems = await _databaseContext.OrderItems
                        .Where(p => p.OrderId == orderId)
                        .Select(p => new Models.OrderItem
                        {
                            Id = p.Id,
                            OrderId = p.OrderId,
                            ProductId = p.ProductId,
                            Quantity = p.Quantity,
                        })
                        .ToListAsync();

                    return orderItems;
                }
                catch (Exception)
                {
                    return new List<Models.OrderItem>();
                }
            }
        }


        public async Task<string> GetProductDescriptionAsync(int productId)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _databaseContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
                try
                {
                    var description = await _databaseContext.Products
                        .Where(p => p.Id == productId)
                        .Select(p => p.Description)
                        .FirstOrDefaultAsync();

                    return description;
                }
                catch (Exception)
                {
                    return "";
                }
            }
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
                            UserInfo    = new Models.User(0, "", "")
                        };
                    }

                    if (account.Password != password)
                    {
                        return new LoginResult
                        {
                            LoginStatus = LoginStatus.InvalidPassword,
                            UserInfo    = new Models.User(0, "", "")
                        };
                    }

                    return new LoginResult
                    {
                        LoginStatus = LoginStatus.Success,
                        UserInfo    = new Models.User(account.UserId, account.Token, account.Role)
                    };
                }
                catch (Exception)
                {
                    return new LoginResult
                    {
                        LoginStatus = LoginStatus.ConnectServerFailed,
                        UserInfo    = new Models.User(0 ,"", "")
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

                    if (email != string.Empty && !email.IsValidEmail())
                    {
                        return SignupStatus.InvalidEmail;
                    }

                    var newUser = new DBModels.User()
                    {
                        Name    = username,
                        Email   = email,
                        Address = null,
                        Phone   = null,
                        CreateTime = DateTime.Now
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

                    var createTime = await _databaseContext.Users
                        .Where(p => p.Id == userId)
                        .Select(p => p.CreateTime)
                        .FirstOrDefaultAsync();

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
                            TotalProducts   = 0,
                            CreateTime      = createTime
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
                        TotalProducts   = totalProducts,
                        CreateTime      = createTime
                    };
                }
                catch (Exception)
                {
                    return new UserDetail
                    {
                        Name = "",
                        Email = "",
                        Phone = "",
                        Address = "",
                        AvatarPath = null,
                        TotalMoneySpent = 0,
                        TotalBills = 0,
                        TotalProducts = 0,
                        CreateTime = DateTime.MinValue
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
                        .Select(p => new Models.Account(p.UserId, p.Username, p.Password, p.Role, p.UserId))
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
                    ListAccounts = new List<Models.Account>(),
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

                    if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                    {
                        return false;
                    }

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
                        CreateTime = DateTime.Now
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

        public async Task<bool> ChangeProductInfoAsync(int id, string newName, string newBrand, string newCategory, int newPrice, int newInventory, int newSold, string newImagePath, string newDescription)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _databaseContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();

                if (string.IsNullOrEmpty(newName) || string.IsNullOrEmpty(newBrand) || string.IsNullOrEmpty(newCategory) || newPrice < 0 || newInventory < 0)
                {
                    return false;
                }

                try
                {
                    var product = await _databaseContext.Products.FirstOrDefaultAsync(p => p.Id == id);

                    if (product == null)
                    {
                        return false;
                    }

                    product.Name    = newName;
                    product.Brand   = newBrand;
                    product.Category = newCategory;
                    product.Price   = newPrice;
                    product.Stock   = newInventory;
                    product.Sold    = newSold;
                    product.ImagePath = newImagePath;
                    product.Description = newDescription;

                    await _databaseContext.SaveChangesAsync();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public async Task<bool> CreateProductAsync(string name, string brand, string category, int price, int inventory, int sold, string imagePath)
        {
            try
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var _databaseContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();

                    if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(brand) || string.IsNullOrEmpty(category) || price < 0 || inventory < 0)
                    {
                        return false;
                    }

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

        public async Task<string> GetUserLevelAsync(int userId)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _databaseContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();

                try
                {
                    var totalMoneySpent = await _databaseContext.Orders
                        .Where(p => p.UserId == userId)
                        .SelectMany(p => p.OrderItems)
                        .SumAsync(p => p.Quantity * p.Product.Price);

                    if (totalMoneySpent >= 20000000)
                    {
                        return "ULTRA";
                    }
                    else if (totalMoneySpent >= 10000000)
                    {
                        return "VIP";
                    }
                    else
                    {
                        return "NORMAL";
                    }

                }
                catch (Exception)
                {
                    return "NORMAL";
                }
            }
        }

        public async Task<GetOrderResult> GetListAllOrdersAsync(int page, int orderPerPage)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _databaseContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();

                try
                {
                    var query = _databaseContext.Orders.AsQueryable();

                    var totalOrders = await query.CountAsync();
                    var numPages = (int)Math.Ceiling((double)totalOrders / orderPerPage);

                    var db = await query.Skip((page - 1) * orderPerPage)
                        .Take(orderPerPage)
                        .Select(p => new Models.Order
                        {
                            Id = p.Id,
                            UserId = p.UserId,
                            OrderStatus = p.OrderStatus,
                            OrderDate = p.OrderDate,
                            ShippingMethod = p.ShippingMethod,
                            PaymentMethod = p.PaymentMethod,
                            VoucherId = p.VoucherId,
                            ShippingAddress = p.ShippingAddress
                        })
                        .ToListAsync();

                    return new GetOrderResult
                    {
                        ListOrders = db,
                        TotalPages = numPages == 0 ? 1 : numPages,
                        TotalOrders = totalOrders
                    };

                }
                catch (Exception)
                {
                    return new GetOrderResult
                    {
                        ListOrders = new List<Models.Order>(),
                        TotalPages = 1,
                        TotalOrders = 0
                    };
                }
            }}
        
        #endregion

        #region Cart
        public async Task<List<CartThumbnail>> GetListCartProductAsync()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _databaseContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
                var userSession = _serviceProvider.GetService(typeof(UserSession)) as UserSession;
                try
                {
                    var query = _databaseContext.Carts.AsQueryable();
                    var cartItems = await query
                        .Where(c => c.UserId == userSession.GetId())
                        .Select(c => new CartThumbnail(
                            c.Id,
                            c.Product.ImagePath,       // Assuming 'Image' is the field in Product
                            c.Product.Name,        // Assuming 'Name' is the field in Product
                            c.Product.Price,       // Assuming 'Price' is the field in Product
                            c.Quantity,            // Quantity as Amount
                            c.Quantity * c.Product.Price // TotalPrice calculation
                        ))
                        .ToListAsync();

                    return cartItems;
                }
                catch (Exception)
                {
                    return new List<CartThumbnail>(); // Return empty list on failure
                }
            }
        }

        private DBModels.Cart ConvertToDbCart(Models.CartThumbnail modelCart)
        {
            return new DBModels.Cart
            {
                UserId = modelCart.UserId,
                ProductId = modelCart.ProductId,
                Quantity = modelCart.Amount // Sử dụng Amount từ CartThumbnail
            };
        }

        public async Task<bool> AddToCartAsync(int productId, int quantity)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _databaseContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
                var userSession = _serviceProvider.GetService(typeof(UserSession)) as UserSession;
                try
                {
                    // Kiểm tra xem sản phẩm đã có trong giỏ hàng chưa
                    var existingCartItem = await _databaseContext.Carts
                        .FirstOrDefaultAsync(c => c.UserId == userSession.GetId() && c.ProductId == productId);

                    if (existingCartItem != null)
                    {
                        // Nếu sản phẩm đã có trong giỏ hàng, cập nhật số lượng
                        existingCartItem.Quantity += quantity;
                        _databaseContext.Carts.Update(existingCartItem);
                    }
                    else
                    {
                        // Nếu sản phẩm chưa có trong giỏ hàng, thêm mới
                        var newCartItem = new Models.CartThumbnail
                        (
                            userSession.GetId(),
                            productId,
                            quantity
                        );
                        //await _databaseContext.Carts.AddAsync(newCartItem);
                        // Chuyển đổi sang DBModels.Cart
                        var dbCartItem = ConvertToDbCart(newCartItem);
                        await _databaseContext.Carts.AddAsync(dbCartItem);
                    }

                    // Lưu thay đổi vào cơ sở dữ liệu
                    await _databaseContext.SaveChangesAsync();
                    return true; // Thêm thành công
                }
                catch (Exception ex)
                {
                    // Xử lý lỗi (có thể ghi log hoặc thông báo cho người dùng)
                    return false; // Thêm không thành công
                }
            }
        }

        public async Task<bool> DeleteFromCartAsync(int cartId)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _databaseContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
                var userSession = _serviceProvider.GetService(typeof(UserSession)) as UserSession;
                try
                {
                    // Tìm kiếm sản phẩm trong giỏ hàng theo cartId và userId
                    var cartItem = await _databaseContext.Carts
                        .FirstOrDefaultAsync(c => c.Id == cartId && c.UserId == userSession.GetId());

                    if (cartItem != null)
                    {
                        // Xóa sản phẩm khỏi giỏ hàng
                        _databaseContext.Carts.Remove(cartItem);

                        // Lưu thay đổi vào cơ sở dữ liệu
                        await _databaseContext.SaveChangesAsync();
                        return true; // Xóa thành công
                    }
                    else
                    {
                        // Nếu không tìm thấy sản phẩm trong giỏ hàng
                        return false; // Xóa không thành công
                    }
                }
                catch (Exception ex)
                {
                    // Xử lý lỗi (có thể ghi log hoặc thông báo cho người dùng)
                    return false; // Xóa không thành công
                }
            }
        }

        public async Task<bool> DeleteFromCartByProductIDAsync(int productId)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _databaseContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
                var userSession = _serviceProvider.GetService(typeof(UserSession)) as UserSession;
                try
                {
                    // Tìm kiếm sản phẩm trong giỏ hàng theo cartId và userId
                    var cartItem = await _databaseContext.Carts
                        .FirstOrDefaultAsync(c => c.ProductId == productId && c.UserId == userSession.GetId());

                    if (cartItem != null)
                    {
                        // Xóa sản phẩm khỏi giỏ hàng
                        _databaseContext.Carts.Remove(cartItem);

                        // Lưu thay đổi vào cơ sở dữ liệu
                        await _databaseContext.SaveChangesAsync();
                        return true; // Xóa thành công
                    }
                    else
                    {
                        // Nếu không tìm thấy sản phẩm trong giỏ hàng
                        return false; // Xóa không thành công
                    }
                }
                catch (Exception ex)
                {
                    // Xử lý lỗi (có thể ghi log hoặc thông báo cho người dùng)
                    return false; // Xóa không thành công
                }
            }
        }

        public async Task<bool> UpdateCartAsync(int cartId, int quantity)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _databaseContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
                try
                {
                    // Tìm kiếm sản phẩm trong giỏ hàng theo cartId
                    var cartItem = await _databaseContext.Carts
                        .FirstOrDefaultAsync(c => c.Id == cartId);

                    if (cartItem != null)
                    {
                        // Cập nhật số lượng
                        cartItem.Quantity = quantity;

                        // Cập nhật trạng thái của cartItem
                        _databaseContext.Carts.Update(cartItem);

                        // Lưu thay đổi vào cơ sở dữ liệu
                        await _databaseContext.SaveChangesAsync();
                        return true; // Cập nhật thành công
                    }
                    else
                    {
                        // Nếu không tìm thấy sản phẩm trong giỏ hàng
                        return false; // Cập nhật không thành công
                    }
                }
                catch (Exception ex)
                {
                    // Xử lý lỗi (có thể ghi log hoặc thông báo cho người dùng)
                    return false; // Cập nhật không thành công
                }
            }
        }

        #endregion

        #region Rating - Review

        public async Task<List<ReviewThumbnail>> GetListReviewThumbnailByIDProductAsync(int idProduct)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _databaseContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
                var userSession = _serviceProvider.GetService(typeof(UserSession)) as UserSession;
                try
                {
                    // Tìm kiếm sản phẩm trong giỏ hàng theo cartId và userId
                    var reviews = await _databaseContext.ProductRatings
                        .Where(r => r.ProductId == idProduct && r.UserId == userSession.GetId())
                        .Select(r => new ReviewThumbnail(
                            r.ProductId,
                            r.UserId,
                            r.User.Name,
                            r.User.AvatarPath,
                            r.Rating,
                            r.RatingDate
                        ))
                        .ToListAsync();
                    return reviews;
                }
                catch (Exception ex)
                {
                    return new List<ReviewThumbnail>();
                }
            }

        }
        #endregion
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

        public async Task<List<Models.Voucher>> GetAllVouchersAsync()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _databaseContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
                try
                {
                    var db = await _databaseContext.Vouchers
                        .Select(p => new Models.Voucher(
                            p.Id,
                            p.Code,
                            p.DiscountAmount,
                            p.PercentageDiscount,
                            p.Description,
                            p.ValidFrom,
                            p.ValidTo,
                            p.IsActive
                            ))
                        .ToListAsync();
                    return db;
                }
                catch (Exception)
                {
                    return new List<Models.Voucher>();
                }
            }
        }

        public List<PaymentProductThumbnail> GetAllPaymentProducts()
        {
            var db = new List<PaymentProductThumbnail>
            {
                new PaymentProductThumbnail(1, null, "Tẩy trang loreal", 150000, 2),
                new PaymentProductThumbnail(2, null, "Tẩy trang loreal", 150000, 2),
                new PaymentProductThumbnail(3, null, "Tẩy trang Bioderma", 150000, 1)
            };

            return db;
        }

        #region Shipping Method
        public async Task<List<Models.ShippingMethod>> GetShippingMethodsAsync()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _databaseContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
                try
                {
                    var db = await _databaseContext.ShippingMethods
                        .Select(p => new Models.ShippingMethod(
                            p.Id,
                            p.MethodName,
                            p.ShippingCost))
                        .ToListAsync();
                    return db;
                }
                catch (Exception)
                {
                    return new List<Models.ShippingMethod>();
                }
            }
        }
        #endregion

        #region Payment Method
        public async Task<List<Models.PaymentMethod>> GetPaymentMethodsAsync()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _databaseContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
                try
                {
                    var db = await _databaseContext.PaymentMethods
                        .Select(p => new Models.PaymentMethod(
                            p.Id,
                            p.MethodName))
                        .ToListAsync();
                    return db;
                }
                catch (Exception)
                {
                    return new List<Models.PaymentMethod>();
                }
            }
        }
        #endregion


        #region Order

        private DBModels.Order ConvertToDbOrder(Models.Order modelOrder)
        {
            return new DBModels.Order
            {
                UserId = modelOrder.UserId,
                OrderStatus = modelOrder.OrderStatus,
                OrderDate = modelOrder.OrderDate,
                PaymentMethod = modelOrder.PaymentMethod,
                ShippingMethod = modelOrder.ShippingMethod,
                VoucherId = modelOrder.VoucherId,
            };
        }

        private DBModels.OrderItem ConvertToDbOrderItem(Models.OrderItem modelOrder)
        {
            return new DBModels.OrderItem
            {
                ProductId = modelOrder.ProductId,
                OrderId = modelOrder.OrderId,
                Quantity = modelOrder.Quantity
            };
        }
        
        public async Task<Models.Order> AddToOrderAsync(List<PaymentProductThumbnail> listCartProduct, int paymentMethod, int shippingMethod, int voucher)
        {
            // Create a new scope for dependency injection
            using (var scope = _serviceProvider.CreateScope())
            {
                // Retrieve the database context from the service provider
                var _databaseContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
                var userSession = _serviceProvider.GetService(typeof(UserSession)) as UserSession;

                // Create a new order instance
                var order = new Models.Order
                {
                    UserId = userSession.GetId(),
                    OrderStatus = 0,
                    OrderDate = DateTime.Now,
                    PaymentMethod = paymentMethod,
                    ShippingMethod = shippingMethod,
                    VoucherId = voucher
                };

                try
                {
                    // Add the order to the context
                    var dbOrder = ConvertToDbOrder(order);
                    await _databaseContext.Orders.AddAsync(dbOrder);

                    // Save changes to the database to generate the OrderId
                    await _databaseContext.SaveChangesAsync();

                    // Now that the order is saved, set the OrderId for each OrderItem
                    foreach (var product in listCartProduct)
                    {
                        var orderItem = new Models.OrderItem
                        {
                            OrderId = dbOrder.Id, // Use the generated OrderId
                            ProductId = product.Id,
                            Quantity = product.Amount
                        };

                        // Convert to DB model and add to the context
                        var dbOrderItem = ConvertToDbOrderItem(orderItem);                      
                        await _databaseContext.OrderItems.AddAsync(dbOrderItem);
                        await DeleteFromCartByProductIDAsync(product.Id);
                    }

                    // Save changes again to persist the OrderItems
                    await _databaseContext.SaveChangesAsync();

                    // Return the created order
                    return order;
                }
                catch (Exception ex)
                {
                    throw; // Rethrow the exception for further handling
                }
            }
        }



        #endregion


    }
}
