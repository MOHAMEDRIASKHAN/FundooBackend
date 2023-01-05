using BusinessLayer.Interfaces;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using RespositryLayer.Context;
using RespositryLayer.Entity;
using RespositryLayer.Interface;
using RespositryLayer.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class NoteBN : INoteBN
    {
        INoteRepo1 noteRepo;
        FundooDBContext fundooDBContext;
        public NoteBN(INoteRepo1 noteRepo,FundooDBContext fundooDBContext)
        {
            this.noteRepo = noteRepo;
            this.fundooDBContext = fundooDBContext;
        }

        public NoteTable CreateNotes(NotesModel notesModel, long UserID)
        {
            try
            {
                return this.noteRepo.CreateNotes(notesModel,  UserID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IEnumerable<NoteTable> GetNote(long userId)
        {
            try
            {
                return noteRepo.GetNotes(userId);
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
                return noteRepo.DeleteNote(userId, noteId);
            }
            catch (Exception)
            {
                throw;
            }
        }
       public bool UpdateNotes( long userId, long noteId, NotesModel notesModel)
        {
            try
            {
                return noteRepo.UpdateNotes( userId, noteId, notesModel);
            }
            catch (Exception)
            {
                throw;
            }
        }


        public NoteTable color(long noteid, string color)
        {
            try
            {
                return noteRepo.color(noteid, color);
                
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
                return noteRepo.PinNotes(noteID);
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
                return noteRepo.Archieve(noteID);
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
                return noteRepo.DisplayArchieveNotes(UserId);
            }
            catch(Exception)
            {
                throw;
            }
        }
        public bool Trash(long noteID)
        {
            try
            {
                return noteRepo.Trash(noteID);
            }
            catch(Exception)
            {
                throw;
            }
        }
        public IEnumerable<NoteTable> DisplayTrashNotes(long usedId)
        {
            try
            {
                return noteRepo.DisplayTrashNotes(usedId);
            }
            catch(Exception)
            {
                throw;
            }
        }
        public bool DeleteNotesForever(long noteID)
        {
            try
            {
                return noteRepo.DeleteNotesForever(noteID);
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
                return noteRepo.Image(noteID, userId, img);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public NoteTable Reminder(long noteID, DateTime Reminder)
        {
            try
            {
                return noteRepo.Reminder(noteID, Reminder);
            }
            catch(Exception)
            {
                throw;
            }
        }
        public async Task<List<NoteTable>> GetAllNotesByRadisCache()  //BL
        {
            try
            {
                return await fundooDBContext.NoteDetailTables.ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
