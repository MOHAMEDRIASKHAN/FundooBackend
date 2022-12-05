using CommonLayer.Model;
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
    }
}