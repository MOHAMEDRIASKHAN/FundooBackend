using CommonLayer.Model;
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
    }
}