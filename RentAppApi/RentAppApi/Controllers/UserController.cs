using Microsoft.AspNetCore.Mvc;
using RentAppApi.Helpers;
using RentAppApi.Models;
using RentAppApi.Service.Users;
using RentAppApi.Tables;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RentAppApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("InsUser")]
        public async Task<IActionResult> InsertUser(UserInsert user)
        {
            var img = SaveImage.Save(user.Image, SaveImage.TypeImage.User);
            user.User.Url = img;
            var us = await _userService.InsertUser(user.User);
            return Ok(us);
        }

        [HttpPost("SelectUser")]
        public async Task<IActionResult> SelectUser(UserModel user)
        {
            var us = await _userService.SelectUser(user.Email, user.Password);
            return Ok(us);
        }

        [HttpPut("UpdateUser")]
        public async Task<IActionResult> UpdateUser(User user)
        {
            var us = await _userService.UpdateUser(user);
            return Ok(us);
        }

        [HttpDelete("DeleteUser")]
        public async Task<IActionResult> DeleteUser(User user)
        {
            var us = await _userService.DeleteUser(user);
            return Ok(us);
        }

        [HttpPost("SendCodePassword")]
        public async Task<IActionResult> SendCodePassword(ForgotPasswordModel model)
        {
            var us = await _userService.ForgotPassword(model.IdUser, model.Email);
            return Ok(us);
        }

        [HttpPost("RecoveryPassword")]
        public async Task<IActionResult> RecoveryPassword(ForgotPasswordModel model)
        {
            var us = await _userService.ForgotPassword(model.IdUser, model.Email, model.Code, model.Password);
            return Ok(us);
        }

        [HttpPost("UpdateNotification")]
        public async Task<IActionResult> UpdateNotification(NotificationModel model)
        {
            var us = await _userService.UpdateNotification(model.IdUser, model.PushToken);
            return Ok(us);
        }
    }
}

