using RespositryLayer.Entity;

namespace BusinessLayer.Interfaces
{
    public interface ICollabBN
    {
        CollabTable CreateCollab(long noteID, long userID, string CollabEmailID);
        bool DeleteCollab(long userID, long collabID);
        List<CollabTable> GetCollabNotes(long userID, long noteID);
        public Task<List<CollabTable>> GetAllCollabNotesByRadisCache();
    }
}