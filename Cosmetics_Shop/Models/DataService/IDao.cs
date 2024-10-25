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

        Tuple<List<ProductThumbnail>, int> GetListProductThumbnail(
            string keyword = "",
            int pageIndex = 1,
            int rowsPerPage = 10,
            bool nameAscending = false);

        List<ProductThumbnail> GetListNewProduct(
            string keyword = "",
            int pageIndex = 1,
            int rowsPerPage = 10,
            bool nameAscending = false);

        List<ProductThumbnail> GetListBestSeller(
            string keyword = "",
            int pageIndex = 1,
            int rowsPerPage = 10,
            bool nameAscending = false);
    }
}
