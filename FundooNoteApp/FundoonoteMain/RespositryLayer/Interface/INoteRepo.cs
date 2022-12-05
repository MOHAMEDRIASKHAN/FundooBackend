using CommonLayer.Model;
using RespositryLayer.Entity;

namespace RespositryLayer.Interface
{
    public interface INoteRepo
    {
        public NoteTable CreateNotes(NotesModel notesModel, long UserID);
        public IEnumerable<NoteTable> GetNotes(long userId);

        public bool DeleteNote(long noteId, long userId);

      //  public bool UpdateNote(NotesModel notesModel, long userId, long noteID);

        public NoteTable color(long noteid, string color);
    }
}