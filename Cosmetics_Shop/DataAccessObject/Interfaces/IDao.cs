using Cosmetics_Shop.Models.Enums;
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

        ProductQueryResult GetListProductThumbnail(
            string keyword = "",
            int pageIndex = 1,
            int productsPerPage = 10,
            bool nameAscending = false,
            string filterBrand = "",
            int minPrice = 0,
            int maxPrice=int.MaxValue);

        ProductDetail GetProductDetail(int idProduct);

        List<ProductThumbnail> GetListNewProduct();

        List<ProductThumbnail> GetListBestSeller();

        LoginResult CheckLogin(string username, string password);
    }
}
