using System;
namespace RentAppApi.ViewModels
{
    public class UserCommentVM
    {
        public Guid IdUser { get; set; }
        public string Comment { get; set; }
        public DateTime Date { get; set; }
        public string NameUser { get; set; }
        public string Image { get; set; }
    }
}

