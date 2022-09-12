using System;
using Microsoft.Extensions.Options;
using RentAppApi.DataBases;
using RentAppApi.Helpers;
using RentAppApi.Tables;

namespace RentAppApi.Service.Categories
{
    public class Category : ICategory
    {
        private readonly RentAppDbContext _context;
        private readonly AppSettings _appSettings;
        public Category(RentAppDbContext context, IOptions<AppSettings> appSettings)
        {
            _context = context;
            _appSettings = appSettings.Value;
        }

        public async Task<Response<Tables.Category>> DeleteCategory(Guid Idcategory)
        {
            try
            {
                using var transacction = _context.Database.BeginTransaction();
                var sel = _context.Category.SingleOrDefault(c => c.CategoryId == Idcategory);
                if(sel != null)
                {
                    var valSubCat = _context.SubCategory.Where(c => c.Category.CategoryId == sel.CategoryId && c.Status).ToList();
                    if(valSubCat != null && valSubCat.Count > 0)
                    {
                        //HAY SUBCATEGORIAS ACTIVAS
                        var response = new Response<Tables.Category>()
                        {
                            Message = "No se puede eliminar la categoria, tiene subcategorias activas",
                            Count = 0,
                            Result = null
                        };
                        return response;
                    }
                    else
                    {
                        sel.Status = false;
                        var del = _context.Category.Update(sel);
                        _context.SaveChanges();
                        var hi = _context.CategoryHistory.Add(new CategoryHistory
                        {
                            Action = ValueSQL.DELETE,
                            DateCreated = DateTime.UtcNow,
                            Category = del.Entity
                        });
                        _context.SaveChanges();
                        transacction.Commit();
                        var response = new Response<Tables.Category>()
                        {
                            Message = string.Empty,
                            Count = 1,
                            Result = del.Entity
                        };
                        return response;
                    }                    
                }
                else
                {
                    var response = new Response<Tables.Category>()
                    {
                        Message = "No se pudo eliminar la categoria",
                        Count = 0,
                        Result = null
                    };
                    return response;
                }
            }
            catch (Exception ex)
            {
                var response = new Response<Tables.Category>()
                {
                    Message = ex.StackTrace,
                    Count = 0,
                    Result = null
                };
                return response;
            }
        }

        public async Task<Response<Tables.Category>> InsertCategory(Tables.Category category)
        {
            try
            {
                using var transacction = _context.Database.BeginTransaction();
                var up = _context.Category.Add(category);
                _context.SaveChanges();
                var hi = _context.CategoryHistory.Add(new CategoryHistory
                {
                    Action = ValueSQL.INSERT,
                    DateCreated = DateTime.UtcNow,
                    Category = up.Entity
                });
                _context.SaveChanges();
                transacction.Commit();
                var response = new Response<Tables.Category>()
                {
                    Message = string.Empty,
                    Count = 1,
                    Result = up.Entity
                };
                return response;
            }
            catch (Exception ex)
            {
                var response = new Response<Tables.Category>()
                {
                    Message = ex.StackTrace,
                    Count = 0,
                    Result = null
                };
                return response;
            }
        }

        public async Task<Response<List<Tables.Category>>> SelectAllCategory()
        {
            try
            {
                var listCat = _context.Category.Where(c => c.Status).ToList();
                if (listCat != null && listCat.Count > 0)
                {
                    var response = new Response<List<Tables.Category>>()
                    {
                        Count = listCat.Count,
                        Result = listCat,
                        Message = string.Empty
                    };
                    return response;
                }
                else
                {
                    var response = new Response<List<Tables.Category>>()
                    {
                        Count = 0,
                        Result = null,
                        Message = "No categorias"
                    };
                    return response;
                }
            }
            catch(Exception ex)
            {
                var response = new Response<List<Tables.Category>>()
                {
                    Count = 0,
                    Result = null,
                    Message = ex.StackTrace
                };
                return response;
            }
        }

        public async Task<Response<Tables.Category>> SelectCategory(Guid Idcategory)
        {
            try
            {
                using var transacction = _context.Database.BeginTransaction();
                var cat = _context.Category.SingleOrDefault(c => c.CategoryId == Idcategory && c.Status);
                if(cat != null)
                {
                    _context.CategoryHistory.Add(new CategoryHistory
                    {
                        Action = ValueSQL.SELECT,
                        DateCreated = DateTime.UtcNow,
                        Category = cat
                    });
                    _context.SaveChanges();
                    transacction.Commit();
                    var response = new Response<Tables.Category>()
                    {
                        Count = 1,
                        Result = cat,
                        Message = string.Empty
                    };
                    return response;
                }
                else
                {
                    var response = new Response<Tables.Category>()
                    {
                        Count = 0,
                        Result = null,
                        Message = "No existe la categoria que ha solicitado"
                    };
                    return response;
                }
            }
            catch(Exception ex)
            {
                var response = new Response<Tables.Category>()
                {
                    Count = 0,
                    Result = null,
                    Message = ex.StackTrace
                };
                return response;
            }
        }

        public async Task<Response<Tables.Category>> UpdateCategory(Tables.Category category)
        {
            try
            {
                using var transacction = _context.Database.BeginTransaction();
                var up = _context.Category.Update(category);
                _context.SaveChanges();
                var hi = _context.CategoryHistory.Add(new CategoryHistory
                {
                    Action = ValueSQL.UPDATE,
                    DateCreated = DateTime.UtcNow,
                    Category = up.Entity
                });
                _context.SaveChanges();
                transacction.Commit();
                var response = new Response<Tables.Category>()
                {
                    Message = string.Empty,
                    Count = 1,
                    Result = up.Entity
                };
                return response;
            }
            catch(Exception ex)
            {
                var response = new Response<Tables.Category>()
                {
                    Message = ex.StackTrace,
                    Count = 0,
                    Result = null
                };
                return response;
            }
        }
    }
}

