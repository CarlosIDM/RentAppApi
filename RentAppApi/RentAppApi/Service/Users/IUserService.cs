using RentAppApi.Helpers;
using RentAppApi.Tables;

namespace RentAppApi.Service.Users
{
    public interface IUserService
    {
        Task<Response<User>> InsertUser(User user);
        Task<Response<User>> SelectUser(string email, string password);
        Task<Response<User>> UpdateUser(User user);
        Task<Response<User>> DeleteUser(User user);
        Task<Response<User>> ForgotPassword(Guid idUser, string email);
        Task<Response<User>> ForgotPassword(Guid idUser, string email, string code, string password);
        Task<Response<User>> UpdateNotification(Guid idUser, string pushToken);
    }
}

