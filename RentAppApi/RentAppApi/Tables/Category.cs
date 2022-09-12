using System;
using System.ComponentModel.DataAnnotations;

namespace RentAppApi.Tables
{
    public class Category
    {
        [Key]
        public Guid CategoryId { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        public string Icon { get; set; }

        public string FontFamily { get; set; }

        public string Color { get; set; }

        public bool Status { get; set; }

        public DateTime DateCreated { get; set; }
    }
}

