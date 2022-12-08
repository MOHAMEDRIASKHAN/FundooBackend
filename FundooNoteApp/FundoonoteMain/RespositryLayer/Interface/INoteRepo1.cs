using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using RespositryLayer.Entity;

namespace RespositryLayer.Interface
{
    public interface INoteRepo1
    {
        NoteTable color(long noteId, string color);
        NoteTable CreateNotes(NotesModel notesModel, long userId);
        bool DeleteNote(long userId, long noteId);
        IEnumerable<NoteTable> GetNotes(long userId);
        bool UpdateNotes(long userId, long noteId, NotesModel notesModel);
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