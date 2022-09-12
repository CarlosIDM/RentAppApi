using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentAppApi.Tables
{
    public class SubCategoryHistory
    {
        [Key]
        public Guid Id { get; set; }

        [StringLength(50)]
        public string Action { get; set; }

        public DateTime DateCreated { get; set; }

        //[ForeignKey("SubCategoryId")]
        public SubCategory SubCategory { get; set; }
    }
}

