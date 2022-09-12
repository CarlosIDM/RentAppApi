using System;
namespace RentAppApi.Models
{
    public class CalificationModel
    {
        public Guid IdProduct { get; set; }
        public double Calification { get; set; }
        public Guid IdUser { get; set; }
    }
}

