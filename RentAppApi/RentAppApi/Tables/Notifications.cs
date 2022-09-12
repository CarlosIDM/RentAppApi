using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentAppApi.Tables
{
    public class Notifications
    {
        [Key]
        public Guid IdNotifications { get; set; }

        public string Token { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateUpdated { get; set; }

        //[ForeignKey("UserId")]
        public User User { get; set; }
    }
}

