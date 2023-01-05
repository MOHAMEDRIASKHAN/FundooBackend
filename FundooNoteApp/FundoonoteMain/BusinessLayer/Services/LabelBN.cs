using BusinessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using RespositryLayer.Context;
using RespositryLayer.Entity;
using RespositryLayer.Interface;
using RespositryLayer.Migrations;
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
        FundooDBContext dBContext;

        public LabelBN(ILabelRepo labelRepo, FundooDBContext dBContext)
        {
            this.labelRepo = labelRepo;
            this.dBContext = dBContext;
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
        public async Task<List<LabelTable>> GetAllLableNotesByRadisCache()
        {
            try
            {
                return await dBContext.LabelDetailTables.ToListAsync();

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
