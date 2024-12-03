using Cosmetics_Shop.Models.DataService;
using Cosmetics_Shop.Models.Enums;
using Cosmetics_Shop.Services;
using Cosmetics_Shop.Services.Interfaces;
using Cosmetics_Shop.ViewModels.PageViewModels;
using Microsoft.UI.Xaml.Controls;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting.AppContainer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnitTest
{
    [TestClass]
    public class LoginTest
    {
        private IDao dao = null;

        [TestInitialize]
        public void Setup()
        {
            dao = new SqlDao();
            Assert.IsNotNull(dao, "Failed to initialize dao.");
        }

        [TestMethod]
        public async Task AdminLogin()
        {
            var expectedRole = "Admin";
            var expectedResult = LoginStatus.Success;

            var result = await dao.CheckLoginAsync("admin", "123");
            Assert.IsNotNull(result, "Result is null.");
            Assert.IsNotNull(result.UserInfo, "UserInfo is null.");

            Assert.AreEqual(expectedRole, result.UserInfo.GetRole());
            Assert.AreEqual(expectedResult, result.LoginStatus);
        }

        [TestMethod]
        public async Task UserLogin()
        {
            var expectedRole = "User";
            var expectedResult = LoginStatus.Success;

            var result = await dao.CheckLoginAsync("ngocphat", "123");
            Assert.IsNotNull(result, "Result is null.");
            Assert.IsNotNull(result.UserInfo, "UserInfo is null.");

            Assert.AreEqual(expectedRole, result.UserInfo.GetRole());
            Assert.AreEqual(expectedResult, result.LoginStatus);
        }

        [TestMethod]
        public async Task CreateAnAccount()
        {
            var expectedLoginStatus = LoginStatus.Success;

            var result = await dao.CheckLoginAsync("cnp", "123");
            Assert.IsNotNull(result, "Result is null.");

            Assert.AreEqual(expectedLoginStatus, result.LoginStatus);
        }

        [TestMethod]
        public async Task GetListProductTest()
        {
            var result = await dao.GetListProductThumbnailAsync(keyword: "Sun");
            Assert.IsNotNull(result, "Result is null.");
            Assert.IsNotNull(result.Products, "Products list is null.");

            Assert.IsTrue(result.Products.All(p => p.Name.Contains("Sun")));
        }

        [TestMethod]
        public async Task GetListProductThumbnailAsync_ShouldFilterByPriceRange()
        {
            var result = await dao.GetListProductThumbnailAsync(minPrice: 100000, maxPrice: 500000);
            Assert.IsNotNull(result, "Result is null.");
            Assert.IsNotNull(result.Products, "Products list is null.");

            Assert.IsTrue(result.Products.All(p => p.Price >= 100000 && p.Price <= 500000));
        }
    }
}
