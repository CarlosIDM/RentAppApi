using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RentAppApi.Helpers;
using RentAppApi.Models;
using RentAppApi.Service.Products;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RentAppApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        IProductsService _product;
        public ProductsController(IProductsService productsService)
        {
            _product = productsService;
        }

        [HttpPost("InsertCalification")]
        public async Task<IActionResult> InsertCalification(CalificationModel calification)
        {
            var pro = await _product.InsertCalification(calification.IdProduct, calification.Calification, calification.IdUser);
            return Ok(pro);
        }

        [HttpGet("CalificationXProduct")]
        public async Task<IActionResult> CalificationXProduct(Guid IdProduct)
        {
            var pro = await _product.CalificationXProduct(IdProduct);
            return Ok(pro);
        }

        [HttpPost("InsertComments")]
        public async Task<IActionResult> InsertComments(CommentsModel comments)
        {
            var pro = await _product.InsertComments(comments.Comment, comments.IdUser, comments.IdProduct);
            return Ok(pro);
        }

        [HttpGet("SelectCommentXProduct")]
        public async Task<IActionResult> SelectCommentXProduct(Guid idProduct)
        {
            var pro = await _product.SelectCommentXProduct(idProduct);
            return Ok(pro);
        }

        [HttpPost("InsertFavorite")]
        public async Task<IActionResult> InsertFavorite(FavoriteModel favorite)
        {
            var pro = await _product.InsertFavorite(favorite.IdUser, favorite.IdProduct);
            return Ok(pro);
        }


        [HttpGet("SelectFavoriteXUser")]
        public async Task<IActionResult> SelectFavoriteXUser(Guid idUser)
        {
            var pro = await _product.SelectFavoriteXUser(idUser);
            return Ok(pro);
        }

        [HttpGet("DeleteFavorite")]
        public async Task<IActionResult> DeleteFavorite(Guid idFavorite)
        {
            var pro = await _product.DeleteFavorite(idFavorite);
            return Ok(pro);
        }

        [HttpGet("ListImageProduct")]
        public async Task<IActionResult> ListImageProduct(Guid idProduct)
        {
            var pro = await _product.ListImageProduct(idProduct);
            return Ok(pro);
        }


        [HttpPost("InsertImageProduct")]
        public async Task<IActionResult> InsertImageProduct(ImageProductModel product)
        {
            var img = SaveImage.Save(product.Image, SaveImage.TypeImage.Product);
            var pro = await _product.InsertImageProduct(product.IdProduct, img);
            return Ok(pro);
        }


        [HttpPost("InsertProduct")]
        public async Task<IActionResult> InsertProduct(Tables.Products products)
        {
            var pro = await _product.InsertProduct(products);
            return Ok(pro);
        }

        [HttpPut("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct(Tables.Products products)
        {
            var pro = await _product.UpdateProduct(products);
            return Ok(pro);
        }


        [HttpGet("SelectXIDProduct")]
        public async Task<IActionResult> SelectXIDProduct(Guid idProducts)
        {
            var pro = await _product.SelectXIDProduct(idProducts);
            return Ok(pro);
        }

        [HttpGet("SelectProductXCategory")]
        public async Task<IActionResult> SelectProductXCategory(Guid idCategory)
        {
            var pro = await _product.SelectProductXCategory(idCategory);
            return Ok(pro);
        }

        [HttpGet("SelectProductXSubCategory")]
        public async Task<IActionResult> SelectProductXSubCategory(Guid idSubCategory)
        {
            var pro = await _product.SelectProductXSubCategory(idSubCategory);
            return Ok(pro);
        }

        [HttpPost("SelectProductXPrice")]
        public async Task<IActionResult> SelectProductXPrice(PriceModel price)
        {
            var pro = await _product.SelectProductXPrice(price.PriceMin, price.PriceMax);
            return Ok(pro);
        }

        [HttpPost("SelectProductXGeocordinate")]
        public async Task<IActionResult> SelectProductXGeocordinate(GeoCordinateModel geoCordinate)
        {
            var pro = await _product.SelectProductXGeocordinate(geoCordinate.Latitude, geoCordinate.Longitude, geoCordinate.Radius);
            return Ok(pro);
        }

        [HttpGet("SelectProductXCalification")]
        public async Task<IActionResult> SelectProductXCalification(bool minorMayor)
        {
            var pro = await _product.SelectProductXCalification(minorMayor);
            return Ok(pro);
        }

        [HttpGet("SelectProductXPriceMinMax")]
        public async Task<IActionResult> SelectProductXPriceMinMax(bool minorMayor)
        {
            var pro = await _product.SelectProductXPriceMinMax(minorMayor);
            return Ok(pro);
        }

        [HttpGet("SelectProductXDate")]
        public async Task<IActionResult> SelectProductXDate(DateTime date)
        {
            var pro = await _product.SelectProductXDate(date);
            return Ok(pro);
        }

        [HttpGet("SelectProductXcountPerson")]
        public async Task<IActionResult> SelectProductXcountPerson(int countPerson)
        {
            var pro = await _product.SelectProductXcountPerson(countPerson);
            return Ok(pro);
        }

        [HttpGet("SelectProductXDiscount")]
        public async Task<IActionResult> SelectProductXDiscount()
        {
            var pro = await _product.SelectProductXDiscount();
            return Ok(pro);
        }
    }
}

