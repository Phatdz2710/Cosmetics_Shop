using Cosmetics_Shop.DataAccessObject.Interfaces;
using Cosmetics_Shop.DataAccessObject;
using Cosmetics_Shop.DBModels;
using Cosmetics_Shop.Models.Enums;
using Cosmetics_Shop.Models;
using Cosmetics_Shop.Services.Interfaces;
using Cosmetics_Shop.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Cosmetics_Shop.Enums;

/// <summary>
/// Unit tests for the Data Access Object (DAO) in the Cosmetics Shop application.
/// </summary>
namespace UnitTest
{
    [TestClass]
    public class DAOTest
    {
        public static IServiceProvider ServiceProvider { get; private set; }
        public IDao dao = null;

        /// <summary>
        /// Initializes the test setup by configuring services and creating an instance of the DAO.
        /// </summary>
        [TestInitialize]
        public void Setup()
        {
            var services = new ServiceCollection();
            ConfigureServices(services);
            var serviceProvider = services.BuildServiceProvider();

            dao = new SqlDao(serviceProvider);
        }

        /// <summary>
        /// Configures the services required for the tests.
        /// </summary>
        /// <param name="services">The service collection to configure.</param>
        private void ConfigureServices(ServiceCollection services)
        {
            services.AddSingleton<INavigationService, NavigationService>();
            services.AddSingleton<IEventAggregator, EventAggregator>();
            services.AddSingleton<IDao, SqlDao>();
            services.AddSingleton<IFilePickerService, FilePickerService>();
            services.AddSingleton<IServiceProvider, ServiceProvider>();
            services.AddSingleton<UserSession>();

            var basePath = AppContext.BaseDirectory;
            var jsonFilePath = System.IO.Path.Combine(basePath, "appsettings.json");
            var jsonContent = File.ReadAllText(jsonFilePath);
            var rootNode = JsonNode.Parse(jsonContent);
            var connectionString = rootNode["ConnectionStrings"]["DefaultConnection"].ToString();

            services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(connectionString));

            ServiceProvider = services.BuildServiceProvider();
        }

        /// <summary>
        /// Tests changing the password when the old password is correct.
        /// </summary>
        [TestMethod]
        public async Task ChangePassword_WhenOldPasswordIsCorrect_ShouldReturnTrue()
        {
            var result = await dao.ChangePasswordAsync(1, "123", "1234");
            Assert.IsTrue(result);
            await dao.ChangePasswordAsync(1, "1234", "123");
            Assert.IsTrue(result);
        }

        /// <summary>
        /// Tests changing the password when the old password is incorrect.
        /// </summary>
        [TestMethod]
        public async Task ChangePassword_WhenOldPasswordIsIncorrect_ShouldReturnFalse()
        {
            var result = await dao.ChangePasswordAsync(1, "1234", "12345");
            Assert.IsFalse(result);
        }

        /// <summary>
        /// Tests creating a new account when all inputs are valid.
        /// </summary>
        [TestMethod]
        public async Task CreateNewAccount_WhenEveryThingIsValid_ShouldReturnTrue()
        {
            var result1 = await dao.CreateAccountAsync("test", "123", "User");
            var result2 = await dao.CreateAccountAsync("test1", "123", "Admin");
            Assert.IsTrue(result1);
            Assert.IsTrue(result2);
        }

        /// <summary>
        /// Tests creating a new account when the username already exists.
        /// </summary>
        [TestMethod]
        public async Task CreateNewAccount_WhenUsernameIsAlrearyExist_ShouldReturnFalse()
        {
            var result1 = await dao.CreateAccountAsync("ngocphat", "123", "User");
            var result2 = await dao.CreateAccountAsync("admin", "123", "Admin");
            Assert.IsFalse(result1);
            Assert.IsFalse(result2);
        }

        /// <summary>
        /// Tests logging in when all inputs are valid.
        /// </summary>
        [TestMethod]
        public async Task Login_WhenEverythingIsValid_ShouldReturnTrue()
        {
            var result1 = await dao.CheckLoginAsync("ngocphat", "123");
            var result2 = await dao.CheckLoginAsync("admin", "123");

            Assert.AreEqual(result1.LoginStatus, LoginStatus.Success);
            Assert.AreEqual(result2.LoginStatus, LoginStatus.Success);
        }

        /// <summary>
        /// Tests logging in when the password is incorrect.
        /// </summary>
        [TestMethod]
        public async Task Login_WhenPasswordIsIncorrect_ShouldReturnFalse()
        {
            var result1 = await dao.CheckLoginAsync("ngocphat", "1234");
            var result2 = await dao.CheckLoginAsync("admin", "1235");

            Assert.AreEqual(result1.LoginStatus, LoginStatus.InvalidPassword);
            Assert.AreEqual(result2.LoginStatus, LoginStatus.InvalidPassword);
        }

        /// <summary>
        /// Tests logging in when the username does not exist.
        /// </summary>
        [TestMethod]
        public async Task Login_WhenUsernameIsNotExists_ShouldReturnFalse()
        {
            var result1 = await dao.CheckLoginAsync("test122", "1234");
            var result2 = await dao.CheckLoginAsync("test1123", "1235");

            Assert.AreEqual(result1.LoginStatus, LoginStatus.InvalidUsername);
            Assert.AreEqual(result2.LoginStatus, LoginStatus.InvalidUsername);
        }

        /// <summary>
        /// Tests deleting an account when all inputs are valid.
        /// </summary>
        [TestMethod]
        public async Task DeleteAccount_WhenEveryThingIsValid_ShouldReturnTrue()
        {
            var login1 = await dao.CheckLoginAsync("test", "123");
            var login2 = await dao.CheckLoginAsync("test1", "123");

            int id1 = login1.UserInfo.GetId();
            int id2 = login2.UserInfo.GetId();

            var del1 = await dao.DeleteAccount(id1);
            var del2 = await dao.DeleteAccount(id2);

            Assert.IsTrue(del1);
            Assert.IsTrue(del2);
        }

        /// <summary>
        /// Tests getting a list of products when a keyword is provided.
        /// </summary>
        [TestMethod]
        public async Task GetListProduct_WhenKeywordIsNotNull_ShouldReturnTrue()
        {
            var result1 = await dao.GetListProductThumbnailAsync(keyword: "Paris");
            var result2 = await dao.GetListProductThumbnailAsync(keyword: "Hydro");

            Assert.IsTrue(result1.Products.All(p => p.Name.Contains("Paris")));
            Assert.IsTrue(result2.Products.All(p => p.Name.Contains("Hydro")));
        }

        /// <summary>
        /// Tests getting a list of products when filtering by brand or category.
        /// </summary>
        [TestMethod]
        public async Task GetListProduct_WhenFilterByBrandOrFilterCategory_ShouldReturnTrue()
        {
            var result1 = await dao.GetListProductThumbnailAsync(filterBrand: "Neutrogena", filterCategory: "Skincare");
            var result2 = await dao.GetListProductThumbnailAsync(filterBrand: "Maybelline", filterCategory: "Makeup");
            var result3 = await dao.GetListProductThumbnailAsync(filterBrand: "L'Oreal", filterCategory: "Skincare");

            Assert.IsTrue(result1.Products.All(p => p.Brand == "Neutrogena" && p.Category == "Skincare"));
            Assert.IsTrue(result2.Products.All(p => p.Brand == "Maybelline" && p.Category == "Makeup"));
            Assert.IsTrue(result3.Products.All(p => p.Brand == "L'Oreal" && p.Category == "Skincare"));
        }

        /// <summary>
        /// Tests getting a list of products when filtering by price.
        /// </summary>
        [TestMethod]
        public async Task GetListProduct_WhenFilterByPrice_ShouldReturnTrue()
        {
            var result = await dao.GetListProductThumbnailAsync(minPrice: 100000, maxPrice: 500000);
            var result1 = await dao.GetListProductThumbnailAsync(minPrice: 100000);

            Assert.IsTrue(result.Products.All(p => p.Price >= 100000 && p.Price <= 500000));
            Assert.IsTrue(result1.Products.All(p => p.Price >= 100000));
        }

        /// <summary>
        /// Tests getting a list of orders when filtering by order status (In Process).
        /// </summary>
        public async Task GetListOrder_WhenOrderStatusIsInProcess_ShouldReturnTrue()
        {
            var result1 = await dao.GetListOrderAsync(2, OrderStatus.InProcess);
            var result2 = await dao.GetListOrderAsync(3, OrderStatus.InProcess);

            Assert.IsTrue(result1.All(o => o.OrderStatus == 1 || o.OrderStatus == 0));
            Assert.IsTrue(result2.All(o => o.OrderStatus == 1 || o.OrderStatus == 0));
        }


        /// <summary>
        /// Tests getting a list of orders when filtering by order status (Success).
        /// </summary>
        public async Task GetListOrder_WhenOrderStatusIsSuccessful_ShouldReturnTrue()
        {
            var result1 = await dao.GetListOrderAsync(2, OrderStatus.Success);
            var result2 = await dao.GetListOrderAsync(4, OrderStatus.Success);

            Assert.IsTrue(result1.All(o => o.OrderStatus == 2));
            Assert.IsTrue(result2.All(o => o.OrderStatus == 2));
        }


    }
}
