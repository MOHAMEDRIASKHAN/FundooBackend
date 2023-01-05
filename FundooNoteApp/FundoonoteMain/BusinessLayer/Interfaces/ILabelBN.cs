using RespositryLayer.Entity;

namespace BusinessLayer.Interfaces
{
    public interface ILabelBN
    {
        LabelTable CreateLabel(long userID, long noteID, string label);
        List<LabelTable> GetLabels(long noteID, long userID);
        bool RemoveLabel(long userID, long labelID);
        bool RenameLabel(long userID, string oldLabelName, string newLabelName);
        public Task<List<LabelTable>> GetAllLableNotesByRadisCache();
    }
}