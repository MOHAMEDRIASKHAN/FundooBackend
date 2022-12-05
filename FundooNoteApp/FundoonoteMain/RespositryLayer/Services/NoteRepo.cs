using CommonLayer.Model;
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

        public NoteRepo(FundooDBContext fundooDBContext)
        {
            this.fundooDBContext = fundooDBContext;
        }

        public NoteTable CreateNotes(NotesModel notesModel, long userId)
        {
            try
            {
                var validationUser = fundooDBContext.UserDetailTable.Where(a => a.UserID == userId);
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
                    fundooDBContext.NoteTable.Add(notestable);

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
                var result = fundooDBContext.NoteTable.Where(a => a.UserID == userId);
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
                var result = fundooDBContext.NoteTable.FirstOrDefault(e => e.NoteID == noteId && e.UserID == userId);
                if (result != null)
                {
                    fundooDBContext.NoteTable.Remove(result);
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
                var result = fundooDBContext.NoteTable.FirstOrDefault(e => e.UserID == userId && e.NoteID == noteId);
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
                NoteTable noteTable = this.fundooDBContext.NoteTable.FirstOrDefault(x => x.NoteID == noteID);
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

    }
}
