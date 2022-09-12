using System;
using RentAppApi.Helpers;
using RentAppApi.Tables;

namespace RentAppApi.Service.Categories
{
    public interface ISubCategories
    {
        Task<Response<SubCategory>> InsertSubCategory(SubCategory category);
        Task<Response<SubCategory>> UpdateSubCategory(SubCategory category);
        Task<Response<SubCategory>> DeleteSubCategory(Guid Idcategory);
        Task<Response<SubCategory>> SelectSubCategory(Guid Idcategory);
        Task<Response<List<SubCategory>>> SelectSubCategoryXID(Guid Idcategory);
        Task<Response<List<SubCategory>>> SelectAllSubCategory();
    }
}

