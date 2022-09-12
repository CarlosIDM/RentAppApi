using System;
namespace RentAppApi.Models
{
    public class NotificationModel
    {
        public string PushToken { get; set; }
        public Guid IdUser { get; set; }
    }
}

