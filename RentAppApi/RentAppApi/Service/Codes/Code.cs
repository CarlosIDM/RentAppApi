using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RentAppApi.DataBases;
using RentAppApi.Helpers;
using RentAppApi.Tables;

namespace RentAppApi.Service.Codes
{
    public class Code : ICode
    {
        private readonly RentAppDbContext _context;
        private readonly AppSettings _appSettings;
        private readonly IEmailService emailService;
        public Code(RentAppDbContext context, IOptions<AppSettings> appSettings, IEmailService emailService)
        {
            this.emailService = emailService;
            _context = context;
            _appSettings = appSettings.Value;
        }

        public async Task<Response<User>> SelectCode(string code,string email,string phone)
        {
            try
            {
                using var transaction = _context.Database.BeginTransaction();
                var codeSearch = _context.Code.SingleOrDefault(c => c.ValidateCode == code && c.Phone == phone && c.Email == email);
                if(codeSearch != null)
                {
                    //SE ACTUALIZA EL USUARIO
                    var user = await _context.User.SingleOrDefaultAsync(c => c.Email == email && c.Phone == phone);
                    user.ValidateCode = true;
                    var us = _context.User.Update(user);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                    var response = new Response<User>()
                    {
                        Count = 1,
                        Result = us.Entity,
                        Message = string.Empty
                    };
                    return response;
                    
                }
                else
                {
                    var response = new Response<User>()
                    {
                        Count = 0,
                        Result = null,
                        Message = "El código no es correcto"
                    };
                    return response;
                }
            }
            catch(Exception ex)
            {
                var response = new Response<User>()
                {
                    Count = 0,
                    Result = null,
                    Message = ex.StackTrace
                };
                return response;
            }
        }

        public async Task<Response<User>> UpdateCode(string email, string phone)
        {
            try
            {
                using var transaction = _context.Database.BeginTransaction();
                var codeSearch = await _context.Code.SingleOrDefaultAsync(c => c.Phone == phone && c.Email == email);
                if (codeSearch != null)
                {
                    var guid = Guid.NewGuid().ToString();
                    var code = guid.Substring(0, guid.IndexOf('-'));
                    codeSearch.ValidateCode = code;
                    var codeIs = _context.Code.Update(codeSearch);
                    await _context.SaveChangesAsync();

                    var user = await _context.User.SingleOrDefaultAsync(c => c.Email == email && c.Phone == phone);
                    if (user != null)
                        await emailService.Send(user.Email,"Cambio de Codigo",code,TypeEmail.Code);
                    await transaction.CommitAsync();
                    var response = new Response<User>()
                    {
                        Count = 1,
                        Result = user,
                        Message = "Se ha enviado el nuevo codigo"
                    };
                    return response;
                }
                else
                {
                    var response = new Response<User>()
                    {
                        Count = 0,
                        Result = null,
                        Message = "El código no es correcto"
                    };
                    return response;
                }
            }
            catch (Exception ex)
            {
                var response = new Response<User>()
                {
                    Count = 0,
                    Result = null,
                    Message = ex.StackTrace
                };
                return response;
            }
        }
    }
}

