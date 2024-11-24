using Cosmetics_Shop.Models;
using Cosmetics_Shop.Models.DataService;
using Cosmetics_Shop.Models.Enums;
using Cosmetics_Shop.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosmetics_Shop.DataAccessObject
{
    public class SqlDao : IDao
    {
        public Task<LoginResult> CheckLoginAsync(string username, string password)
        {
            throw new NotImplementedException();
        }

        public List<ProductThumbnail> GetListBestSellerAsync()
        {
            throw new NotImplementedException();
        }

        public List<CartThumbnail> GetListCartProduct()
        {
            throw new NotImplementedException();
        }

        public List<ProductThumbnail> GetListNewProductAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ProductQueryResult> GetListProductThumbnailAsync(
            string keyword = "",
            int pageIndex = 1,
            int productsPerPage = 10,
            SortProduct sortProduct = SortProduct.DateAscending,
            string filterBrand = "",
            int minPrice = 0,
            int maxPrice =
            int.MaxValue)
        {
            throw new NotImplementedException();
        }

        public List<ProductThumbnail> GetListRecentlyViewAsync()
        {
            throw new NotImplementedException();
        }

        public List<ReviewThumbnail> GetListReviewThumbnail()
        {
            throw new NotImplementedException();
        }

        public List<ReviewThumbnail> GetListReviewThumbnailByIDProduct(int idProduct)
        {
            throw new NotImplementedException();
        }

        public ProductDetail GetProductDetail(int idProduct)
        {
            throw new NotImplementedException();
        }

        public List<string> GetSuggestions(string keyword)
        {
            throw new NotImplementedException();
        }

        public void InsertProduct(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
