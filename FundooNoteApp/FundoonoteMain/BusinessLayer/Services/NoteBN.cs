using BusinessLayer.Interfaces;
using CommonLayer.Model;
using RespositryLayer.Entity;
using RespositryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class NoteBN : INoteBN
    {
        INoteRepo noteRepo;
        public NoteBN(INoteRepo noteRepo)
        {
            this.noteRepo = noteRepo;
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
        //public bool UpdateNotes(NotesModel notesModel, long userId, long noteID)
        //{
        //    try
        //    {
        //        return noteRepo.UpdateNote(notesModel, userId, noteID);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}


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
    }
}
