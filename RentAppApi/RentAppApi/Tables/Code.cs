using System;
using System.ComponentModel.DataAnnotations;

namespace RentAppApi.Tables
{
    public class Code
    {
        [Key]
        public Guid CodeId { get; set; }

        [StringLength(20)]
        public string Phone { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(10)]
        public string ValidateCode { get; set; }

        public DateTime DateCreated { get; set; }
    }
}

