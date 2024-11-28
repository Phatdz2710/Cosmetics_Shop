using Cosmetics_Shop.Enums;
using Cosmetics_Shop.Models.Enums;
using Cosmetics_Shop.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.System.UserProfile;

namespace Cosmetics_Shop.Models.DataService
{
    public interface IDao
    {
        void InsertProduct(ProductThumbnail product);

        #region Get Products
        Task<SearchResult> GetListProductThumbnailAsync(
            string keyword = "",
            int pageIndex = 1,
            int productsPerPage = 10,
            SortProduct sortProduct = SortProduct.DateAscending,
            string filterBrand = "",
            string filterCategory = "",
            int minPrice = 0,
            int maxPrice = int.MaxValue);

        ProductDetail GetProductDetail(int idProduct);

        Task<List<ProductThumbnail>> GetListNewProductAsync();

        Task<List<ProductThumbnail>> GetListBestSellerAsync();

        Task<List<ProductThumbnail>> GetListRecentlyViewAsync();

        #endregion

        #region Login, Signup, User

        Task<LoginResult> CheckLoginAsync(string username, string password);

        Task<SignupStatus> DoSignupAsync(string username, string password, string confirmPassword, string email);

        #endregion

        #region Get User Information

        // Get all user information
        Task<UserDetail> GetUserDetailAsync(int userId);

        // Get only one field of user information
        Task<object> GetInformationAsync(int userId, UserInformationType infoType);

        public Task<bool> ChangeUserInformationAsync(int userId, UserInformationType infoType, string newValue);
        
        public Task<bool> ChangeAllUserInformationAsync(int userId, UserDetail userDetail);

        public Task<bool> ChangePasswordAsync(int userId, string oldPassword, string newPassword);
        

        #endregion 


        Task<List<string>> GetSuggestionsAsync(string keyword);

        List<CartThumbnail> GetListCartProduct();

        List<ReviewThumbnail> GetListReviewThumbnail();

        List<ReviewThumbnail> GetListReviewThumbnailByIDProduct(int idProduct);


        List<Voucher> GetAllVouchers();

        //Voucher GetVoucherByCode(string code);
        //void AddVoucher(Voucher voucher);
        //void DeleteVoucher(int id);
        //void UpdateVoucher(Voucher updatedVoucher);

        List<PaymentProductThumbnail> GetAllPaymentProducts();
        List<ShippingMethod> GetShippingMethods();
    }
}
