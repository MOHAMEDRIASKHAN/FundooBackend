using BusinessLayer.Interfaces;
using RespositryLayer.Entity;
using RespositryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class LabelBN : ILabelBN
    {
        ILabelRepo labelRepo;

        public LabelBN(ILabelRepo labelRepo)
        {
            this.labelRepo = labelRepo;
        }

        public LabelTable CreateLabel(long userID, long noteID, string label)
        {
            try
            {
                return labelRepo.CreateLabel(userID, noteID, label);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<LabelTable> GetLabels(long noteID, long userID)
        {
            try
            {
                return labelRepo.GetLabels(noteID, userID);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool RemoveLabel(long userID, long labelID)
        {
            try
            {
                return labelRepo.RemoveLabel(userID, labelID);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool RenameLabel(long userID, string oldLabelName, string newLabelName)
        {
            try
            {
                return labelRepo.RenameLabel(userID, oldLabelName, newLabelName);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
