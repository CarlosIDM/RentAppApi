using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentAppApi.Tables
{
    public class UserHistory
    {
        [Key]
        public Guid Id { get; set; }

        [StringLength(20)]
        public string Action { get; set; }

        public DateTime DateCreated { get; set; }

        //[ForeignKey("UserId")]
        public User User { get; set; }
    }
}

