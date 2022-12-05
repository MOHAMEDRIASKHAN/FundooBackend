using CommonLayer;
using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RespositryLayer.Interface
{
    public interface IUserRepo
    {
        public Entity.UserTable RegUser(UserPostModel userDetail);
        public string LoginUser(string email, string password);
        public string ForgetPassword(string email);
        public bool UpdatePassword(string email, PasswordValidation valid);
    }
}
