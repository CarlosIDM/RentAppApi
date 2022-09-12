using System;
namespace RentAppApi.Models
{
    public class ForgotPasswordModel
    {
        public Guid IdUser { get;  set; }
        public string Email { get;  set; }
        public string Password { get; set; }
        public string Code { get; set; }
    }
}

