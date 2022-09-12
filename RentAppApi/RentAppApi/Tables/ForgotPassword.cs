using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentAppApi.Tables
{
    public class ForgotPassword
    {
        [Key]
        public Guid IdPassword { get; set; }

        public string Code { get; set; }

        public DateTime DateCreated { get; set; } = DateTime.UtcNow;

        //[ForeignKey("UserId")]
        public User User { get; set; }
    }
}

