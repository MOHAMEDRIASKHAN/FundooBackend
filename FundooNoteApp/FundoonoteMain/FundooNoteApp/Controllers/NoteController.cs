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
    public class NoteController : ControllerBase
    {
        INoteBN noteBN;
        FundooDBContext FundooDB;
        private readonly IMemoryCache memoryCache;
        private readonly IDistributedCache distributedCache;
        private string keyName = "riyas";
        private readonly ILogger<NoteController> logger;
        public NoteController(INoteBN noteBN, FundooDBContext FundooDB, ILogger<NoteController> logger, IDistributedCache distributedCache, IMemoryCache memoryCache)
        {
            this.noteBN = noteBN;
            this.FundooDB = FundooDB;
            this.memoryCache = memoryCache;
            this.distributedCache = distributedCache;
            this.logger = logger;
          
        }

        [HttpPost("CreateNotes")]
        public ActionResult CreateNotes(NotesModel notesModel)
        {
            try
            {
                // long userID = Convert.ToInt32(User.Claims.FirstOrDefault(a => a.Type == "UserID").Value);
                long userId = Convert.ToInt64(User.Claims.FirstOrDefault(x => x.Type == "UserID").Value);
                var result = this.noteBN.CreateNotes(notesModel, userId);
                if (result != null)
                {
                    logger.LogInformation("Note Created Successfully");
                    return this.Ok(new { success = true, message = "Create Notes Successfuly" });
                }
                else
                {
                    logger.LogWarning("Note Created Failed");
                    return this.BadRequest(new { success = false, message = "Notes can`t Created" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet("GetNotes")]
        public ActionResult GetNotes()
        {
            try
            {
                long userID = Convert.ToInt32(User.Claims.FirstOrDefault(a => a.Type == "UserID").Value);
                IEnumerable<NoteTable> result = this.noteBN.GetNote(userID);
                if (result != null)
                {
                    logger.LogInformation("Display notes Successfully");
                    return this.Ok(new { success = true, message = "Note is Success",Data = result });
                }
                else
                {
                    logger.LogWarning("Display Notes failed");
                    return this.BadRequest(new { success = true, message = "Note not Created" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpDelete("DeleteNotes")]
        public ActionResult DeleteNote( long noteID)
        {
            try
            {
                long userID = Convert.ToInt32(User.Claims.FirstOrDefault(a => a.Type == "UserID").Value);
                var result = this.noteBN.DeleteNote(userID, noteID);
                if (result != null)
                {
                    logger.LogInformation("Delete Notes successfuly");
                    return this.Ok(new { success = true, message = "DeleteNotes successfully" });
                }
                else
                {
                    logger.LogWarning("Delete notes failed");
                    return this.BadRequest(new { success = false, message = "DeleteNotes Failed" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPut("UpdateNotes")]
        public ActionResult UpdateNotes( long noteID, NotesModel notesModel)
        {
            try
            {
                long userID = Convert.ToInt32(User.Claims.FirstOrDefault(a => a.Type == "UserID").Value);
                var result = this.noteBN.UpdateNotes( userID, noteID, notesModel);
                if (result != null)
                {
                    logger.LogInformation("Update Notes Successfully");
                    return this.Ok(new { success = true, message = "UpdateNotes Successfully" });
                }
                else
                {
                    logger.LogWarning("Update Notes Failed");
                    return this.BadRequest(new { success = false, message = "UpdateNotes Failed" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPost("Color")]
        public ActionResult color(long noteID, string color)
        {
            try
            {
                long userID = Convert.ToInt32(User.Claims.FirstOrDefault(a => a.Type == "UserID").Value);
                var result = this.noteBN.color( noteID, color);
                if(result != null)
                {
                    logger.LogInformation("Color option will be work");
                    return this.Ok(new { success = true, message = "Color option will work",Data = result });
                }
                else
                {
                    logger.LogWarning("Color will not be work");
                    return this.BadRequest(new { success = false, message = "Color will not work" });
                }
            }
            catch(Exception)
            {
                throw;
            }
        }
        [HttpPut]
        [Route("PinNotes")]

        public ActionResult PinNotes(long noteID)
        {
            try
            {
                long userID = Convert.ToInt32(User.Claims.FirstOrDefault(a => a.Type == "UserID").Value);
                var result = this.noteBN.PinNotes(noteID);
                if(result != null)
                {
                    logger.LogInformation("PinNotes Successfully");
                    return this.Ok(new { success = true,  message =" PinNotes will work", Data = result });
                }
                else
                {
                    logger.LogWarning("PinNotes failed");
                    return this.BadRequest(new { success = false, message = "PinNotes does not Work" });
                }
            }
            catch(Exception)
            {
                throw;
            }
        }
        [HttpPut]
        [Route("Archieve")]
        public ActionResult Archieve(long noteID)
        {
            try
            {
                long userID = Convert.ToInt32(User.Claims.FirstOrDefault(a => a.Type == "UserID").Value);
                var result = this.noteBN.Archieve(noteID);
                if (result != null)
                {
                    logger.LogInformation("Note Sent Archieve Place");
                    return this.Ok(new { success = true, message = "Archieve is worked", Data = result });

                }
                else
                {
                    logger.LogWarning("Note not sent Archieve place");
                    return this.BadRequest(new { success = false,  message = "Archieve is not work"});
                }
            }
            catch(Exception)
            {
                throw;
            }
        }
        [HttpGet("DisplayArchieveNotes")]
        public ActionResult DisplayArchieveNotes()
        {
            try
            {
                long userID = Convert.ToInt32(User.Claims.FirstOrDefault(a => a.Type == "UserID").Value);
                IEnumerable<NoteTable> DisplayArchieveNote = this.noteBN.GetNote(userID);
                if (DisplayArchieveNote != null)
                {
                    logger.LogInformation("DisplayArchieveNotes successfully");
                    return this.Ok(new { success = true, message = "Display Notes is exist", Data = DisplayArchieveNote });
                }
                else
                {
                    logger.LogWarning("Display ArchieveNotes failed");
                    return this.BadRequest(new { success = true, message = "Display Notes is not exist" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPut]
        [Route("Trash")]
        public ActionResult Trash(long noteID)
        {
            try
            {
                long userID = Convert.ToInt32(User.Claims.FirstOrDefault(a => a.Type == "UserID").Value);
                var result = this.noteBN.Trash(noteID);
                if(result != null)
                {
                    logger.LogInformation("Trash is worked");
                    return this.Ok(new { success = true, message = "Trash is worked",Data =result });
                }
                else
                {
                    logger.LogWarning("Trash is not worked");
                    return this.BadRequest(new { success = false, message = "Trash is not worked" });
                }
            }
            catch(Exception)
            {
                throw;
            }
        }
        [HttpGet("DisplayTrashNotes")]
        public ActionResult DisplayTrashNotes()
        {
            try
            {
                long userID = Convert.ToInt32(User.Claims.FirstOrDefault(a => a.Type == "UserID").Value);
                IEnumerable<NoteTable> DisplayTrashNote = this.noteBN.GetNote(userID);
                if (DisplayTrashNote != null)
                {
                    logger.LogInformation("Display TrashNotes Successfully");
                    return this.Ok(new { success = true, message = "Display Notes is exist", Data = DisplayTrashNote });
                }
                else
                {
                    logger.LogWarning("Display TrashNotes failed");
                    return this.BadRequest(new { success = true, message = "Display Notes is not exist" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpDelete]
        [Route("DeleteForever")]
        public ActionResult DeleteNotesForever(long noteID)
        {
            try
            {
                long userID = Convert.ToInt32(User.Claims.FirstOrDefault(a => a.Type == "UserID").Value);
                var result = this.noteBN.DeleteNotesForever(noteID);
                if (result != null)
                {
                    logger.LogInformation("Notes delete Forever");
                    return this.Ok(new { success = true, message = "DeleteNotesForever successfully" });
                }
                else
                {
                    logger.LogWarning("DeleteNotes Forever failed");
                    return this.BadRequest(new { success = false, message = "DeleteNotesForever Failed" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        [HttpPut]
        [Route("UploadImage")]

        public ActionResult Image(long usedID,long noteID, IFormFile img)
        {
            try
            {
                long userID = Convert.ToInt32(User.Claims.FirstOrDefault(a => a.Type == "UserID").Value);
                var result = this.noteBN.Image(usedID,noteID, img);
                if (result != null)
                {
                    logger.LogInformation("Upload image successfully");
                    return this.Ok(new { success = true, message = "UploadImage Successfully", Data = result });
                }
                else
                {
                    logger.LogWarning("Upload image failed");
                    return this.BadRequest(new { success = false, message = "UploadImage Failed" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [Route("Reminder")]

        public ActionResult Reminder(long noteID, DateTime Reminder)
            {
            try
            {
                long userID = Convert.ToInt32(User.Claims.FirstOrDefault(a => a.Type == "UserID").Value);
                var result = this.noteBN.Reminder(noteID, Reminder);
                if (result != null)
                {
                    logger.LogInformation("Reminder will work");
                    return this.Ok(new { success = true, message = "Reminder will work", Data = result });
                }
                else
                {
                    logger.LogWarning("Reminder will not work");
                    return this.BadRequest(new { success = false, message = "Reminder will not work" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet]
        [Route("GetNotesByRedis")]
        public async Task<ActionResult> GetAllNotesByRadisCache()   //Controller
        {
            try
            {
                string serializeNoteList;
                var notelist = new List<NoteTable>();
                var redisNoteList = await distributedCache.GetAsync(keyName);
                if (redisNoteList != null)
                {
                    serializeNoteList = Encoding.UTF8.GetString(redisNoteList);
                    notelist = JsonConvert.DeserializeObject<List<NoteTable>>(serializeNoteList);
                }
                else
                {
                    notelist = await this.noteBN.GetAllNotesByRadisCache();
                    serializeNoteList = JsonConvert.SerializeObject(notelist);
                    redisNoteList = Encoding.UTF8.GetBytes(serializeNoteList);
                    var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                    await distributedCache.SetAsync(keyName, redisNoteList, options);
                }

                return this.Ok(new { success = true, message = "Note is Success", Data = notelist });
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

}
