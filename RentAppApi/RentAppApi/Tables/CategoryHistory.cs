using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentAppApi.Tables
{
    public class CategoryHistory
    {
        [Key]
        public Guid Id { get; set; }

        [StringLength(50)]
        public string Action { get; set; }

        public DateTime DateCreated { get; set; }

        //[ForeignKey("CategoryId")]
        public Category Category { get; set; }
    }
}

