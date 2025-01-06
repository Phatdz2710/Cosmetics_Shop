using Cosmetics_Shop.DataAccessObject.Data;
using Cosmetics_Shop.DBModels;
using Cosmetics_Shop.Enums;
using Cosmetics_Shop.Models;
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


namespace Cosmetics_Shop.DataAccessObject.Interfaces
{
    /// <summary>
    /// Interface for the Data Access Object (DAO) class.
    /// </summary>
    public interface IDao
    {

        #region Get Products

        /// <summary>
        /// Loads a list of products from the database based on the provided filters and options.
        /// </summary>
        /// <param name="keyword">The search keyword for product names or descriptions.</param>
        /// <param name="pageIndex">The current page index (starting from 1).</param>
        /// <param name="productsPerPage">The number of products displayed per page.</param>
        /// <param name="sortProduct">The sorting criteria for the products.</param>
        /// <param name="filterBrand">The brand to filter the products by.</param>
        /// <param name="filterCategory">The category to filter the products by.</param>
        /// <param name="minPrice">The minimum price for the products.</param>
        /// <param name="maxPrice">The maximum price for the products.</param>
        /// <returns>
        /// A <see cref="Task{SearchResult}"/> representing the asynchronous operation, 
        /// which returns a <see cref="SearchResult"/> containing the filtered and sorted product list.
        /// </returns>
        Task<SearchResult> GetListProductThumbnailAsync(
            string keyword = "",
            int pageIndex = 1,
            int productsPerPage = 10,
            SortProduct sortProduct = SortProduct.DateAscending,
            string filterBrand = "",
            string filterCategory = "",
            int minPrice = 0,
            int maxPrice = int.MaxValue);

        /// <summary>
        /// Asynchronously retrieves a list of new products.
        /// </summary>
        /// <returns>
        /// A <see cref="ProductThumbnail"/> representing the new products.
        /// </returns>
        Task<List<ProductThumbnail>> GetListNewProductAsync();




        /// <summary>
        /// Asynchronously retrieves a list of the best-selling products.
        /// </summary>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation, 
        /// which returns a list of <see cref="ProductThumbnail"/> for the best-selling products.
        /// </returns>
        Task<List<ProductThumbnail>> GetListBestSellerAsync();




        /// <summary>
        /// Asynchronously retrieves a list of recently viewed products for the current user.
        /// </summary>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation, 
        /// which returns a list of <see cref="ProductThumbnail"/> for recently viewed products.
        /// </returns>
        Task<List<ProductThumbnail>> GetListRecentlyViewAsync();




        /// <summary>
        /// Asynchronously retrieves a detailed information of all product.
        /// </summary>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation, 
        /// which returns a list of <see cref="ProductDetail"/> for all product details.
        /// </returns>
        Task<List<ProductDetail>> GetProductDetailsAsync();

        /// <summary>
        /// Asynchronously retrieves a detailed information of a specific product.
        /// </summary>
        /// <param name="idProduct"></param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation
        /// which returns a <see cref="ProductDetail"/> for product details.
        /// </returns>
        Task<ProductDetail> GetProductDetailAsync(int idProduct);


        /// <summary>
        /// Asynchronously retrieves the description of a specific product.
        /// </summary>
        /// <param name="productId"></param>
        /// A <see cref="Task"/> representing the asynchronous operation
        /// which returns a <see cref="string"/> for product's description.
        /// <returns></returns>
        Task<string> GetProductDescriptionAsync(int productId);

        #endregion

        #region Login, Signup, User

        /// <summary>
        /// Asynchronously verifies the login credentials for a user.
        /// </summary>
        /// <param name="username">The username entered by the user.</param>
        /// <param name="password">The password entered by the user.</param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation, 
        /// which returns a <see cref="LoginResult"/> indicating the login status and user details if successful.
        /// </returns>
        Task<LoginResult> CheckLoginAsync(string username, string password);

        /// <summary>
        /// Asynchronously registers a new user account.
        /// </summary>
        /// <param name="username">The desired username for the new account.</param>
        /// <param name="password">The password for the new account.</param>
        /// <param name="confirmPassword">The confirmation of the password.</param>
        /// <param name="email">The email address for the new account.</param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation, 
        /// which returns a <see cref="SignupStatus"/> indicating whether the signup was successful or if there were any errors.
        /// </returns>
        Task<SignupStatus> DoSignupAsync(string username, string password, string confirmPassword, string email);

        #endregion

        #region Get User Information

        /// <summary>
        /// Asynchronously retrieves the user all information for a specific user.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation, 
        /// which returns a <see cref="UserDetail"/> for user's detail.
        /// </returns>
        Task<UserDetail> GetUserDetailAsync(int userId);

        /// <summary>
        /// Asynchronously retrieves the user information for a specific user.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="infoType"></param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation, 
        /// which returns a <see cref="object"/> for user's information.
        /// </returns>
        Task<object> GetInformationAsync(int userId, UserInformationType infoType);


        /// <summary>
        /// Asynchronously retrieves the user information for a specific user.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="infoType"></param>
        /// <param name="newValue"></param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation,
        /// which returns a <see cref="bool"/> for result of change information.
        /// </returns>
        public Task<bool> ChangeUserInformationAsync(int userId, UserInformationType infoType, string newValue);
        

        /// <summary>
        /// Asynchronously retrieves the user information for a specific user.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="userDetail"></param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation,
        /// which returns a <see cref="bool"/> for result of change all information.
        /// </returns>
        public Task<bool> ChangeAllUserInformationAsync(int userId, UserDetail userDetail);


        /// <summary>
        /// Asynchronously retrieves the user information for a specific user.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation,
        /// which returns a <see cref="bool"/> for result of change password.
        /// </returns>
        public Task<bool> ChangePasswordAsync(int userId, string oldPassword, string newPassword);


        #endregion

        #region For Cart And Order

        /// <summary>
        /// Add a product to the cart.
        /// </summary>
        /// <param name="productId">The ID of product will be added to cart.</param>
        /// <param name="quantity">The quantity of product will be added to cart.</param>
        /// <returns>
        /// A <see cref="List{T}"/> of <see cref="bool"/> objects representing the products in the cart.
        /// </returns>
        Task<bool> AddToCartAsync(int productId, int quantity);

        /// <summary>
        /// Create an order
        /// </summary>
        /// <param name="listCartProduct">The list of product in the order.</param>
        /// <param name="paymentMethod">The method of payment in the order.</param>
        /// <param name="shippingMethod">The method of shipping in the order.</param>
        /// <param name="voucher">The voucher was applied.</param>
        /// <param name="address">The address of the order.</param>
        /// <param name="totalprice">The total price of the order.</param>
        /// <returns>
        /// A <see cref="List{T}"/> of <see cref="PaymentProductThumbnail"/> objects representing the products in the cart.
        /// </returns>
        Task<Models.Order> AddToOrderAsync(List<PaymentProductThumbnail> listCartProduct, 
                                            int paymentMethod, 
                                            int shippingMethod, 
                                            int voucher, 
                                            string address, 
                                            int totalprice);

        /// <summary>
        /// Delete a product to the cart by Card ID.
        /// </summary>
        /// <param name="cartId">The ID of cart product.</param>
        /// <returns>
        /// A <see cref="List{T}"/> of <see cref="bool"/> objects representing the products in the cart.
        /// </returns>
        Task<bool> DeleteFromCartAsync(int cartId);

        /// <summary>
        /// Delete a product to the cart by Product ID.
        /// </summary>
        /// <param name="productId">The ID of product in cart.</param>
        /// <returns>
        /// A <see cref="List{T}"/> of <see cref="bool"/> objects representing the products in the cart.
        /// </returns>
        Task<bool> DeleteFromCartByProductIDAsync(int productId);

        /// <summary>
        /// Update cart.
        /// </summary>
        /// <param name="cartId">The ID of cart product.</param>
        /// <param name="quantity">The quantity of product in cart.</param>
        /// <returns>
        /// A <see cref="List{T}"/> of <see cref="bool"/> objects representing the products in the cart.
        /// </returns>
        Task<bool> UpdateCartAsync(int cartId, int quantity);

        /// <summary>
        /// Get the information of a product in the cart.
        /// </summary>
        /// <param name="productid"></param>
        /// <returns></returns>
        Task<OrderItemDisplay> GetOrderItemDisplayAsync(int productid);

        /// <summary>
        /// Get the status of products in the cart.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        Task<List<Models.Order>> GetListOrderAsync(int userId, OrderStatus type);

        /// <summary>
        /// Get the informaton list of products in the cart.
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        Task<List<Models.OrderItemDisplay>> GetListOrderItemAsync(int orderId);

        /// <summary>
        /// Retrieves a list of products in the cart.
        /// </summary>
        /// <returns>
        /// A <see cref="List{T}"/> of <see cref="CartThumbnail"/> objects representing the products in the cart.
        /// </returns>
        Task<List<CartThumbnail>> GetListCartProductAsync();


        #endregion

        #region For Admin

        /// <summary>
        /// Asynchronously retrieves a list of all accounts.
        /// </summary>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation, 
        /// which returns an <see cref="AccountSearchResult"/> containing account details.
        /// </returns>
        Task<AccountSearchResult> GetListAccountAsync();

        /// <summary>
        /// Asynchronously updates account information.
        /// </summary>
        /// <param name="id">The ID of the account to update.</param>
        /// <param name="newUsername">The new username to set.</param>
        /// <param name="newPassword">The new password to set.</param>
        /// <param name="newRole">The new role to assign to the account.</param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation, 
        /// which returns a boolean indicating whether the update was successful.
        /// </returns>
        Task<bool> ChangeAccountInfoAsync(int id, string newUsername, string newPassword, string newRole);

        /// <summary>
        /// Asynchronously deletes an account by its ID.
        /// </summary>
        /// <param name="id">The ID of the account to delete.</param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation, 
        /// which returns a boolean indicating whether the deletion was successful.
        /// </returns>
        Task<bool> DeleteAccount(int id);

        /// <summary>
        /// Asynchronously creates a new account.
        /// </summary>
        /// <param name="username">The username for the new account.</param>
        /// <param name="password">The password for the new account.</param>
        /// <param name="role">The role to assign to the new account.</param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation, 
        /// which returns a boolean indicating whether the account creation was successful.
        /// </returns>
        Task<bool> CreateAccountAsync(string username, string password, string role);

        /// <summary>
        /// Asynchronously updates product information.
        /// </summary>
        /// <param name="id">The ID of the product to update.</param>
        /// <param name="newName">The new name of the product.</param>
        /// <param name="newBrand">The new brand of the product.</param>
        /// <param name="newCategory">The new category of the product.</param>
        /// <param name="newPrice">The new price of the product.</param>
        /// <param name="newInventory">The new inventory quantity of the product.</param>
        /// <param name="newSold">The new sold quantity of the product.</param>
        /// <param name="newImagePath">The new image path of the product.</param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation, 
        /// which returns a boolean indicating whether the update was successful.
        /// </returns>
        Task<bool> ChangeProductInfoAsync(int id, string newName, string newBrand, string newCategory, int newPrice, int newInventory, string newImagePath, string newDescription);

        /// <summary>
        /// Asynchronously creates a new product.
        /// </summary>
        /// <param name="name">The name of the new product.</param>
        /// <param name="brand">The brand of the new product.</param>
        /// <param name="category">The category of the new product.</param>
        /// <param name="price">The price of the new product.</param>
        /// <param name="sold">The initial sold quantity of the new product.</param>
        /// <param name="inventory">The initial inventory quantity of the new product.</param>
        /// <param name="imagePath">The image path of the new product.</param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation, 
        /// which returns a boolean indicating whether the product creation was successful.
        /// </returns>
        Task<bool> CreateProductAsync(string name, string brand, string category, int price, int sold, int inventory, string imagePath, string description);


        /// <summary>
        /// Asynchronously retrieves the user's level of a specific user.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation,
        /// which returns a <see cref="string"/> for user's level.
        /// </returns>
        Task<string> GetUserLevelAsync(int userId);

        /// <summary>
        /// Asynchronously retrieves the user's level of a specific user.
        /// </summary>
        /// <param name="page"></param>
        /// <param name="orderPerPage"></param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation,
        /// </returns>
        Task<GetOrderResult> GetListAllOrdersAsync(int page, int orderPerPage);

        /// <summary>
        /// Asynchronously retrieves the user's level of a specific user.
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="status"></param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation,
        /// </returns>
        Task<bool>ChangeOrderStatusAsync(int orderId, int status);

        #endregion

        #region Others
        /// <summary>
        /// Asynchronously retrieves a list of product suggestions based on a keyword.
        /// </summary>
        /// <param name="keyword">The keyword to search for product suggestions.</param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation,
        /// which returns a list of strings containing product suggestions.
        /// </returns>
        Task<List<string>> GetSuggestionsAsync(string keyword);
        #endregion

        #region Rating, Shipping and Payment

        /// <summary>
        /// Retrieves a list of review thumbnails for a specific product.
        /// </summary>
        /// <param name="idProduct">The ID of the product to retrieve reviews for.</param>
        /// <returns>
        /// A <see cref="List{T}"/> of <see cref="ReviewThumbnail"/> objects associated with the specified product.
        /// </returns>
        Task<List<ReviewThumbnail>> GetListReviewThumbnailByIDProductAsync(int idProduct);

        /// <summary>
        /// Add a new rating for a specific product.
        /// </summary>
        /// <param name="idProduct">The ID of the product adding to the database.</param>
        /// <param name="idProduct">The star number of the product adding to the database.</param>
        /// <returns>
        /// A <see cref="List{T}"/> of <see cref="bool"/> objects associated with the specified product.
        /// </returns>
        Task<bool> AddReviewAsync(int idProduct, int starNumber);

        /// <summary>
        /// Recalculate the average rating of a product
        /// </summary>
        /// <param name="productID">The ID of the product.</param>
        /// <param name="starNumber">The star number of the product will be calculated.</param>
        /// <returns>
        /// A <see cref="List{T}"/> of <see cref="bool"/> objects associated with the specified product.
        /// </returns>
        Task<bool> RecalculateRatingAverage(int productID, int starNumber);

        /// <summary>
        /// Retrieves a list of all available vouchers.
        /// </summary>
        /// <returns>
        /// A <see cref="List{T}"/> of <see cref="Voucher"/> objects representing the available vouchers.
        /// </returns>
        Task<List<Models.Voucher>> GetAllVouchersAsync();

        /// <summary>
        /// Retrieves a list of available shipping methods.
        /// </summary>
        /// <returns>
        /// A <see cref="List{T}"/> of <see cref="ShippingMethod"/> objects representing the available shipping methods.
        /// </returns>
        Task<List<Models.ShippingMethod>> GetShippingMethodsAsync();

        /// <summary>
        /// Retrieves a list of available payment methods.
        /// </summary>
        /// <returns>
        /// A <see cref="List{T}"/> of <see cref="PaymentMethod"/> objects representing the available shipping methods.
        /// </returns>
        Task<List<Models.PaymentMethod>> GetPaymentMethodsAsync();

        #endregion
    }
}
