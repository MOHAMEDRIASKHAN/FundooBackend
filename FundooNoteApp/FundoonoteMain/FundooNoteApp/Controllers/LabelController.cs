using BusinessLayer.Interfaces;
using CommonLayer.Model;
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
    public class LabelController : ControllerBase
    {
        ILabelBN labelBN;
        FundooDBContext FundooDBContext;
        private readonly IMemoryCache memoryCache;
        private readonly IDistributedCache distributedCache;
        private string keyName = "khan";
        private readonly ILogger<LabelController> logger;

        public LabelController(ILabelBN labelBN, FundooDBContext fundooDBContext, IMemoryCache memoryCache, IDistributedCache distributedCache, ILogger<LabelController> logger)
        {
            this.labelBN = labelBN;
            FundooDBContext = fundooDBContext;
            this.memoryCache = memoryCache;
            this.distributedCache = distributedCache;
            this.logger = logger;
        }

        [HttpPost]
        [Route("CreateLabel")]

        public ActionResult CreateLabel(long noteID, string label)
        {
            try
            {
                long userID = Convert.ToInt32(User.Claims.FirstOrDefault(a => a.Type == "UserID").Value);
                var result = this.labelBN.CreateLabel(userID,noteID,label);
                if(result != null)
                {
                    logger.LogInformation("Create LabelNotes Successfully");
                    return this.Ok(new { success = true, message = "Create LabelNotes Successfuly", });
                }
                else
                {
                    logger.LogWarning("Created labelnotes Failed");
                    return this.BadRequest(new { success = false, message = "LabelNotes can`t Created" });
                }
            }
            catch(Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("GetLabelNotes")]
        public ActionResult GetLabels(long noteID)
        {
            try
            {
                long userID = Convert.ToInt32(User.Claims.FirstOrDefault(a => a.Type == "UserID").Value);
                List<LabelTable> result = this.labelBN.GetLabels(noteID, userID);
                if (result != null)
                {
                    logger.LogInformation("Display LabelNotes Successfully");
                    return this.Ok(new { success = true, message = "Note is Success", Data = result });
                }
                else
                {
                    logger.LogWarning("Display LabelNotes failed");
                    return this.BadRequest(new { success = true, message = "Note not Created" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpDelete("DeleteLabelNotes")]
        public ActionResult RemoveLabel( long labelID)
        {
            try
            {
                long userID = Convert.ToInt32(User.Claims.FirstOrDefault(a => a.Type == "UserID").Value);
                var result = this.labelBN.RemoveLabel( userID,labelID);
                if (result != null)
                {
                    logger.LogInformation("Delete LabelNotes successfully");
                    return this.Ok(new { success = true, message = "DeleteNotes successfully" });
                }
                else
                {
                    logger.LogWarning("Delete LabelNotes Failed");
                    return this.BadRequest(new { success = false, message = "DeleteNotes Failed" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPut("RenameLabelNotes")]
        public ActionResult RenameLabel(string oldLabelName, string newLabelName)
        {
            try
            {
                long userID = Convert.ToInt32(User.Claims.FirstOrDefault(a => a.Type == "UserID").Value);
                var result = this.labelBN.RenameLabel(userID, oldLabelName, newLabelName);
                if (result != null)
                {
                    logger.LogInformation("Rename LabelNotes successfully");
                    return this.Ok(new { success = true, message = "RenameLabelNotes Successfully" });
                }
                else
                {
                    logger.LogWarning("Rename LablesNotes Failed");
                    return this.BadRequest(new { success = false, message = "RenameLabelNotes Failed" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet]
        [Route("GetLabelNotesByRedis")]
        public async Task<ActionResult> GetAllLableNotesByRadisCache()   //Controller
        {
            try
            {
                string serializeLabelList;
                var labellist = new List<LabelTable>();
                var redisLableList = await distributedCache.GetAsync(keyName);
                if (redisLableList != null)
                {
                    serializeLabelList = Encoding.UTF8.GetString(redisLableList);
                    labellist = JsonConvert.DeserializeObject<List<LabelTable>>(serializeLabelList);
                }
                else
                {
                    labellist = await this.labelBN.GetAllLableNotesByRadisCache();
                    serializeLabelList = JsonConvert.SerializeObject(labellist);
                    redisLableList = Encoding.UTF8.GetBytes(serializeLabelList);
                    var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                    await distributedCache.SetAsync(keyName, redisLableList, options);
                }
                return this.Ok(new { success = true, message = "LabelList is Success", Data = labellist });
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
