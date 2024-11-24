﻿using Cosmetics_Shop.Models.Enums;
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
        void InsertProduct(ProductThumbnail product);

        Task<SearchResult> GetListProductThumbnailAsync(
            string keyword = "",
            int pageIndex = 1,
            int productsPerPage = 10,
            SortProduct sortProduct = SortProduct.DateAscending,
            string filterBrand = "",
            int minPrice = 0,
            int maxPrice = int.MaxValue);

        Task<List<string>> GetSuggestionsAsync(string keyword);

        ProductDetail GetProductDetail(int idProduct);

        List<ProductThumbnail> GetListNewProductAsync();

        List<ProductThumbnail> GetListBestSellerAsync();

        List<ProductThumbnail> GetListRecentlyViewAsync();

        List<CartThumbnail> GetListCartProduct();

        List<ReviewThumbnail> GetListReviewThumbnail();

        List<ReviewThumbnail> GetListReviewThumbnailByIDProduct(int idProduct);

        Task<LoginResult> CheckLoginAsync(string username, string password);

        List<Voucher> GetAllVouchers();

        //Voucher GetVoucherByCode(string code);
        //void AddVoucher(Voucher voucher);
        //void DeleteVoucher(int id);
        //void UpdateVoucher(Voucher updatedVoucher);
    }
}
