using RespositryLayer.Entity;

namespace RespositryLayer.Interface
{
    public interface ILabelRepo
    {
        LabelTable CreateLabel(long userID, long noteID, string label);
        List<LabelTable> GetLabels(long noteID, long userID);
        bool RemoveLabel(long userID, long labelID);
        bool RenameLabel(long userID, string oldLabelName, string newLabelName);
    }
}