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
        public ActionResult GetNotes()
        {
            try
            {
                long userID = Convert.ToInt32(User.Claims.FirstOrDefault(a => a.Type == "UserID").Value);
                IEnumerable<NoteTable> result = this.noteBN.GetNote(userID);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Note is Success",Data = result });
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
        public ActionResult DeleteNote( long noteID)
        {
            try
            {
                long userID = Convert.ToInt32(User.Claims.FirstOrDefault(a => a.Type == "UserID").Value);
                var result = this.noteBN.DeleteNote(userID, noteID);
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
        [HttpPut("UpdateNotes")]
        public ActionResult UpdateNotes( long noteID, NotesModel notesModel)
        {
            try
            {
                long userID = Convert.ToInt32(User.Claims.FirstOrDefault(a => a.Type == "UserID").Value);
                var result = this.noteBN.UpdateNotes( userID, noteID, notesModel);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "UpdateNotes Successfully" });
                }
                else
                {
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
                    return this.Ok(new { success = true, message = "Color option will work",Data = result });
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
