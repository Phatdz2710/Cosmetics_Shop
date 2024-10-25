using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosmetics_Shop.Models.DataService
{
    public class MockDao : IDao
    {
        public Tuple<List<ProductThumbnail>, int> GetListProductThumbnail(string keyword = "", int pageIndex = 1, int rowsPerPage = 10, bool nameAscending = false)
        {
            var db = new List<ProductThumbnail>()
            {
                new ProductThumbnail(1, "Moisturizing Cream", null, 250000),
                new ProductThumbnail(2, "Sunscreen SPF 50", null, 300000),
                new ProductThumbnail(3, "Vitamin C Serum", null, 400000),
                new ProductThumbnail(4, "Anti-Aging Night Cream", null, 500000),
                new ProductThumbnail(5, "Lip Balm", null, 100000),
                new ProductThumbnail(6, "Eye Cream", null, 350000),
                new ProductThumbnail(7, "Makeup Remover", null, 150000),
                new ProductThumbnail(8, "Facial Cleanser", null, 200000),
                new ProductThumbnail(9, "Hydrating Toner", null, 180000),
                new ProductThumbnail(10, "Exfoliating Scrub", null, 220000),
                new ProductThumbnail(11, "Sheet Mask", null, 50000),
                new ProductThumbnail(12, "Clay Mask", null, 250000),
                new ProductThumbnail(13, "BB Cream", null, 320000),
                new ProductThumbnail(14, "Foundation", null, 350000),
                new ProductThumbnail(15, "Blush", null, 150000),
                new ProductThumbnail(16, "Lipstick", null, 200000),
                new ProductThumbnail(17, "Concealer", null, 250000),
                new ProductThumbnail(18, "Mascara", null, 220000),
                new ProductThumbnail(19, "Eyeliner", null, 150000),
                new ProductThumbnail(20, "Brow Pencil", null, 120000),
                new ProductThumbnail(21, "Face Serum", null, 450000),
                new ProductThumbnail(22, "Facial Oil", null, 500000),
                new ProductThumbnail(23, "Body Lotion", null, 180000),
                new ProductThumbnail(24, "Hand Cream", null, 120000),
                new ProductThumbnail(25, "Perfume", null, 600000),
                new ProductThumbnail(26, "Body Wash", null, 150000),
                new ProductThumbnail(27, "Hair Conditioner", null, 200000),
                new ProductThumbnail(28, "Shampoo", null, 180000),
                new ProductThumbnail(29, "Deodorant", null, 100000),
                new ProductThumbnail(30, "Nail Polish", null, 90000),
                new ProductThumbnail(25, "Perfume", null, 600000),
                new ProductThumbnail(26, "Body Wash", null, 150000),
                new ProductThumbnail(27, "Hair Conditioner", null, 200000),
                new ProductThumbnail(28, "Shampoo", null, 180000),
                new ProductThumbnail(27, "Hair Conditioner", null, 200000),
                new ProductThumbnail(28, "Shampoo", null, 180000),
            };

            // Filter by keyword
            if (!string.IsNullOrEmpty(keyword))
            {
                db = db.Where(p => p.Name.ToLower().Contains(keyword.ToLower())).ToList();
            }

            return new Tuple<List<ProductThumbnail>, int>(db, db.Count);
        }

        public List<ProductThumbnail> GetListNewProduct(string keyword = "", int pageIndex = 1, int rowsPerPage = 10, bool nameAscending = false)
        {
            var db = new List<ProductThumbnail>()
            {
                new ProductThumbnail(1, "Moisturizing Cream", null, 250000),
                new ProductThumbnail(2, "Sunscreen SPF 50", null, 300000),
                new ProductThumbnail(3, "Vitamin C Serum", null, 400000),
            };

            // Filter by keyword
            if (!string.IsNullOrEmpty(keyword))
            {
                db = db.Where(p => p.Name.ToLower().Contains(keyword.ToLower())).ToList();
            }

            return db;
        }

        public List<ProductThumbnail> GetListBestSeller(string keyword = "", int pageIndex = 1, int rowsPerPage = 10, bool nameAscending = false)
        {
            var db = new List<ProductThumbnail>()
            {
                new ProductThumbnail(1, "Moisturizing Cream", null, 250000),
                new ProductThumbnail(2, "Sunscreen SPF 50", null, 300000),
                new ProductThumbnail(3, "Vitamin C Serum", null, 400000),
            };

            // Filter by keyword
            if (!string.IsNullOrEmpty(keyword))
            {
                db = db.Where(p => p.Name.ToLower().Contains(keyword.ToLower())).ToList();
            }

            return db;
        }
        public void InsertProduct(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
