using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentAppApi.Tables
{
    public class Favorites
    {
        [Key]
        public Guid IdFavorite { get; set; }

        //[ForeignKey("UserId")]
        public User User { get; set; }

        //[ForeignKey("ProductId")]
        public Products Products { get; set; }

        public DateTime DateCreated { get; set; }

        public bool Status { get; set; }
    }
}

