using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RentAppApi.Models;
using RentAppApi.Service.Codes;
using RentAppApi.Service.Users;
using RentAppApi.Tables;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RentAppApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CodeController : ControllerBase
    {
        private ICode _code;

        public CodeController(ICode _code)
        {
            this._code = _code;
        }

        [HttpPost("SelectCode")]
        public async Task<IActionResult> SelectCode(User user)
        {
            var us = await _code.SelectCode(user.Code, user.Email, user.Phone);
            return Ok(us);
        }

        [HttpPut("UpdateCode")]
        public async Task<IActionResult> UpdateCode(UpdateCode code)
        {
            var us = await _code.UpdateCode(code.Email, code.Phone);
            return Ok(us);
        }
    }
}

