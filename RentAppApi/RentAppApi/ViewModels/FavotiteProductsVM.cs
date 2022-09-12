using System;
namespace RentAppApi.ViewModels
{
    public class FavotiteProductsVM
    {
        public Guid IdFavorite { get; set; }
        public Tables.Products Product { get; set; }
    }
}

