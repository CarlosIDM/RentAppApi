using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentAppApi.Tables
{
    public class User
    {
        [Key]
        public Guid UserId { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(100)]
        public string LastName { get; set; }

        [StringLength(20)]
        public string Phone { get; set; }

        [StringLength(200)]
        public string Address { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        public string Password { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public bool IsClient { get; set; } = true;

        public bool IsProveedor { get; set; }

        public string Url { get; set; }

        [StringLength(100)]
        public string TypeRegister { get; set; }

        public bool Status { get; set; }

        public bool ValidateCode { get; set; }

        [NotMapped]
        public string Validate { get; set; } = "SUCCESS";

        [NotMapped]
        public string Code { get; set; }
    }
}

