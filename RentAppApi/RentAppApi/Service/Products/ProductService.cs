using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RentAppApi.DataBases;
using RentAppApi.Helpers;
using RentAppApi.Tables;
using RentAppApi.ViewModels;

namespace RentAppApi.Service.Products
{
    public class ProductService : IProductsService
    {
        private readonly RentAppDbContext _context;
        private readonly AppSettings _appSettings;
        private readonly IEmailService emailService;
        public ProductService(RentAppDbContext context, IOptions<AppSettings> appSettings, IEmailService emailService)
        {
            this.emailService = emailService;
            _context = context;
            _appSettings = appSettings.Value;
        }

        public async Task<Response<CalificationVM>> CalificationXProduct(Guid idProduct)
        {
            try
            {
                var product = _context.Products.SingleOrDefault(c => c.ProductId == idProduct);
                var cal = _context.Califications.Where(c => c.Products.ProductId == idProduct).ToList();
                if (product != null && cal != null)
                {
                    var cl = new CalificationVM();
                    cl.Calification = product.Calification.Value;
                    foreach (var item in cal)
                    {
                        cl.ListCalification.Add(new UserCalificationProduct
                        {
                            Calification = item.Calification,
                            Date = item.DateCreated,
                            Name = item.Products.User.Name,
                            Image = item.Products.User.Url
                        });
                    }
                    return new Response<CalificationVM>
                    {
                        Result = cl,
                        Count = cl.ListCalification.Count,
                        Message = string.Empty
                    };
                }
                return new Response<CalificationVM>
                {
                    Result = null,
                    Count = 0,
                    Message = "No hay calificación para este producto"
                };
            }
            catch (Exception ex)
            {
                return new Response<CalificationVM>
                {
                    Result = null,
                    Count = 0,
                    Message = ex.StackTrace
                };
            }
        }

        public async Task<Response<Favorites>> DeleteFavorite(Guid idFavorite)
        {
            var response = new Response<Favorites>();
            response.Count = 0;
            response.Result = null;
            try
            {
                var fav = _context.Favorites.SingleOrDefault(c => c.IdFavorite == idFavorite);
                if (fav != null)
                {
                    fav.Status = false;
                    var favUp = _context.Favorites.Update(fav);
                    _context.SaveChanges();
                    response.Result = fav;
                    response.Count = 1;
                }
                return response;
            }
            catch (Exception ex)
            {
                response.Message = ex.StackTrace;
                return response;
            }
        }

        public async Task<Response<Califications>> InsertCalification(Guid idProduct, double calification, Guid IdUser)
        {
            try
            {
                var user = _context.User.SingleOrDefault(c => c.UserId == IdUser);
                var produc = _context.Products.SingleOrDefault(c => c.ProductId == idProduct);
                var cal = _context.Califications.Add(new Califications
                {
                    Calification = calification,
                    DateCreated = DateTime.UtcNow,
                    //User = user,
                    Products = produc
                });
                var sumCalification = _context.Califications.Where(c => c.Products.ProductId == idProduct).ToList().Sum(c => c.Calification);
                produc.Calification = sumCalification;
                _context.Products.Update(produc);
                _context.SaveChanges();
                return new Response<Califications>
                {
                    Count = 1,
                    Message = String.Empty,
                    Result = cal.Entity
                };
            }
            catch (Exception ex)
            {
                return new Response<Califications>
                {
                    Count = 0,
                    Message = ex.StackTrace,
                    Result = null
                };
            }
        }

        public async Task<Response<Comments>> InsertComments(string comment, Guid idUser, Guid idProduct)
        {
            try
            {
                var response = new Response<Comments>();
                var product = _context.Products.SingleOrDefault(c => c.ProductId == idProduct);
                var user = _context.User.SingleOrDefault(c => c.UserId == idUser);
                if (product != null && user != null)
                {
                    var com = _context.Comments.Add(new Comments
                    {
                        DateCreated = DateTime.UtcNow,
                        Message = comment,
                        Status = true,
                        Products = product,
                        User = user
                    });
                    response.Count = 1;
                    response.Message = "Se guardo correctamente el comentario";
                    response.Result = com.Entity;
                }
                return response;
            }
            catch (Exception ex)
            {
                return new Response<Comments>
                {
                    Count = 0,
                    Result = null,
                    Message = ex.StackTrace
                };
            }
        }

        public async Task<Response<Favorites>> InsertFavorite(Guid idUser, Guid idProduct)
        {
            var response = new Response<Favorites>();
            response.Count = 0;
            response.Result = null;
            try
            {
                var pro = _context.Products.SingleOrDefault(c => c.ProductId == idProduct);
                var user = _context.User.SingleOrDefault(c => c.UserId == idUser);
                if (pro != null && user != null)
                {
                    var fav = _context.Favorites.Add(new Favorites
                    {
                        DateCreated = DateTime.UtcNow,
                        Products = pro,
                        Status = true,
                        User = user
                    });
                    response.Result = fav.Entity;
                    response.Count = 1;
                }
                return response;
            }
            catch (Exception ex)
            {
                response.Message = ex.StackTrace;
                return response;
            }
        }

        public async Task<Response<ImageProduct>> InsertImageProduct(Guid idProduct, string image)
        {
            try
            {
                var pro = _context.Products.SingleOrDefault(c => c.ProductId == idProduct);
                var ins = _context.ImageProduct.Add(new ImageProduct
                {
                    Image = image,
                    Products = pro,
                });
                _context.SaveChanges();
                return new Response<ImageProduct>
                {
                    Result = ins.Entity,
                    Message = string.Empty,
                    Count = 1
                };
            }
            catch (Exception ex)
            {
                return new Response<ImageProduct>
                {
                    Result = null,
                    Message = ex.StackTrace,
                    Count = 0
                };
            }
        }

        public async Task<Response<Tables.Products>> InsertProduct(Tables.Products products)
        {
            try
            {
                var pro = _context.Products.Add(products);
                _context.SaveChanges();
                return new Response<Tables.Products>()
                {
                    Count = (pro.Entity != null) ? 1 : 0,
                    Result = pro.Entity,
                    Message = (pro.Entity != null) ? "Se guardo correctamente el producto" : "No se pudo guardar el producto"
                };
            }
            catch (Exception ex)
            {
                return new Response<Tables.Products>()
                {
                    Count = 0,
                    Message = ex.StackTrace,
                    Result = null
                };
            }
        }

        public async Task<Response<List<ImageProduct>>> ListImageProduct(Guid idProduct)
        {
            var response = new Response<List<ImageProduct>>();
            response.Result = null;
            response.Count = 0;
            try
            {
                var proList = _context.ImageProduct.Where(c => c.Products.ProductId == idProduct).ToList();
                response.Result = proList;
                response.Count = (proList != null && proList.Count > 0) ? proList.Count : 0;
                return response;
            }
            catch (Exception ex)
            {
                return response;
            }
        }

        public async Task<Response<List<UserCommentVM>>> SelectCommentXProduct(Guid idProduct)
        {
            var response = new Response<List<UserCommentVM>>();
            try
            {
                var comm = _context.Comments.Where(c => c.Products.ProductId == idProduct).ToList();
                if (comm != null && comm.Count > 0)
                {
                    var userCom = new List<UserCommentVM>();
                    foreach (var item in comm)
                    {
                        userCom.Add(new UserCommentVM
                        {
                            Comment = item.Message,
                            Date = item.DateCreated,
                            IdUser = item.User.UserId,
                            Image = item.User.Url,
                            NameUser = item.User.Name
                        });
                    }
                    response.Count = userCom.Count;
                    response.Result = userCom;
                    return response;
                }
                response.Count = 0;
                response.Result = null;
                response.Message = "No hay comentarios para este producto";
                return response;
            }
            catch (Exception ex)
            {
                response.Count = 0;
                response.Result = null;
                response.Message = ex.StackTrace;
                return response;
            }
        }

        public async Task<Response<List<FavotiteProductsVM>>> SelectFavoriteXUser(Guid idUser)
        {
            var response = new Response<List<FavotiteProductsVM>>();
            response.Count = 0;
            response.Message = "No tiene favoritos";
            response.Result = null;
            try
            {
                var product = _context.Favorites.Where(c => c.User.UserId == idUser).ToList();
                if (product != null && product.Count > 0)
                {
                    var list = new List<FavotiteProductsVM>();
                    foreach (var item in product)
                    {
                        var d = new FavotiteProductsVM()
                        {
                            IdFavorite = item.IdFavorite,
                            Product = item.Products
                        };
                        list.Add(d);
                    }
                    response.Count = list.Count;
                    response.Result = list;
                }
                return response;
            }
            catch (Exception ex)
            {
                return new Response<List<FavotiteProductsVM>>
                {
                    Count = 0,
                    Message = ex.StackTrace,
                    Result = null
                };
            }
        }

        public async Task<Response<List<Tables.Products>>> SelectProductXCalification(bool mayor)
        {
            try
            {
                if (mayor)
                {
                    //MAYOR A MENOR
                    var select = _context.Products.Where(c => c.Status && !c.IsRentSale).OrderByDescending(c => c.Calification).ToList();
                    return new Response<List<Tables.Products>>
                    {
                        Count = (select == null) ? 0 : select.Count,
                        Message = (select == null && select.Count == 0) ? "No hay productos con esa categoria" : String.Empty,
                        Result = select
                    };
                }
                else
                {
                    //MENOR A MAYO
                    var select = _context.Products.Where(c => c.Status && !c.IsRentSale).OrderBy(c => c.Calification).ToList();
                    return new Response<List<Tables.Products>>
                    {
                        Count = (select == null) ? 0 : select.Count,
                        Message = (select == null && select.Count == 0) ? "No hay productos con esa categoria" : String.Empty,
                        Result = select
                    };
                }
            }
            catch (Exception ex)
            {
                return new Response<List<Tables.Products>>
                {
                    Count = 0,
                    Message = ex.StackTrace,
                    Result = null
                };
            }
        }

        public async Task<Response<List<Tables.Products>>> SelectProductXCategory(Guid idCategory)
        {
            try
            {
                var select = _context.Products.Where(c => c.SubCategory.Category.CategoryId == idCategory && c.Status && !c.IsRentSale).ToList();
                return new Response<List<Tables.Products>>
                {
                    Count = (select == null) ? 0 : select.Count,
                    Message = (select == null && select.Count == 0) ? "No hay productos con esa categoria" : String.Empty,
                    Result = select
                };
            }
            catch (Exception ex)
            {
                return new Response<List<Tables.Products>>
                {
                    Count = 0,
                    Message = ex.StackTrace,
                    Result = null
                };
            }
        }

        public async Task<Response<List<Tables.Products>>> SelectProductXcountPerson(int countPerson)
        {
            try
            {
                var select = _context.Products.Where(c => Convert.ToInt32(c.TotalPerson) == countPerson && c.Status && !c.IsRentSale).ToList();
                return new Response<List<Tables.Products>>
                {
                    Count = (select == null) ? 0 : select.Count,
                    Message = (select == null && select.Count == 0) ? "No hay productos con esa categoria" : String.Empty,
                    Result = select
                };
            }
            catch (Exception ex)
            {
                return new Response<List<Tables.Products>>
                {
                    Count = 0,
                    Message = ex.StackTrace,
                    Result = null
                };
            }
        }

        public async Task<Response<List<Tables.Products>>> SelectProductXDate(DateTime date)
        {
            try
            {
                var select = _context.Products.Where(c => c.DateCreated == date && c.Status && !c.IsRentSale).ToList();
                return new Response<List<Tables.Products>>
                {
                    Count = (select == null) ? 0 : select.Count,
                    Message = (select == null && select.Count == 0) ? "No hay productos con esa categoria" : String.Empty,
                    Result = select
                };
            }
            catch (Exception ex)
            {
                return new Response<List<Tables.Products>>
                {
                    Count = 0,
                    Message = ex.StackTrace,
                    Result = null
                };
            }
        }

        public async Task<Response<List<Tables.Products>>> SelectProductXDiscount()
        {
            try
            {
                var select = _context.Products.Where(c => c.IsDiscount && c.Status && !c.IsRentSale).ToList();
                return new Response<List<Tables.Products>>
                {
                    Count = (select == null) ? 0 : select.Count,
                    Message = (select == null && select.Count == 0) ? "No hay productos con descuentos" : string.Empty,
                    Result = select
                };
            }
            catch (Exception ex)
            {
                return new Response<List<Tables.Products>>
                {
                    Count = 0,
                    Message = ex.StackTrace,
                    Result = null
                };
            }
        }

        public async Task<Response<List<Tables.Products>>> SelectProductXGeocordinate(double latitude, double longitude, float Radius)
        {
            try
            {
                string sql = $"SELECT IdProduct,Name,Address,Description,Price,Latitude,Longitude,Calification,Bedrooms,Bathrooms,Garage,TotalPerson,Metters,DateCreated,DateUpdated,IsRentSale,IsDiscount,Percentage,Status,SubCategory, User, ( 6371 * acos(cos(radians({latitude})) * cos(radians(Latitude)) * cos(radians(Longitude) - radians({longitude})) + sin(radians({latitude})) * sin(radians(Latitude)))) AS Distance FROM Product HAVING Distance < {Radius} ORDER BY Distance";
                var product = _context.Products.FromSqlRaw<Tables.Products>(sql).ToList();
                return new Response<List<Tables.Products>>
                {
                    Count = (product == null) ? 0 : product.Count,
                    Message = (product == null && product.Count == 0) ? "No hay productos" : string.Empty,
                    Result = product
                };
            }
            catch (Exception ex)
            {
                return new Response<List<Tables.Products>>
                {
                    Count = 0,
                    Message = ex.StackTrace,
                    Result = null
                };
            }
        }

        public async Task<Response<List<Tables.Products>>> SelectProductXPrice(double priceMin, double priceMax)
        {
            try
            {
                var select = _context.Products.Where(c => c.Price >= priceMin && c.Price <= priceMax && c.Status && !c.IsRentSale).ToList();
                return new Response<List<Tables.Products>>
                {
                    Count = (select == null) ? 0 : select.Count,
                    Message = (select == null && select.Count == 0) ? "No hay productos con esa categoria" : String.Empty,
                    Result = select
                };
            }
            catch (Exception ex)
            {
                return new Response<List<Tables.Products>>
                {
                    Count = 0,
                    Message = ex.StackTrace,
                    Result = null
                };
            }
        }

        public async Task<Response<List<Tables.Products>>> SelectProductXPriceMinMax(bool mayor)
        {
            try
            {
                if (mayor)
                {
                    //MAYOR A MENOR
                    var select = _context.Products.Where(c => c.Status && !c.IsRentSale).OrderByDescending(c => c.Price).ToList();
                    return new Response<List<Tables.Products>>
                    {
                        Count = (select == null) ? 0 : select.Count,
                        Message = (select == null && select.Count == 0) ? "No hay productos con esa categoria" : String.Empty,
                        Result = select
                    };
                }
                else
                {
                    //MENOR A MAYOR
                    var select = _context.Products.Where(c => c.Status && !c.IsRentSale).OrderBy(c => c.Price).ToList();
                    return new Response<List<Tables.Products>>
                    {
                        Count = (select == null) ? 0 : select.Count,
                        Message = (select == null && select.Count == 0) ? "No hay productos con esa categoria" : String.Empty,
                        Result = select
                    };
                }
            }
            catch (Exception ex)
            {
                return new Response<List<Tables.Products>>
                {
                    Count = 0,
                    Message = ex.StackTrace,
                    Result = null
                };
            }
        }

        public async Task<Response<List<Tables.Products>>> SelectProductXSubCategory(Guid idSubCategory)
        {
            try
            {
                var select = _context.Products.Where(c => c.SubCategory.SubCategoryId == idSubCategory && c.Status && !c.IsRentSale).ToList();
                return new Response<List<Tables.Products>>
                {
                    Count = (select == null) ? 0 : select.Count,
                    Message = (select == null && select.Count == 0) ? "No hay productos con esa categoria" : String.Empty,
                    Result = select
                };
            }
            catch (Exception ex)
            {
                return new Response<List<Tables.Products>>
                {
                    Count = 0,
                    Message = ex.StackTrace,
                    Result = null
                };
            }
        }

        public async Task<Response<Tables.Products>> SelectXIDProduct(Guid idProducts)
        {
            try
            {
                var up = _context.Products.SingleOrDefault(c => c.ProductId == idProducts && c.Status && !c.IsRentSale);
                return new Response<Tables.Products>()
                {
                    Count = (up != null) ? 1 : 0,
                    Result = up,
                    Message = (up == null) ? "No hay producto" : String.Empty
                };
            }
            catch (Exception ex)
            {
                return new Response<Tables.Products>()
                {
                    Count = 0,
                    Message = ex.StackTrace,
                    Result = null
                };
            }
        }

        public async Task<Response<Tables.Products>> UpdateProduct(Tables.Products products)
        {
            try
            {
                var up = _context.Products.Update(products);
                _context.SaveChanges();
                return new Response<Tables.Products>
                {
                    Count = (up == null) ? 0 : 1,
                    Message = (up == null) ? "No se pudo actualizar el producto" : "Se actualizo correctamente",
                    Result = up.Entity
                };
            }
            catch (Exception ex)
            {
                return new Response<Tables.Products>
                {
                    Count = 0,
                    Message = ex.Message,
                    Result = null
                };
            }
        }
    }
}

