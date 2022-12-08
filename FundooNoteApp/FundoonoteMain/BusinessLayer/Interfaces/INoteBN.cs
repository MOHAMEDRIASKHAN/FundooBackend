using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using RespositryLayer.Entity;

namespace BusinessLayer.Interfaces
{
    public interface INoteBN
    {
       public NoteTable CreateNotes(NotesModel notesModel, long UserID);
       public IEnumerable<NoteTable> GetNote(long userId);
        public bool DeleteNote(long userId, long noteId);
        public bool UpdateNotes( long userId, long noteId, NotesModel notesModel);
        public NoteTable color(long noteid, string color);
        public bool PinNotes(long noteID);
        public bool Archieve(long noteID);
        public IEnumerable<NoteTable> DisplayArchieveNotes(long UserId);
        public bool Trash(long noteID);
        public IEnumerable<NoteTable> DisplayTrashNotes(long usedId);
        public bool DeleteNotesForever(long noteID);
        public string Image(long userId, long noteID, IFormFile img);
        public NoteTable Reminder(long noteID, DateTime Reminder);
    }
}