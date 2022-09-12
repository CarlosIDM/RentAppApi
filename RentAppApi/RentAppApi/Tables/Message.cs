using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentAppApi.Tables
{
    public class Message
    {
        [Key]
        public Guid IdMessage { get; set; }

        public string Text { get; set; }

        public bool InComming { get; set; }

        public bool Outgoing { get; set; }

        public DateTime DateCreated { get; set; }

        public Guid IdUserClient { get; set; }

        public Guid IdUserProveedor { get; set; }

        //[ForeignKey("ProductId")]
        public Products Products { get; set; }
    }
}

