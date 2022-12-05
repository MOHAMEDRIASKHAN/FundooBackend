using BusinessLayer.Interfaces;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RespositryLayer.Context;
using RespositryLayer.Entity;

namespace FundooNoteApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NoteController : ControllerBase
    {
        INoteBN noteBN;
        FundooDBContext FundooDB;
        public NoteController(INoteBN noteBN, FundooDBContext FundooDB)
        {
            this.noteBN = noteBN;
            this.FundooDB = FundooDB;
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
                    return this.Ok(new { success = true, message = "Create Notes Successfuly" });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Notes can`t Created" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet("GetNotes")]
        public ActionResult GetNotes(long userId)
        {
            try
            {
                long userID = Convert.ToInt32(User.Claims.FirstOrDefault(a => a.Type == "userID").Value);
                IEnumerable<NoteTable> result = this.noteBN.GetNote(userId);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Note is Success" });
                }
                else
                {
                    return this.BadRequest(new { success = true, message = "Note not Created" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpDelete("DeleteNotes")]
        public ActionResult DeleteNote(long userId, long noteId)
        {
            try
            {
                long userID = Convert.ToInt32(User.Claims.FirstOrDefault(a => a.Type == "userID").Value);
                var result = this.noteBN.DeleteNote(userId, noteId);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "DeleteNotes successfully" });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "DeleteNotes Failed" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        //[HttpPut("UpdateNotes")]
        //public ActionResult UpdateNotes(NotesModel notesModel, long userId, long noteID)
        //{
        //    try
        //    {
        //        long userID = Convert.ToInt32(User.Claims.FirstOrDefault(a => a.Type == "userID").Value);
        //        var result = this.noteBN.UpdateNotes(notesModel, userId, noteID);
        //        if (result != null)
        //        {
        //            return this.Ok(new { success = true, message = "UpdateNotes Successfully" });
        //        }
        //        else
        //        {
        //            return this.BadRequest(new { success = false, message = "UpdateNotes Failed" });
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
        [HttpPost("Color")]
        public ActionResult color(long noteid, string color)
        {
            try
            {
                long userID = Convert.ToInt32(User.Claims.FirstOrDefault(a => a.Type == "userID").Value);
                var result = this.noteBN.color(noteid, color);
                if(result != null)
                {
                    return this.Ok(new { success = true, message = "Color option will work" });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Color will not work" });
                }
            }
            catch(Exception)
            {
                throw;
            }
        }
    }

}
