using System.Numerics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RentAppApi.DataBases;
using RentAppApi.Helpers;
using RentAppApi.Tables;

namespace RentAppApi.Service.Users
{
    public class UserService : IUserService
    {
        private readonly RentAppDbContext _context;
        private readonly AppSettings _appSettings;
        private readonly IEmailService emailService;

        public UserService(RentAppDbContext context, IOptions<AppSettings> appSettings, IEmailService emailService)
        {
            this.emailService = emailService;
            _context = context;
            _appSettings = appSettings.Value;
        }

        public async Task<Response<User>> InsertUser(User user)
        {
            using var transaction = _context.Database.BeginTransaction();

            var search = _context.User.SingleOrDefault(c => c.Email == user.Email && c.Phone == user.Phone);
            if(search != null)
            {
                //SE ACTUALIZA
                var password = BCrypt.Net.BCrypt.HashPassword(user.Password);
                user.Password = password;
                user.ValidateCode = false;
                user.Status = true;
                var us = _context.User.Update(user);
                _context.SaveChanges();

                _context.UserHistory.Add(new UserHistory()
                {
                    Action = ValueSQL.UPDATE,
                    DateCreated = DateTime.UtcNow,
                    User = us.Entity
                });
                _context.SaveChanges();

                var guid = Guid.NewGuid().ToString();
                var code = guid.Substring(0, guid.IndexOf('-'));
                var codeIs = _context.Code.Add(new Code
                {
                    DateCreated = DateTime.UtcNow,
                    Email = user.Email,
                    Phone = user.Phone,
                    ValidateCode = code,
                });
                _context.SaveChanges();

                _context.CodeHistory.Add(new CodeHistory
                {
                    DateCreated = DateTime.UtcNow,
                    Action = ValueSQL.INSERT,
                    Code = codeIs.Entity
                });
                _context.SaveChanges();
                transaction.Commit();
                await emailService.Send(user.Email, "Envio de codigo", code, TypeEmail.Code);
                var response = new Response<User>()
                {
                    Count = 1,
                    Result = user,
                    Message = String.Empty
                };
                return response;
            }
            else
            {
                var password = BCrypt.Net.BCrypt.HashPassword(user.Password);
                user.Password = password;
                user.ValidateCode = false;
                user.Status = true;
                var us = await _context.User.AddAsync(user);
                _context.SaveChanges();

                _context.UserHistory.Add(new UserHistory()
                {
                    Action = ValueSQL.INSERT,
                    DateCreated = DateTime.UtcNow,
                    User = us.Entity
                });
                _context.SaveChanges();

                var guid = Guid.NewGuid().ToString();
                var code = guid.Substring(0, guid.IndexOf('-'));
                var codeIs = _context.Code.Add(new Code
                {
                    DateCreated = DateTime.UtcNow,
                    Email = user.Email,
                    Phone = user.Phone,
                    ValidateCode = code,
                });
                _context.SaveChanges();

                //emailService.Send(user.Email);
                _context.CodeHistory.Add(new CodeHistory
                {
                    DateCreated = DateTime.UtcNow,
                    Action = ValueSQL.INSERT,
                    Code = codeIs.Entity
                });
                _context.SaveChanges();

                transaction.Commit();
                var response = new Response<User>()
                {
                    Count = 1,
                    Result = user,
                    Message = String.Empty
                };
                return response;
            }
        }

        public async Task<Response<User>> SelectUser(string email, string password)
        {
            using var transaction = _context.Database.BeginTransaction();
            var response = new Response<User>();
            var user = _context.User.SingleOrDefault(c => c.Email == email);        
            if (user != null)
            {
                var validatePassword = BCrypt.Net.BCrypt.Verify(password, user.Password);
                if (validatePassword)
                {
                    if(!user.ValidateCode)
                    {
                        //emailService.Send(user.Email);
                        user.Validate = "ERROR";
                        response.Count = 0;
                        response.Message = "La cuenta aun no ha sido verificada, se ha enviado un correo con el código";
                        response.Result = user;
                        return response;
                    }
                    else
                    {
                        _context.UserHistory.Add(new UserHistory()
                        {
                            Action = ValueSQL.SELECT,
                            DateCreated = DateTime.UtcNow,
                            User = user
                        });
                        _context.SaveChanges();

                        transaction.Commit();
                        //SI HAY DATOS
                        response.Count = 1;
                        response.Message = string.Empty;
                        response.Result = user;
                        return response;
                    }                  
                }
                else
                {
                    //CONTRASEÑA NO COINCIDE
                    response.Count = 0;
                    response.Message = "La contraseña no coincide";
                    response.Result = null;
                    return response;
                }
            }
            else
            {
                response.Count = 0;
                response.Message = "El correo no existe";
                response.Result = null;
                return response;
            }
        }

        public async Task<Response<User>> UpdateUser(User user)
        {
            using var transaction = _context.Database.BeginTransaction();
            var response = new Response<User>();

            var password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            user.Password = password;
            var up = _context.User.Update(user);
            _context.SaveChanges();

            _context.UserHistory.Add(new UserHistory()
            {
                Action = ValueSQL.UPDATE,
                DateCreated = DateTime.UtcNow,
                User = up.Entity
            });
            _context.SaveChanges();
            transaction.Commit();
            response.Count = 1;
            response.Message = String.Empty;
            response.Result = up.Entity;
            return response;
        }

        public async Task<Response<User>> DeleteUser(User user)
        {
            using var transaction = _context.Database.BeginTransaction();
            var response = new Response<User>();
            user.Status = false;
            var up = _context.User.Update(user);
            _context.SaveChanges();

            _context.UserHistory.Add(new UserHistory()
            {
                Action = ValueSQL.DELETE,
                DateCreated = DateTime.UtcNow,
                User = up.Entity,
                Id = Guid.NewGuid()
            });
            _context.SaveChanges();
            transaction.Commit();
            response.Count = 1;
            response.Message = String.Empty;
            response.Result = up.Entity;
            return response;
        }

        public async Task<Response<User>> ForgotPassword(Guid idUser, string email)
        {
            using var transacction = _context.Database.BeginTransaction();
            var response = new Response<User>();
            try
            {
                var userSearch = _context.User.SingleOrDefault(c => c.UserId == idUser && c.Email == email);
                if (userSearch != null)
                {
                    var guid = Guid.NewGuid().ToString();
                    var code = guid.Substring(0, guid.IndexOf('-'));

                    //SE ENVIA CORREO PARA LA NUEVA CONTRASEÑA
                    var forgotSearch = await _context.ForgotPassword.SingleOrDefaultAsync(c => c.User.UserId == userSearch.UserId);
                    if (forgotSearch != null)
                    {
                        //SE ACTUALIZA EL DATO ANTERIOR
                        forgotSearch.Code = code;
                        _context.ForgotPassword.Update(forgotSearch);
                        _context.SaveChanges();
                        await emailService.Send(userSearch.Email, "\"Envia nuevo codigo de contraseña\"", code, TypeEmail.ForgotPassword);
                    }
                    else
                    {
                        //SE REGISTRA UN NUEVO CODIGO
                        _context.ForgotPassword.Add(new Tables.ForgotPassword
                        {
                            Code = code,
                            User = userSearch
                        });
                        _context.SaveChanges();
                        await emailService.Send(userSearch.Email, "Envia nuevo codigo de contraseña", code, TypeEmail.ForgotPassword);
                    }
                    transacction.Commit();
                    response.Count = 1;
                    response.Message = "Se envio un codigo a su correo";
                    response.Result = userSearch;
                    return response;
                }
                else
                {
                    response.Count = 0;
                    response.Message = "El correo que proporcionó no existe";
                    response.Result = null;
                    return response;
                }
            }
            catch (Exception ex)
            {
                response.Count = 0;
                response.Message = ex.StackTrace;
                response.Result = null;
                return response;
            }
        }

        public async Task<Response<User>> ForgotPassword(Guid idUser, string email, string code, string password)
        {
            using var transacction = _context.Database.BeginTransaction();
            var response = new Response<User>();
            try
            {
                var userSearch = _context.User.SingleOrDefault(c => c.UserId == idUser && c.Email == email);
                if (userSearch != null)
                {
                    var forgotSearch = await _context.ForgotPassword.SingleOrDefaultAsync(c => c.User.UserId == userSearch.UserId && c.Code == code);
                    if (forgotSearch != null)
                    {
                        //SE ACTUALIZA contraseña
                        userSearch.Password = BCrypt.Net.BCrypt.HashPassword(userSearch.Password);
                        var upUser = _context.User.Update(userSearch);
                        _context.SaveChanges();
                        response.Count = 1;
                        response.Message = string.Empty;
                        response.Result = upUser.Entity;
                    }
                    else
                    {
                        response.Count = 0;
                        response.Message = "No se encontro el codigo ingresado";
                        response.Result = null;
                    }
                    transacction.Commit();
                    return response;
                }
                else
                {
                    response.Count = 0;
                    response.Message = "El correo que proporcionó no existe";
                    response.Result = null;
                    return response;
                }
            }
            catch (Exception ex)
            {
                response.Count = 0;
                response.Message = ex.StackTrace;
                response.Result = null;
                return response;
            }
        }

        public async Task<Response<User>> UpdateNotification(Guid idUser, string pushToken)
        {
            var response = new Response<User>();
            try
            {
                var userSearch = _context.User.SingleOrDefault(c => c.UserId == idUser);
                if (userSearch != null)
                {
                    var notiSearch = _context.Notifications.SingleOrDefault(c => c.User.UserId == idUser);
                    if (notiSearch != null)
                    {
                        //SE ACTUALIZA
                        notiSearch.Token = pushToken;
                        notiSearch.DateUpdated = DateTime.UtcNow;
                        _context.Notifications.Update(notiSearch);
                        response.Count = 1;
                        response.Message = "Se actualizo correctamente el token";
                        response.Result = userSearch;
                    }
                    else
                    {
                        _context.Notifications.Add(new Notifications
                        {
                            DateUpdated = DateTime.UtcNow,
                            DateCreated = DateTime.UtcNow,
                            Token = pushToken,
                            User = userSearch
                        });
                        response.Count = 1;
                        response.Message = "Se registro correctamente el token";
                        response.Result = userSearch;
                    }
                    return response;
                }
                else
                {
                    response.Count = 0;
                    response.Message = "Usuario no encontrado";
                    response.Result = null;
                    return response;
                }
            }
            catch (Exception ex)
            {
                response.Count = 0;
                response.Message = ex.StackTrace;
                response.Result = null;
                return response;
            }
        }
    }
}

