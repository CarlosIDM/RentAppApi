using System;
using RentAppApi.Helpers;
using RentAppApi.Tables;
using RentAppApi.ViewModels;

namespace RentAppApi.Service.Products
{
    public interface IProductsService
    {
        Task<Response<Califications>> InsertCalification(Guid idProduct, double calification, Guid IdUser);
        Task<Response<CalificationVM>> CalificationXProduct(Guid idProduct);
        Task<Response<Comments>> InsertComments(string comment, Guid idUser, Guid idProduct);
        Task<Response<List<UserCommentVM>>> SelectCommentXProduct(Guid idProduct);
        Task<Response<Favorites>> InsertFavorite(Guid idUser, Guid idProduct);
        Task<Response<List<FavotiteProductsVM>>> SelectFavoriteXUser(Guid idUser);
        Task<Response<Favorites>> DeleteFavorite(Guid idFavorite);
        Task<Response<List<ImageProduct>>> ListImageProduct(Guid idProduct);
        Task<Response<ImageProduct>> InsertImageProduct(Guid idProduct, string image);
        Task<Response<Tables.Products>> InsertProduct(Tables.Products products);
        Task<Response<Tables.Products>> UpdateProduct(Tables.Products products);
        Task<Response<Tables.Products>> SelectXIDProduct(Guid idProducts);
        Task<Response<List<Tables.Products>>> SelectProductXCategory(Guid idCategory);
        Task<Response<List<Tables.Products>>> SelectProductXSubCategory(Guid idSubCategory);
        Task<Response<List<Tables.Products>>> SelectProductXPrice(double priceMin, double priceMax);
        Task<Response<List<Tables.Products>>> SelectProductXGeocordinate(double latitude, double longitude, float Radius);
        Task<Response<List<Tables.Products>>> SelectProductXCalification(bool mayor);
        Task<Response<List<Tables.Products>>> SelectProductXPriceMinMax(bool mayor);
        Task<Response<List<Tables.Products>>> SelectProductXDate(DateTime date);
        Task<Response<List<Tables.Products>>> SelectProductXcountPerson(int countPerson);
        Task<Response<List<Tables.Products>>> SelectProductXDiscount();
    }
}

