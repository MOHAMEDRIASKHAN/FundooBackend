using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RespositryLayer.Context;

namespace FundooNoteApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CollabController : ControllerBase
    {
        ICollabBN collabBN;
        FundooDBContext FundooDBContext;
        public CollabController(ICollabBN collabBN, FundooDBContext fundooDBContext)
        {
            this.collabBN = collabBN;
            FundooDBContext = fundooDBContext;
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
                    return this.Ok(new { success = true, message = "Collab will be Created",Data = result });
                }
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
                    return this.Ok(new { success = true, message = "GetCollabNotes is successfully",Data = result });
                }
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
                    return this.Ok(new { success = true, message = "DeleteCollab is successfully" });
                }
                return this.BadRequest(new { success = false, message = "DeleteCollab is failed" });
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}
