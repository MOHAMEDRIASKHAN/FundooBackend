using CommonLayer.Model;
using RespositryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IUserBN
    {
        //iuserBn
        public UserTable RegUser(UserPostModel userDetail);
        public string LoginUser(string email, string password);
        public string ForgetPassword(string email);
        public bool UpdatePassword(String email, string Password, string ConfirmPassword);
    }
}
