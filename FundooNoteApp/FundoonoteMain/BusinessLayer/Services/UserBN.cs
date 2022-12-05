using BusinessLayer.Interfaces;
using CommonLayer;
using CommonLayer.Model;
using RespositryLayer.Entity;
using RespositryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class UserBN : IUserBN
    {
        private readonly IUserRepo userRepo;
        public UserBN(IUserRepo userRepo)
        {
            this.userRepo = userRepo;
        }

        public UserTable RegUser(UserPostModel userDetail)
        {
            try
            {
                return this.userRepo.RegUser(userDetail);
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
                return userRepo.LoginUser(email, password);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string ForgetPassword(String email)
        {
            try
            {
                return userRepo.ForgetPassword(email);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool UpdatePassword(string email, PasswordValidation valid)
        {
            try
            {
                return userRepo.UpdatePassword(email, valid);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
