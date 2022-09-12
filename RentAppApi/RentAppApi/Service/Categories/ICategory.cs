using System;
using RentAppApi.Helpers;
using RentAppApi.Tables;

namespace RentAppApi.Service.Categories
{
    public interface ICategory
    {
        Task<Response<Tables.Category>> InsertCategory(Tables.Category category);
        Task<Response<Tables.Category>> UpdateCategory(Tables.Category category);
        Task<Response<Tables.Category>> DeleteCategory(Guid Idcategory);
        Task<Response<Tables.Category>> SelectCategory(Guid Idcategory);
        Task<Response<List<Tables.Category>>> SelectAllCategory();
    }
}

