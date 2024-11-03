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
            dao = new MockDao();
        }

        [TestMethod]
        public void AdminLogin()
        {

            var expectedRole = "Admin";
            var expectedToken = "1234";
            var expectedResult = LoginStatus.Success;

            var result = dao.CheckLoginAsync("admin", "admin");

            Assert.AreEqual(expectedRole, result.Result.Role);
            Assert.AreEqual(expectedToken, result.Result.Token);
            Assert.AreEqual(expectedResult, result.Result.LoginStatus);
        }

        [TestMethod]
        public void UserLogin()
        {

            var expectedRole = "User";
            var expectedToken = "2345";
            var expectedResult = LoginStatus.Success;

            var result = dao.CheckLoginAsync("user", "user");

            Assert.AreEqual(expectedRole, result.Result.Role);
            Assert.AreEqual(expectedToken, result.Result.Token);
            Assert.AreEqual(expectedResult, result.Result.LoginStatus);
        }

        [TestMethod]
        public void InvalidUsername()
        {

            var expectedRole = "";
            var expectedToken = "";
            var expectedResult = LoginStatus.InvalidUsername;

            var result = dao.CheckLoginAsync("abc", "user");

            Assert.AreEqual(expectedRole, result.Result.Role);
            Assert.AreEqual(expectedToken, result.Result.Token);
            Assert.AreEqual(expectedResult, result.Result.LoginStatus);
        }

        [TestMethod]
        public void InvalidPassword()
        {

            var expectedRole = "";
            var expectedToken = "";
            var expectedResult = LoginStatus.InvalidPassword;

            var result = dao.CheckLoginAsync("user", "dsfsd");

            Assert.AreEqual(expectedRole, result.Result.Role);
            Assert.AreEqual(expectedToken, result.Result.Token);
            Assert.AreEqual(expectedResult, result.Result.LoginStatus);
        }

        [TestMethod]
        public async Task GetListProductTest()
        {
            var result = await dao.GetListProductThumbnailAsync(keyword: "Cream");

            Assert.IsTrue(result.Products.All(p => p.Name.Contains("Cream")));
        }

        [TestMethod]
        public async Task GetListProductThumbnailAsync_ShouldFilterByPriceRange()
        {
            // Act
            var result = await dao.GetListProductThumbnailAsync(minPrice: 100000, maxPrice: 400000);

            // Assert
            Assert.IsTrue(result.Products.All(p => p.Price >= 100000 && p.Price <= 400000));
        }

    }
}
