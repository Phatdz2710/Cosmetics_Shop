using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosmetics_Shop.Models.DataService
{
    public class MockDao : IDao
    {
        public ProductQueryResult GetListProductThumbnail(string keyword = "", int pageIndex = 1, int productsPerPage = 10, bool nameAscending = false)
        {
            var db = new List<ProductThumbnail>()
            {
                new ProductThumbnail(1, "Moisturizing Cream", null, 250000, "Cetaphil"),
                new ProductThumbnail(2, "Sunscreen SPF 50", null, 300000, "La Roche-Posay"),
                new ProductThumbnail(3, "Vitamin C Serum", null, 400000, "Skinceuticals"),
                new ProductThumbnail(4, "Anti-Aging Night Cream", null, 500000, "Olay"),
                new ProductThumbnail(5, "Lip Balm", null, 100000, "Burt's Bees"),
                new ProductThumbnail(6, "Eye Cream", null, 350000, "Cetaphil"),
                new ProductThumbnail(7, "Makeup Remover", null, 150000, "La Roche-Posay"),
                new ProductThumbnail(8, "Facial Cleanser", null, 200000, "Skinceuticals"),
                new ProductThumbnail(9, "Hydrating Toner", null, 180000, "Olay"),
                new ProductThumbnail(10, "Exfoliating Scrub", null, 220000, "Burt's Bees"),
                new ProductThumbnail(11, "Sheet Mask", null, 50000, "Cetaphil"),
                new ProductThumbnail(12, "Clay Mask", null, 250000, "La Roche-Posay"),
                new ProductThumbnail(13, "BB Cream", null, 320000, "Skinceuticals"),
                new ProductThumbnail(14, "Foundation", null, 350000, "Olay"),
                new ProductThumbnail(15, "Blush", null, 150000, "Burt's Bees"),
                new ProductThumbnail(16, "Lipstick", null, 200000, "Cetaphil"),
                new ProductThumbnail(17, "Concealer", null, 250000, "La Roche-Posay"),
                new ProductThumbnail(18, "Mascara", null, 220000, "Skinceuticals"),
                new ProductThumbnail(19, "Eyeliner", null, 150000, "Olay"),
                new ProductThumbnail(20, "Brow Pencil", null, 120000, "Burt's Bees"),
                new ProductThumbnail(21, "Face Serum", null, 450000, "Cetaphil"),
                new ProductThumbnail(22, "Facial Oil", null, 500000, "La Roche-Posay"),
                new ProductThumbnail(23, "Body Lotion", null, 180000, "Skinceuticals"),
                new ProductThumbnail(24, "Hand Cream", null, 120000, "Olay"),
                new ProductThumbnail(25, "Perfume", null, 600000, "Burt's Bees"),
                new ProductThumbnail(26, "Body Wash", null, 150000, "Cetaphil"),
                new ProductThumbnail(27, "Hair Conditioner", null, 200000, "La Roche-Posay"),
                new ProductThumbnail(28, "Shampoo", null, 180000, "Skinceuticals"),
                new ProductThumbnail(29, "Deodorant", null, 100000, "Olay"),
                new ProductThumbnail(30, "Nail Polish", null, 90000, "Burt's Bees"),
            };


            // Filter by keyword
            if (!string.IsNullOrEmpty(keyword))
            {
                db = db.Where(p => p.Name.ToLower().Contains(keyword.ToLower())).ToList();
            }

            List<string> brands = db.Select(p => p.Brand).Distinct().ToList();
            var resBrands = new List<string>() { "Tất cả" };
            resBrands.AddRange(brands);

            int totalProduct = db.Count;

            // Paging
            db = db.Skip((pageIndex - 1) * productsPerPage).Take(productsPerPage).ToList();

            int numPages = totalProduct / productsPerPage + (totalProduct % productsPerPage != 0 ? 1 : 0);
            if (numPages == 0) numPages = 1;

            return new ProductQueryResult()
            {
                Products = db,
                TotalPages = numPages,
                Brands = resBrands
            };
        }

        public List<ProductThumbnail> GetListNewProduct()
        {
            var db = new List<ProductThumbnail>()
            {
                new ProductThumbnail(1, "Moisturizing Cream", null, 250000, "La Roche-Rosay"),
                new ProductThumbnail(2, "Sunscreen SPF 50", null, 300000, "La Roche-Rosay"),
                new ProductThumbnail(3, "Vitamin C Serum", null, 400000, "La Roche-Rosay"),
            };

            // Filter by keyword

            return db;
        }

        public List<ProductThumbnail> GetListBestSeller()
        {
            var db = new List<ProductThumbnail>()
            {
                new ProductThumbnail(1, "Moisturizing Cream", null, 250000, "La Roche-Rosay"),
                new ProductThumbnail(2, "Sunscreen SPF 50", null, 300000, "La Roche-Rosay"),
                new ProductThumbnail(3, "Vitamin C Serum", null, 400000, "La Roche-Rosay"),
            };

            return db;
        }
        public void InsertProduct(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
