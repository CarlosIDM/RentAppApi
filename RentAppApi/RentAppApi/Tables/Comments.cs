using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentAppApi.Tables
{
    public class Comments
    {
        [Key]
        public Guid IdComment { get; set; }

        public string Message { get; set; }

        public DateTime DateCreated { get; set; }

        public bool Status { get; set; }

        //[ForeignKey("UserId")]
        public User User { get; set; }

        //[ForeignKey("ProductId")]
        public Products Products { get; set; }
    }
}

