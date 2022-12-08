using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using RespositryLayer.Context;
using RespositryLayer.Entity;
using RespositryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RespositryLayer.Services
{
    public class NoteRepo : INoteRepo1
    {
        private readonly FundooDBContext fundooDBContext;
        private readonly IConfiguration configuration;
        public NoteRepo( IConfiguration configuration, FundooDBContext fundooDBContext)
        {
            this.fundooDBContext = fundooDBContext;
            this.configuration = configuration;
        }
        

        public NoteTable CreateNotes(NotesModel notesModel, long userId)
        {
            try
            {
                var validationUser = fundooDBContext.UserTables.Where(a => a.UserID == userId);
                if (validationUser != null)
                {
                    NoteTable notestable = new NoteTable();
                    notestable.Title = notesModel.Title;
                    notestable.Body = notesModel.Body;
                    notestable.Reminder = notesModel.Reminder;
                    notestable.Color = notesModel.Color;
                    notestable.Image = notesModel.Image;
                    notestable.Archieve = notesModel.Archieve;
                    notestable.PinNotes = notesModel.PinNotes;
                    notestable.Trash = notesModel.Trash;
                    notestable.Created = notesModel.Created;
                    notestable.Modified = notesModel.Modified;
                    notestable.UserID = userId;
                    fundooDBContext.NoteDetailTable.Add(notestable);

                    fundooDBContext.SaveChanges();
                    return notestable;

                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public IEnumerable<NoteTable> GetNotes(long userId)
        {
            try
            {
                var result = fundooDBContext.NoteDetailTable.Where(a => a.UserID == userId);
                if (result != null)
                {
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteNote(long userId, long noteId)
        {
            try
            {
                var result = fundooDBContext.NoteDetailTable.FirstOrDefault(e => e.NoteID == noteId && e.UserID == userId);
                if (result != null)
                {
                    fundooDBContext.NoteDetailTable.Remove(result);
                    fundooDBContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }


            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool UpdateNotes(long userId, long noteId, NotesModel notesModel)
        {
            try
            {
                var result = fundooDBContext.NoteDetailTable.FirstOrDefault(e => e.UserID == userId && e.NoteID == noteId);
                if (result != null)
                {
                    if (notesModel.Title != null)
                    {
                        result.Title = notesModel.Title;
                    }
                    if (notesModel.Body != null)
                    {
                        result.Body = notesModel.Body;
                    }

                    result.Modified = DateTime.Now;
                    fundooDBContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public NoteTable color(long noteID, string color)
        {
            try
            {
                NoteTable noteTable = this.fundooDBContext.NoteDetailTable.FirstOrDefault(x => x.NoteID == noteID);
                if (noteTable.Color != null)
                {
                    noteTable.Color = color;
                    this.fundooDBContext.SaveChanges();
                    return noteTable;

                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool PinNotes(long noteID)
        {
            try
            {
                NoteTable result = this.fundooDBContext.NoteDetailTable.FirstOrDefault(x => x.NoteID==noteID);
                if(result.PinNotes == true)
                {
                    result.PinNotes = false;
                    this.fundooDBContext.SaveChanges();
                    return false;
                }
                else
                {
                    result.PinNotes = true;
                    this.fundooDBContext.SaveChanges();
                    return false;
                }
            }
            catch(Exception)
            {
                throw;
            }
        }

        public bool Archieve(long noteID)
        {
            try
            {
                NoteTable result = this.fundooDBContext.NoteDetailTable.FirstOrDefault(x => x.NoteID ==noteID);
                if(result.Archieve == true)
                {
                    result.Archieve = false;
                    this.fundooDBContext.SaveChanges();
                    return false;
                }
                else
                {
                    result.Archieve = true;
                    this.fundooDBContext.SaveChanges();
                    return true;
                }
            }
            catch(Exception)
            {
                throw;
            }
        }
        public IEnumerable<NoteTable> DisplayArchieveNotes(long UserId)
        {
            try
            {
                var result = fundooDBContext.NoteDetailTable.Where(r => r.UserID == UserId && r.Archieve == true);
                if (result != null)
                {
                    
                    this.fundooDBContext.SaveChanges();
                    return null;
                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool Trash(long noteID)
        {
            try
            {
                NoteTable result = this.fundooDBContext.NoteDetailTable.FirstOrDefault(x => x.NoteID == noteID);
                if (result.Trash == true)
                {

                    result.Trash = false;
                    this.fundooDBContext.SaveChanges();
                    return false;
                   
                }
                else
                {
                    result.Trash= true;
                    this.fundooDBContext.SaveChanges();
                    return true;
                }
            }
            catch(Exception)
            {
                throw;
            }
        }
        public IEnumerable<NoteTable> DisplayTrashNotes(long usedID)
        {
            try
            {
                var result = fundooDBContext.NoteDetailTable.Where(r => r.UserID == usedID && r.Trash == true);
                if (result != null)
                {

                    return result;
                }
                else
                {
                    return null;
                }
               
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool DeleteNotesForever(long noteID)
        {
            try
            {
                NoteTable DeleteForever = this.fundooDBContext.NoteDetailTable.FirstOrDefault(e => e.NoteID == noteID);
                if(DeleteForever.Trash == true)
                {
                    fundooDBContext.NoteDetailTable.Remove(DeleteForever);
                    this.fundooDBContext.SaveChanges();
                    return false;
                }
                DeleteForever.Trash = true;
                this.fundooDBContext.SaveChanges();
                return true;
            }
            catch(Exception)
            {
                throw;
            }
        }
        public string Image(long userId, long noteID, IFormFile img)
        {
            try
            {
                var result = this.fundooDBContext.NoteDetailTable.FirstOrDefault(i => i.NoteID == noteID && i.UserID == userId);
                if (result != null)
                {
                    Account account = new Account(
                        this.configuration["CloundinarySettings:CloudName"],
                        this.configuration["CloundinarySettings:ApiKey"],
                        this.configuration["CloundinarySettings:ApiSecret"]);


                   
                    Cloudinary cloudinary = new Cloudinary(account);
                    var uploadPara = new ImageUploadParams()
                    {
                        File = new FileDescription(img.FileName, img.OpenReadStream()),
                    };

                    var uploadResult = cloudinary.Upload(uploadPara);
                    string imagePath = uploadResult.Url.ToString();
                    result.Image = imagePath;
                    fundooDBContext.SaveChanges();
                    return "Image uploaded Successfully";
                }
                else
                {
                    return "Image not Uploaded";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public NoteTable Reminder(long noteID, DateTime Reminder)
        {
            try
            {
                NoteTable noteTable = this.fundooDBContext.NoteDetailTable.FirstOrDefault(x => x.NoteID == noteID);
                if (noteTable.Reminder != null)
                {
                    noteTable.Reminder = Reminder;
                    this.fundooDBContext.SaveChanges();
                    return noteTable;

                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
