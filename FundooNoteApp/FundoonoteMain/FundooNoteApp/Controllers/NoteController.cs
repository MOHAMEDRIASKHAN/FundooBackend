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
                    return this.Ok(new { success = true,  message =" PinNotes will work", Data = result });
                }
                else
                {
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
                    return this.Ok(new { success = true, message = "Archieve is worked", Data = result });

                }
                else
                {
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
                    return this.Ok(new { success = true, message = "Display Notes is exist", Data = DisplayArchieveNote });
                }
                else
                {
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
                    return this.Ok(new { success = true, message = "Trash is worked",Data =result });
                }
                else
                {
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
                    return this.Ok(new { success = true, message = "Display Notes is exist", Data = DisplayTrashNote });
                }
                else
                {
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
                    return this.Ok(new { success = true, message = "DeleteNotesForever successfully" });
                }
                else
                {
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
                    return this.Ok(new { success = true, message = "UploadImage Successfully", Data = result });
                }
                else
                {
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
                    return this.Ok(new { success = true, message = "Reminder will work", Data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Reminder will not work" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

}
