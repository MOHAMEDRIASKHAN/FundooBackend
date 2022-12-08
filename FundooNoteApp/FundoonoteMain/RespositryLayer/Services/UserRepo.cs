using CommonLayer;
using CommonLayer.Model;
using Experimental.System.Messaging;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RespositryLayer.Context;
using RespositryLayer.Entity;
using RespositryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RespositryLayer.Services
{
    //repo
    public class UserRepo : IUserRepo
    {
        FundooDBContext fundoo;
        private readonly IConfiguration configuration;
        public UserRepo(FundooDBContext fundoo, IConfiguration configuration)
        {
            this.fundoo = fundoo;
            this.configuration = configuration;
        }
        public Entity.UserTable RegUser(UserPostModel userDetail)
        {
            try
            {
                Entity.UserTable usertable = new Entity.UserTable();
                usertable.UserID = new Entity.UserTable().UserID;
                usertable.FirstName = userDetail.FirstName;
                usertable.LastName = userDetail.LastName;
                usertable.EmailID = userDetail.EmailID;
                usertable.Password = userDetail.Password;


                fundoo.UserDetailTables.Add(usertable);
                fundoo.SaveChanges();
                return usertable;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public string LoginUser(string email, string password)
        {
            try
            {
                var result = fundoo.UserDetailTables.Where(u => u.EmailID == email && u.Password == password).FirstOrDefault();
                if(result != null)
                {
                    
                    return GetJWTToken(email, result.UserID);
                }
                return null;
                 //String password = password//
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        //Generate JWT Token

        private  string GetJWTToken(string email, long UserID)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(this.configuration[("Jwt:key")]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email,email),
                    new Claim("UserID",UserID.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(6),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        public static string EncryptPassword(String password)
        {
            try
            {
                if(string.IsNullOrEmpty(password))
                {
                    return null;
                }
                else
                {
                    byte[] b = Encoding.ASCII.GetBytes(password);
                    string encrypted = Convert.ToBase64String(b);
                    return encrypted;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public static string DecryptedPassword(string encryptedPassword)
        {
            byte[] b;
            string decrypted;
            try
            {
                if(string.IsNullOrEmpty(encryptedPassword))
                {
                    return null;
                }
                else
                {
                    b=Convert.FromBase64String(encryptedPassword);
                    decrypted = Encoding.ASCII.GetString(b);
                    return decrypted;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string ForgetPassword(string email)
        {
            try
            {
                var CheckEmail = fundoo.UserDetailTables.FirstOrDefault(e => e.EmailID == email);
                if (CheckEmail != null)
                {
                    var Token =  GetJWTToken(CheckEmail.EmailID, CheckEmail.UserID);
                    MSMQModel msmqModel = new MSMQModel();
                    msmqModel.sendDatatoQueue(Token);
                    return Token.ToString();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
      
        public bool UpdatePassword(String email, PasswordValidation valid)
        {
            try
            {
                if(valid.Password.Equals(valid.ConfirmPassword))
                {
                    var user = fundoo.UserDetailTables.Where(x => x.EmailID == email).FirstOrDefault();
                    user.Password = EncryptPassword(valid.ConfirmPassword);
                    fundoo.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
