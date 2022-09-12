using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RentAppApi.Service.Categories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RentAppApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        ICategory _category;
        public CategoryController(ICategory category)
        {
            _category = category;
        }

        [HttpPost("InsertCategory")]
        public async Task<IActionResult> InsertCategory(Tables.Category category)
        {
            var cat = await _category.InsertCategory(category);
            return Ok(cat);
        }

        [HttpPut("UpdateCategory")]
        public async Task<IActionResult> UpdateCategory(Tables.Category category)
        {
            var cat = await _category.UpdateCategory(category);
            return Ok(cat);
        }

        [HttpDelete("DeleteCategory")]
        public async Task<IActionResult> DeleteCategory(Guid Idcategory)
        {
            var cat = await _category.DeleteCategory(Idcategory);
            return Ok(cat);
        }

        [HttpGet("SelectCategory")]
        public async Task<IActionResult> SelectCategory(Guid Idcategory)
        {
            var cat = await _category.SelectCategory(Idcategory);
            return Ok(cat);
        }

        [HttpGet("SelectAllCategory")]
        public async Task<IActionResult> SelectAllCategory()
        {
            var cat = await _category.SelectAllCategory();
            return Ok(cat);
        }
    }
}

