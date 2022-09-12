using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentAppApi.Tables
{
    public class SubCategory
    {
        [Key]
        public Guid SubCategoryId { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        public string Icon { get; set; }

        public string FontFamily { get; set; }

        public string Color { get; set; }

        public bool Status { get; set; }

        public DateTime DateCreated { get; set; }

        //[ForeignKey("CategoryId")]
        public Category Category { get; set; }
    }
}

