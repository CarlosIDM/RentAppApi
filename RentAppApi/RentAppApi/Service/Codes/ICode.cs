using System;
using RentAppApi.Helpers;
using RentAppApi.Tables;

namespace RentAppApi.Service.Codes
{
    public interface ICode
    {
        Task<Response<User>> SelectCode(string code, string email, string phone);
        Task<Response<User>> UpdateCode(string email, string phone);
    }
}

