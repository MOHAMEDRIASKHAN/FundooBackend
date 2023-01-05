using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using RespositryLayer.Context;
using RespositryLayer.Entity;
using System.Text;

namespace FundooNoteApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CollabController : ControllerBase
    {
        ICollabBN collabBN;
        FundooDBContext FundooDBContext;
        private readonly IMemoryCache memoryCache;
        private readonly IDistributedCache distributedCache;
        private string keyName = "collab";
        private readonly Logger<CollabController> logger;
        public CollabController(ICollabBN collabBN, FundooDBContext fundooDBContext, Logger<CollabController> logger, IMemoryCache memoryCache,IDistributedCache distributedCache)
        {
            this.collabBN = collabBN;
            FundooDBContext = fundooDBContext;
            this.logger = logger;
            this.memoryCache = memoryCache;
            this.distributedCache = distributedCache;
        }

        [HttpPost]
        [Route("CreateCollab")]
        public ActionResult CreateCollab(long noteID,string CollabEmailID)
        {
            try
            {
                long userID = Convert.ToInt32(User.Claims.FirstOrDefault(a => a.Type == "UserID").Value);
                var result = collabBN.CreateCollab(userID,noteID,CollabEmailID);
                if(result != null)
                {
                    logger.LogInformation("Create Collab Tables successfully");
                    return this.Ok(new { success = true, message = "Collab will be Created"});
                }
                logger.LogWarning("Create Collab Tables failed");
                return this.BadRequest(new { success = false, message = "Collab will not be Created" });
            }
            catch(Exception)
            {
                throw;
            }
        }
        [HttpGet]
        [Route("GetCollabNotes")]
        public ActionResult GetCollabNotes(long noteID)
        {
            try
            {
                long userID = Convert.ToInt32(User.Claims.FirstOrDefault(a => a.Type == "UserID").Value);
                var result =collabBN.GetCollabNotes(userID,noteID);
                if(result !=null)
                {
                    logger.LogInformation("Display CollabNotes successfully");
                    return this.Ok(new { success = true, message = "GetCollabNotes is successfully",Data = result });
                }
                logger.LogWarning("Display CollabNotes failed");
                return this.BadRequest(new { success = false, message = "GetCollabNotes is failed" });
            }
            catch(Exception)
            {
                throw;
            }
        }
        [HttpDelete]
        [Route("DeleteCollab")]
        public ActionResult DeleteCollab(long collabID)
        {
            try
            {
                long userID = Convert.ToInt32(User.Claims.FirstOrDefault(a => a.Type == "UserID").Value);
                var result = collabBN.DeleteCollab(userID,collabID);
                if(result != null)
                {
                    logger.LogInformation("Delete Notes Successfully");
                    return this.Ok(new { success = true, message = "DeleteCollab is successfully" });
                }
                logger.LogWarning("Delete Notes failed");
                return this.BadRequest(new { success = false, message = "DeleteCollab is failed" });
            }
            catch(Exception)
            {
                throw;
            }
        }
        [HttpGet]
        [Route("GetCollabNotesByRedis")]
        public async Task<ActionResult> GetAllCollabNotesByRadisCache()   //Controller
        {
            try
            {
                string serializeCollabList;
                var collablist = new List<CollabTable>();
                var rediscollabList = await distributedCache.GetAsync(keyName);
                if (rediscollabList != null)
                {
                    serializeCollabList = Encoding.UTF8.GetString(rediscollabList);
                    collablist = JsonConvert.DeserializeObject<List<CollabTable>>(serializeCollabList);
                }
                else
                {
                    collablist = await this.collabBN.GetAllCollabNotesByRadisCache();
                    serializeCollabList = JsonConvert.SerializeObject(collablist);
                    rediscollabList = Encoding.UTF8.GetBytes(serializeCollabList);
                    var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                    await distributedCache.SetAsync(keyName, rediscollabList, options);
                }
                return this.Ok(new { success = true, message = "CollabNote is Success", Data = collablist });
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
