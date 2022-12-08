using Microsoft.Extensions.Configuration;
using RespositryLayer.Context;
using RespositryLayer.Entity;
using RespositryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RespositryLayer.Services
{
    public class LabelRepo : ILabelRepo
    {
        private readonly FundooDBContext fundooDBContext;
        private readonly IConfiguration configuration;

        public LabelRepo(FundooDBContext fundooDBContext, IConfiguration configuration)
        {
            this.fundooDBContext = fundooDBContext;
            this.configuration = configuration;
        }

        public LabelTable CreateLabel(long userID, long noteID, string label)
        {
            try
            {
                LabelTable labelTable = new LabelTable();
                labelTable.LabelName = label;
                labelTable.NoteID = noteID;
                labelTable.UserID = userID;
                fundooDBContext.LabelTables.Add(labelTable);
                int result = fundooDBContext.SaveChanges();
                if (result > 0)
                {
                    return labelTable;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<LabelTable> GetLabels(long noteID, long userID)  //GetLabelbyNoteID
        {
            try
            {
                var result = fundooDBContext.LabelTables.Where(e => e.NoteID == noteID && e.UserID == userID).ToList();
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool RemoveLabel(long userID, long labelID)  //RemoveLabelbyNote
        {
            try
            {
                var result = fundooDBContext.LabelTables.FirstOrDefault(x => x.UserID == userID && x.LabelID == labelID);
                if (result != null)
                {
                    fundooDBContext.Remove(result);
                    fundooDBContext.SaveChanges();
                    return true;

                }
                return false;
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
                var result = fundooDBContext.LabelTables.FirstOrDefault(x => x.UserID == userID && x.LabelName == oldLabelName);
                if (result != null)
                {
                    result.LabelName = newLabelName;
                    fundooDBContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
