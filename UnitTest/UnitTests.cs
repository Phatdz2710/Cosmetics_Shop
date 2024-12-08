using Cosmetics_Shop.DBModels;
using Cosmetics_Shop.Models;
using Cosmetics_Shop.Models.Enums;
using Cosmetics_Shop.Services;
using Cosmetics_Shop.Services.Interfaces;
using Cosmetics_Shop.ViewModels.AdminPageViewModels;
using Cosmetics_Shop.ViewModels;
using Cosmetics_Shop.ViewModels.PageViewModels;
using Cosmetics_Shop.ViewModels.UserControlViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting.AppContainer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.IO;
using Cosmetics_Shop.DataAccessObject;
using Cosmetics_Shop.DataAccessObject.Interfaces;

namespace UnitTest
{
    /// <summary>
    /// Unit tests for the DAO (Data Access Object) class.
    /// </summary>
    [TestClass]
    public class DAOTest
    {
        public static IServiceProvider ServiceProvider { get; private set; }
        public IDao dao = null;

        /// <summary>
        /// Initializes the test setup by configuring services and creating an instance of SqlDao.
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
        /// Configures the services required for the tests, including reading the connection string from appsettings.json.
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
        /// Tests the ChangePasswordAsync method.
        /// </summary>
        [TestMethod]
        public async Task ChangePassword()
        {
            // Kiểm tra đổi mật khẩu 
            var result = await dao.ChangePasswordAsync(1, "123", "1234");
            Assert.IsTrue(result);
            // Trả lại mật khẩu cũ
            await dao.ChangePasswordAsync(1, "1234", "123");
            Assert.IsTrue(result);

            // Kiểm tra mật khẩu cũ không đúng
            var result1 = await dao.ChangePasswordAsync(1, "12", "1234");
            Assert.IsFalse(result1);
        }

        /// <summary>
        /// Tests the CreateAccountAsync and DeleteAccount methods.
        /// </summary>
        [TestMethod]
        public async Task CreateAndDeleteAccount()
        {
            // Tạo tài khoản
            var result1 = await dao.CreateAccountAsync("test", "123", "User");
            var result2 = await dao.CreateAccountAsync("test1", "123", "Admin");
            var result3 = await dao.CreateAccountAsync("test1", "123", "User");
            Assert.IsTrue(result1);
            Assert.IsTrue(result2);
            Assert.IsFalse(result3);

            // Xóa tài khoản
            var login1 = await dao.CheckLoginAsync("test", "123");
            var login2 = await dao.CheckLoginAsync("test1", "123");
            int id1 = login1.UserInfo.GetId();
            int id2 = login2.UserInfo.GetId();

            var del1 = await dao.DeleteAccount(id1);
            var del2 = await dao.DeleteAccount(id2);
            var del3 = await dao.DeleteAccount(id2);

            Assert.IsTrue(del1);
            Assert.IsTrue(del2);
            Assert.IsFalse(del3);
        }

        /// <summary>
        /// Tests the CheckLoginAsync method.
        /// </summary>
        [TestMethod]
        public async Task LoginTest()
        {
            var result1 = await dao.CheckLoginAsync("ngocphat", "123");
            var result2 = await dao.CheckLoginAsync("admin", "123");
            var result3 = await dao.CheckLoginAsync("cnp", "123");
            var result4 = await dao.CheckLoginAsync("ngocphat", "1234");

            Assert.AreEqual(LoginStatus.Success, result1.LoginStatus);
            Assert.AreEqual(LoginStatus.Success, result2.LoginStatus);
            Assert.AreEqual(LoginStatus.InvalidUsername, result3.LoginStatus);
            Assert.AreEqual(LoginStatus.InvalidPassword, result4.LoginStatus);

            Assert.AreEqual("User", result1.UserInfo.GetRole());
            Assert.AreEqual("Admin", result2.UserInfo.GetRole());
        }

        /// <summary>
        /// Tests the GetListProductThumbnailAsync method with a keyword filter.
        /// </summary>
        [TestMethod]
        public async Task GetListProductTest()
        {
            var result1 = await dao.GetListProductThumbnailAsync(keyword: "Sun");
            var result2 = await dao.GetListProductThumbnailAsync(keyword: "ngocphat");

            Assert.IsTrue(result1.Products.All(p => p.Name.Contains("Sun")));
            Assert.AreEqual(0, result2.Products.Count);
        }

        /// <summary>
        /// Tests the GetListProductThumbnailAsync method with a price range filter.
        /// </summary>
        [TestMethod]
        public async Task GetListProductThumbnailAsync_FilterByPriceRange()
        {
            var result = await dao.GetListProductThumbnailAsync(minPrice: 100000, maxPrice: 500000);

            Assert.IsTrue(result.Products.All(p => p.Price >= 100000 && p.Price <= 500000));
        }

        /// <summary>
        /// Tests the GetListProductThumbnailAsync method with brand and category filters.
        /// </summary>
        [TestMethod]
        public async Task GetListProductThumbnailAsync_FilterByBrandAndCategory()
        {
            var result1 = await dao.GetListProductThumbnailAsync(filterBrand: "L'Oreal", filterCategory: "Sunscreen");
            var result2 = await dao.GetListProductThumbnailAsync(filterBrand: "L'Oreal");
            var result3 = await dao.GetListProductThumbnailAsync(filterCategory: "Sunscreen");

            Assert.IsTrue(result1.Products.All(p => p.Brand == "L'Oreal" && p.Category == "Sunscreen"));
            Assert.IsTrue(result2.Products.All(p => p.Brand == "L'Oreal"));
            Assert.IsTrue(result3.Products.All(p => p.Category == "Sunscreen"));
        }
    }
}
