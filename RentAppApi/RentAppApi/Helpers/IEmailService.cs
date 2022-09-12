using System;
namespace RentAppApi.Helpers
{
    public interface IEmailService
    {
        Task Send(string mail, string subject, string data, TypeEmail typeEmail);
    }

    public enum TypeEmail
    {
        ForgotPassword,
        Code
    }
}

