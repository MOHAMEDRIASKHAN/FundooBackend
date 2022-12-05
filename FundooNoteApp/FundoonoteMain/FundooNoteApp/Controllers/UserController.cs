using BusinessLayer.Interfaces;
using BusinessLayer.Services;
using CommonLayer;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using RespositryLayer.Context;
using System.Runtime.CompilerServices;
using System.Security.Claims;

namespace FundooNoteApp.Controllers
{
    //controller
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        IUserBN userBN;
        FundooDBContext fundoo;

        public UserController(IUserBN userBN, FundooDBContext fundoo)
        {
            this.userBN = userBN;
            this.fundoo = fundoo;
        }
        [HttpPost("register")]
        public ActionResult RegisterUser(UserPostModel userDetail)
        {
            try
            {
                var result = this.userBN.RegUser(userDetail);
                if (result != null)
                {
                    return this.Ok(new { success = true, Message = $"Register Successful {result}" });
                }
                return this.BadRequest(new { success = false, Message = $"Register Failed" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost("Login/{email}/{password}")]
        public ActionResult LoginUser(string email, string password)
        {
            try
            {
                var result = this.userBN.LoginUser(email, password);
                if (result != null)
                {
                    return Ok(new { success = true, Message = $"Login Success{result}" });
                }
                return BadRequest(new { success = false, Message = $"Login Failed" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("ForgetPassword")]
        public ActionResult ForgetPassword(string email)
        {
            try
            {
                var result = this.userBN.ForgetPassword(email);
                if (result != null)
                {
                    return this.Ok(new { success = true, Message = $"ForgetPassword Success" });
                }
                return this.BadRequest(new { success = false, Message = $"ForgetPassword can not Work" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [Route("UpdatePassword")]
        public ActionResult UpdatePassword(String email, PasswordValidation valid)
        {
            try
            {
                //var mail = User.FindFirst(ClaimTypes.Email).Value.ToString();
                var result = userBN.UpdatePassword(email, valid );
                if (result != false)
                {
                    return Ok(new { success = true, message = "Password Reset Successfully" });
                }
                return this.BadRequest(new { success = false, Message = $"ResetPassword is Failed" });
            }
            catch (Exception ex)
            {
                 throw ex;
            }
        }

    }
}
