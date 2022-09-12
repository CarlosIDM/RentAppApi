using System;
using RentAppApi.Tables;

namespace RentAppApi.Models
{
    public class UserInsert
    {
        public byte[] Image { get; set; }
        public User User { get; set; }
    }
}

