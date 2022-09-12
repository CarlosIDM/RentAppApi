using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentAppApi.Tables
{
    public class ImageProduct
    {
        [Key]
        public Guid IdImageProduct { get; set; }

        public string Image { get; set; }

        //[ForeignKey("ProductId")]
        public Products Products { get; set; }
    }
}

