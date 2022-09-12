using System;
namespace RentAppApi.Models
{
    public class ImageProductModel
    {
        public Guid IdProduct { get; set; }
        public byte[] Image { get; set; }
    }
}

