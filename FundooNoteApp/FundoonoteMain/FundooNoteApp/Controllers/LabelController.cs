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
    public class LabelController : ControllerBase
    {
        ILabelBN labelBN;
        FundooDBContext FundooDBContext;

        public LabelController(ILabelBN labelBN, FundooDBContext fundooDBContext)
        {
            this.labelBN = labelBN;
            FundooDBContext = fundooDBContext;
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
                    return this.Ok(new { success = true, message = "Create LabelNotes Successfuly", });
                }
                else
                {
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
                    return this.Ok(new { success = true, message = "Note is Success", Data = result });
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
        [HttpDelete("DeleteLabelNotes")]
        public ActionResult RemoveLabel( long labelID)
        {
            try
            {
                long userID = Convert.ToInt32(User.Claims.FirstOrDefault(a => a.Type == "UserID").Value);
                var result = this.labelBN.RemoveLabel( userID,labelID);
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
        [HttpPut("RenameLabelNotes")]
        public ActionResult RenameLabel(string oldLabelName, string newLabelName)
        {
            try
            {
                long userID = Convert.ToInt32(User.Claims.FirstOrDefault(a => a.Type == "UserID").Value);
                var result = this.labelBN.RenameLabel(userID, oldLabelName, newLabelName);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "RenameLabelNotes Successfully" });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "RenameLabelNotes Failed" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
