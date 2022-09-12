using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RentAppApi.Service.Categories;
using RentAppApi.Tables;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RentAppApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubCategoryController : ControllerBase
    {
        ISubCategories _subCategories;
        public SubCategoryController(ISubCategories subCategories)
        {
            _subCategories = subCategories;
        }

        [HttpPost("InsSubCategory")]
        public async Task<IActionResult> InsSubCategory(SubCategory subCategory)
        {
            var us = await _subCategories.InsertSubCategory(subCategory);
            return Ok(us);
        }

        [HttpPut("UpdateSubCategory")]
        public async Task<IActionResult> UpdateSubCategory(SubCategory subCategory)
        {
            var us = await _subCategories.UpdateSubCategory(subCategory);
            return Ok(us);
        }

        [HttpDelete("DeleteSubCategory")]
        public async Task<IActionResult> DeleteSubCategory(Guid IdsubCategory)
        {
            var us = await _subCategories.DeleteSubCategory(IdsubCategory);
            return Ok(us);
        }

        [HttpGet("SelectSubCategory")]
        public async Task<IActionResult> SelectSubCategory(Guid IdsubCategory)
        {
            var us = await _subCategories.SelectSubCategory(IdsubCategory);
            return Ok(us);
        }

        [HttpGet("SelectSubCategoryXID")]
        public async Task<IActionResult> SelectSubCategoryXID(Guid IdsubCategory)
        {
            var us = await _subCategories.SelectSubCategoryXID(IdsubCategory);
            return Ok(us);
        }

        [HttpGet("SelectAllSubCategory")]
        public async Task<IActionResult> SelectAllSubCategory()
        {
            var us = await _subCategories.SelectAllSubCategory();
            return Ok(us);
        }
    }
}

