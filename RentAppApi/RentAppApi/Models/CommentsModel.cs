using System;
namespace RentAppApi.Models
{
    public class CommentsModel
    {
        public string Comment { get; set; }
        public Guid IdUser { get; set; }
        public Guid IdProduct { get; set; }
    }
}

