using BusinessLayer.Interfaces;
using BusinessLayer.Services;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using RespositryLayer.Context;
using RespositryLayer.Entity;
using StackExchange.Redis;
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
        private readonly ILogger<UserController> logger;

        public UserController(IUserBN userBN, FundooDBContext fundoo, ILogger<UserController> logger)
        {
            this.userBN = userBN;
            this.fundoo = fundoo;
            this.logger = logger;
        }
        [HttpPost("register")]     //Name for the particular method in request url//
        public ActionResult RegisterUser(UserPostModel userDetail)
        {
            try
            {
                var result = this.userBN.RegUser(userDetail);
                if (result != null)
                {
                    logger.LogInformation("Register Successfully");
                    return this.Ok(new { success = true, Message = $"Register Successful {result}" });
                }
                logger.LogWarning("Register Failed Try again");
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
                    ConnectionMultiplexer connectionMultiplexer = ConnectionMultiplexer.Connect("127.0.0.1:6379");
                    IDatabase database = connectionMultiplexer.GetDatabase();
                    string FirstName = database.StringGet("FirstName");
                    string LastName = database.StringGet("LastName");
                    long usedID = Convert.ToInt32(database.StringGet("UserID"));
                    this.logger.LogInformation(FirstName + " is loggerIn");
                    UserTable userData = new UserTable
                    {
                        FirstName = FirstName,
                        LastName = LastName,
                        UserID = usedID,
                        EmailID = email,
                    };
                    logger.LogInformation("Login Successfully");
                    return Ok(new { success = true, Message = "Login Success",token =result });
                }
                
                logger.LogWarning("Login Unsuccessfully");
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
                    logger.LogInformation("Email sent Successfully");
                    return this.Ok(new { success = true, Message = $"ForgetPassword Success" });
                }
                logger.LogWarning("Email not matched");
                return this.BadRequest(new { success = false, Message = $"ForgetPassword can not Work" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [Authorize]
        [HttpPut]
        [Route("UpdatePassword")]
        public ActionResult UpdatePassword(string Password, string ConfirmPassword)
        {
            try
            {
                // var email = User.FindFirst(ClaimTypes.Email).Value.ToString();
                string email = (User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.Email).Value);
                var result = userBN.UpdatePassword(email,Password,ConfirmPassword );
                if (result != false)
                {
                    logger.LogInformation("Reset Password Successfully");
                    return Ok(new { success = true, message = "Password Reset Successfully" });
                }
                logger.LogWarning("Email not found ");
                return this.BadRequest(new { success = false, Message = $"ResetPassword is Failed" });
            }
            catch (Exception ex)
            {
                 throw ex;
            }
        }

    }
}
