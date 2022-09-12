using System;
using Microsoft.Extensions.Options;
using RentAppApi.DataBases;
using RentAppApi.Helpers;
using RentAppApi.Tables;

namespace RentAppApi.Service.Categories
{
    public class SubCategories : ISubCategories
    {
        private readonly RentAppDbContext _context;
        private readonly AppSettings _appSettings;
        public SubCategories(RentAppDbContext context, IOptions<AppSettings> appSettings)
        {
            _context = context;
            _appSettings = appSettings.Value;
        }

        public async Task<Response<SubCategory>> DeleteSubCategory(Guid Idcategory)
        {
            try
            {
                using var transacction = _context.Database.BeginTransaction();
                var sel = _context.SubCategory.SingleOrDefault(c => c.SubCategoryId == Idcategory);
                if (sel != null)
                {
                    sel.Status = false;
                    var del = _context.SubCategory.Update(sel);
                    _context.SaveChanges();
                    var hi = _context.SubCategoryHistory.Add(new SubCategoryHistory
                    {
                        Action = ValueSQL.DELETE,
                        DateCreated = DateTime.UtcNow,
                        SubCategory = del.Entity
                    });
                    _context.SaveChanges();
                    transacction.Commit();
                    var response = new Response<Tables.SubCategory>()
                    {
                        Message = string.Empty,
                        Count = 1,
                        Result = del.Entity
                    };
                    return response;
                }
                else
                {
                    var response = new Response<Tables.SubCategory>()
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
                var response = new Response<Tables.SubCategory>()
                {
                    Message = ex.StackTrace,
                    Count = 0,
                    Result = null
                };
                return response;
            }
        }

        public async Task<Response<SubCategory>> InsertSubCategory(SubCategory category)
        {
            try
            {
                using var transacction = _context.Database.BeginTransaction();
                var up = _context.SubCategory.Add(category);
                _context.SaveChanges();
                var hi = _context.SubCategoryHistory.Add(new SubCategoryHistory
                {
                    Action = ValueSQL.INSERT,
                    DateCreated = DateTime.UtcNow,
                    SubCategory = up.Entity
                });
                _context.SaveChanges();
                transacction.Commit();
                var response = new Response<Tables.SubCategory>()
                {
                    Message = string.Empty,
                    Count = 1,
                    Result = up.Entity
                };
                return response;
            }
            catch (Exception ex)
            {
                var response = new Response<Tables.SubCategory>()
                {
                    Message = ex.StackTrace,
                    Count = 0,
                    Result = null
                };
                return response;
            }
        }

        public async Task<Response<List<SubCategory>>> SelectAllSubCategory()
        {
            try
            {
                var listCat = _context.SubCategory.Where(c => c.Status).ToList();
                if (listCat != null && listCat.Count > 0)
                {
                    var response = new Response<List<Tables.SubCategory>>()
                    {
                        Count = listCat.Count,
                        Result = listCat,
                        Message = string.Empty
                    };
                    return response;
                }
                else
                {
                    var response = new Response<List<Tables.SubCategory>>()
                    {
                        Count = 0,
                        Result = null,
                        Message = "No categorias"
                    };
                    return response;
                }
            }
            catch (Exception ex)
            {
                var response = new Response<List<Tables.SubCategory>>()
                {
                    Count = 0,
                    Result = null,
                    Message = ex.StackTrace
                };
                return response;
            }
        }

        public async Task<Response<SubCategory>> SelectSubCategory(Guid IdSubcategory)
        {
            try
            {
                using var transacction = _context.Database.BeginTransaction();
                var cat = _context.SubCategory.SingleOrDefault(c => c.SubCategoryId == IdSubcategory && c.Status);
                if (cat != null)
                {
                    _context.SubCategoryHistory.Add(new SubCategoryHistory
                    {
                        Action = ValueSQL.SELECT,
                        DateCreated = DateTime.UtcNow,
                        SubCategory = cat
                    });
                    _context.SaveChanges();
                    transacction.Commit();
                    var response = new Response<Tables.SubCategory>()
                    {
                        Count = 1,
                        Result = cat,
                        Message = string.Empty
                    };
                    return response;
                }
                else
                {
                    var response = new Response<Tables.SubCategory>()
                    {
                        Count = 0,
                        Result = null,
                        Message = "No existe la categoria que ha solicitado"
                    };
                    return response;
                }
            }
            catch (Exception ex)
            {
                var response = new Response<Tables.SubCategory>()
                {
                    Count = 0,
                    Result = null,
                    Message = ex.StackTrace
                };
                return response;
            }
        }

        public async Task<Response<List<SubCategory>>> SelectSubCategoryXID(Guid Idcategory)
        {
            try
            {
                var listCat = _context.SubCategory.Where(c => c.Status && c.Category.CategoryId == Idcategory).ToList();
                if (listCat != null && listCat.Count > 0)
                {
                    var response = new Response<List<Tables.SubCategory>>()
                    {
                        Count = listCat.Count,
                        Result = listCat,
                        Message = string.Empty
                    };
                    return response;
                }
                else
                {
                    var response = new Response<List<Tables.SubCategory>>()
                    {
                        Count = 0,
                        Result = null,
                        Message = "No hay sub categorias"
                    };
                    return response;
                }
            }
            catch (Exception ex)
            {
                var response = new Response<List<Tables.SubCategory>>()
                {
                    Count = 0,
                    Result = null,
                    Message = ex.StackTrace
                };
                return response;
            }
        }

        public async Task<Response<SubCategory>> UpdateSubCategory(SubCategory category)
        {
            try
            {
                using var transacction = _context.Database.BeginTransaction();
                var up = _context.SubCategory.Update(category);
                _context.SaveChanges();
                var hi = _context.SubCategoryHistory.Add(new SubCategoryHistory
                {
                    Action = ValueSQL.UPDATE,
                    DateCreated = DateTime.UtcNow,
                    SubCategory = up.Entity
                });
                _context.SaveChanges();
                transacction.Commit();
                var response = new Response<Tables.SubCategory>()
                {
                    Message = string.Empty,
                    Count = 1,
                    Result = up.Entity
                };
                return response;
            }
            catch (Exception ex)
            {
                var response = new Response<Tables.SubCategory>()
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

