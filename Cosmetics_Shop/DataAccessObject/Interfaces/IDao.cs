using Cosmetics_Shop.Models.Enums;
using Cosmetics_Shop.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosmetics_Shop.Models.DataService
{
    public interface IDao
    { 
        void InsertProduct(Product product);

        Task<ProductQueryResult> GetListProductThumbnailAsync(
            string keyword = "",
            int pageIndex = 1,
            int productsPerPage = 10,
            bool nameAscending = false,
            string filterBrand = "",
            int minPrice = 0,
            int maxPrice=int.MaxValue);

        ProductDetail GetProductDetail(int idProduct);

        List<ProductThumbnail> GetListNewProductAsync();

        List<ProductThumbnail> GetListBestSellerAsync();

        List<CartThumbnail> GetListCartProduct();

        List<ReviewThumbnail> GetListReviewThumbnail();

        List<ReviewThumbnail> GetListReviewThumbnailByIDProduct(int idProduct);

        Task<LoginResult> CheckLoginAsync(string username, string password);


    }
}
