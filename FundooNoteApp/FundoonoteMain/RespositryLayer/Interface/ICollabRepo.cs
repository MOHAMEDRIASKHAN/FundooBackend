using RespositryLayer.Entity;

namespace RespositryLayer.Interface
{
    public interface ICollabRepo
    {
        CollabTable CreateCollab(long noteID, long userID, string CollabEmailID);
        bool DeleteCollab(long userID, long collabID);
        List<CollabTable> GetCollabNotes(long userID, long noteID);
        public Task<List<CollabTable>> GetAllCollabNotesByRadisCache();
    }
}