﻿using Cosmetics_Shop.DataAccessObject.Data;
using Cosmetics_Shop.DataAccessObject.Interfaces;
using Cosmetics_Shop.Enums;
using Cosmetics_Shop.Models;
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

namespace Cosmetics_Shop.DataAccessObject
{
    /// <summary>
    /// Mock data access object
    /// </summary>
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
            //var db = new List<ProductThumbnail>()
            //{
            //    new ProductThumbnail(1, "Moisturizing Cream", null, 250000, "Cetaphil"),
            //    new ProductThumbnail(2, "Sunscreen SPF 50", null, 300000, "La Roche-Posay"),
            //    new ProductThumbnail(3, "Vitamin C Serum", null, 400000, "Skinceuticals"),
            //    new ProductThumbnail(4, "Anti-Aging Night Cream", null, 500000, "Olay"),
            //    new ProductThumbnail(5, "Lip Balm", null, 100000, "Burt's Bees"),
            //    new ProductThumbnail(6, "Eye Cream", null, 350000, "Cetaphil"),
            //    new ProductThumbnail(7, "Makeup Remover", null, 150000, "La Roche-Posay"),
            //    new ProductThumbnail(8, "Facial Cleanser", null, 200000, "Skinceuticals"),
            //    new ProductThumbnail(9, "Hydrating Toner", null, 180000, "Olay"),
            //    new ProductThumbnail(10, "Exfoliating Scrub", null, 220000, "Burt's Bees"),
            //    new ProductThumbnail(11, "Sheet Mask", null, 50000, "Cetaphil"),
            //    new ProductThumbnail(12, "Clay Mask", null, 250000, "La Roche-Posay"),
            //    new ProductThumbnail(13, "BB Cream", null, 320000, "Skinceuticals"),
            //    new ProductThumbnail(14, "Foundation", null, 350000, "Olay"),
            //    new ProductThumbnail(15, "Blush", null, 150000, "Burt's Bees"),
            //    new ProductThumbnail(16, "Lipstick", null, 200000, "Cetaphil"),
            //    new ProductThumbnail(17, "Concealer", null, 250000, "La Roche-Posay"),
            //    new ProductThumbnail(18, "Mascara", null, 220000, "Skinceuticals"),
            //    new ProductThumbnail(19, "Eyeliner", null, 150000, "Olay"),
            //    new ProductThumbnail(20, "Brow Pencil", null, 120000, "Burt's Bees"),
            //    new ProductThumbnail(21, "Face Serum", null, 450000, "Cetaphil"),
            //    new ProductThumbnail(22, "Facial Oil", null, 500000, "La Roche-Posay"),
            //    new ProductThumbnail(23, "Body Lotion", null, 180000, "Skinceuticals"),
            //    new ProductThumbnail(24, "Hand Cream", null, 120000, "Olay"),
            //    new ProductThumbnail(25, "Perfume", null, 600000, "Burt's Bees"),
            //    new ProductThumbnail(26, "Body Wash", null, 150000, "Cetaphil"),
            //    new ProductThumbnail(27, "Hair Conditioner", null, 200000, "La Roche-Posay"),
            //    new ProductThumbnail(28, "Shampoo", null, 180000, "Skinceuticals"),
            //    new ProductThumbnail(29, "Deodorant", null, 100000, "Olay"),
            //    new ProductThumbnail(30, "Nail Polish", null, 90000, "Burt's Bees"),
            //    new ProductThumbnail(31, "Face Serum", null, 450000, "Cetaphil"),
            //    new ProductThumbnail(32, "Facial Oil", null, 500000, "La Roche-Posay"),
            //    new ProductThumbnail(33, "Body Lotion", null, 180000, "Skinceuticals"),
            //    new ProductThumbnail(34, "Hand Cream", null, 120000, "Olay"),
            //    new ProductThumbnail(35, "Perfume", null, 600000, "Burt's Bees"),
            //    new ProductThumbnail(36, "Body Wash", null, 150000, "Cetaphil"),
            //    new ProductThumbnail(37, "Hair Conditioner", null, 200000, "La Roche-Posay"),
            //    new ProductThumbnail(38, "Shampoo", null, 180000, "Skinceuticals"),
            //    new ProductThumbnail(39, "Deodorant", null, 100000, "Olay"),
            //    new ProductThumbnail(40, "Nail Polish", null, 90000, "Burt's Bees"),
            //};

            var db = new List<ProductThumbnail>();

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

        
        public async Task<List<ProductDetail>> GetProductDetailsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<ProductDetail> GetProductDetailAsync(int idProduct)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ProductThumbnail>> GetListNewProductAsync()
        {
            //var db = new List<ProductThumbnail>()
            //{
            //    new ProductThumbnail(1, "Moisturizing Cream", null, 250000, "La Roche-Posay"),
            //    new ProductThumbnail(2, "Sunscreen SPF 50", null, 300000, "La Roche-Posay"),
            //    new ProductThumbnail(3, "Vitamin C Serum", null, 400000, "La Roche-Posay"),
            //    new ProductThumbnail(1, "Moisturizing Cream", null, 250000, "La Roche-Posay"),
            //    new ProductThumbnail(2, "Sunscreen SPF 50", null, 300000, "La Roche-Posay"),
            //    new ProductThumbnail(3, "Vitamin C Serum", null, 400000, "La Roche-Posay"),
            //    new ProductThumbnail(1, "Moisturizing Cream", null, 250000, "La Roche-Posay"),
            //    new ProductThumbnail(2, "Sunscreen SPF 50", null, 300000, "La Roche-Posay"),
            //    new ProductThumbnail(3, "Vitamin C Serum", null, 400000, "La Roche-Posay"),
            //    new ProductThumbnail(1, "Moisturizing Cream", null, 250000, "La Roche-Posay"),
            //    new ProductThumbnail(2, "Sunscreen SPF 50", null, 300000, "La Roche-Posay"),
            //    new ProductThumbnail(3, "Vitamin C Serum", null, 400000, "La Roche-Posay"),
            //    new ProductThumbnail(1, "Moisturizing Cream", null, 250000, "La Roche-Posay"),
            //    new ProductThumbnail(2, "Sunscreen SPF 50", null, 300000, "La Roche-Posay"),
            //    new ProductThumbnail(3, "Vitamin C Serum", null, 400000, "La Roche-Posay"),
            //    new ProductThumbnail(1, "Moisturizing Cream", null, 250000, "La Roche-Posay"),
            //    new ProductThumbnail(2, "Sunscreen SPF 50", null, 300000, "La Roche-Posay"),
            //    new ProductThumbnail(3, "Vitamin C Serum", null, 400000, "La Roche-Posay"),
            //};

            var db = new List<ProductThumbnail>();

            return db;
        }
        public async Task<List<ProductThumbnail>> GetListBestSellerAsync()
        {
            var db = new List<ProductThumbnail>();

            return db;
        }

        public async Task<List<ProductThumbnail>> GetListRecentlyViewAsync()
        {
            var db = new List<ProductThumbnail>();

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
                new CartThumbnail(1, null, "Tẩy trang loreal", 150000, 2, 300000),
                new CartThumbnail(3, null, "Tẩy trang Bioderma", 150000, 1, 150000),
                new CartThumbnail(5, null, "Tẩy trang Ganier", 130000, 1, 130000),
                new CartThumbnail(6, null, "Kem chống nắng skinaqua", 125000, 1, 125000)
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
                new PaymentProductThumbnail(1, null, "Tẩy trang loreal", 150000, 2),
                new PaymentProductThumbnail(2, null, "Tẩy trang loreal", 150000, 2),
                new PaymentProductThumbnail(3, null, "Tẩy trang Bioderma", 150000, 1)
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

        public Task<AccountSearchResult> GetListAccountAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> ChangeAccountInfoAsync(int id, string newUsername, string newPassword, string newRole)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAccount(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CreateAccountAsync(string username, string password, string role)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ChangeProductInfoAsync(int id, string newName, string newBrand, string newCategory, int newPrice, int newInventory, int newSold, string newImagePath)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CreateProductAsync(string name, string brand, string category, int price, int inventory, int sold, string imagePath)
        {
            throw new NotImplementedException();
        }
    }
}
