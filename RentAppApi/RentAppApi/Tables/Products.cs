using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentAppApi.Tables
{
    [Table("Product")]
    public class Products
    {
        [Key]
        public Guid ProductId { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public double? Calification { get; set; }

        public string Bedrooms { get; set; }

        public string Bathrooms { get; set; }

        public bool Garage { get; set; }

        public string TotalPerson { get; set; }

        public double Metters { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateUpdated { get; set; }

        public bool IsRentSale { get; set; }

        public bool IsDiscount { get; set; }

        public double? Percentage { get; set; }

        public bool Status { get; set; }

        //[ForeignKey("SubCategoryId")]
        public SubCategory SubCategory { get; set; }

        //[ForeignKey("UserId")]
        public User User { get; set; }
    }
}

