using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore;

namespace RentAppApi.Tables
{
    public class Califications
    {
        [Key]
        public Guid IdCalifications { get; set; }

        public double Calification { get; set; }

        public DateTime DateCreated { get; set; }

        //[ForeignKey("UserId")]
        public User User { get; set; }

        //[ForeignKey("ProductId")]
        public Products Products { get; set; }
    }
}

