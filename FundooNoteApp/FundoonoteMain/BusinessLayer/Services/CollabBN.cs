using BusinessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using RespositryLayer.Context;
using RespositryLayer.Entity;
using RespositryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class CollabBN : ICollabBN
    {
        ICollabRepo collabRepo;
        FundooDBContext FundooDBContext;

        public CollabBN(ICollabRepo collabRepo, FundooDBContext fundooDBContext)
        {
            this.collabRepo = collabRepo;
            this.FundooDBContext = fundooDBContext;
        }

        public CollabTable CreateCollab(long noteID, long userID, string CollabEmailID)
        {
            try
            {
                return collabRepo.CreateCollab(noteID, userID, CollabEmailID);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<CollabTable> GetCollabNotes(long userID, long noteID)
        {
            try
            {
                return collabRepo.GetCollabNotes(userID, noteID);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool DeleteCollab(long userID, long collabID)
        {
            try
            {
                return collabRepo.DeleteCollab(userID, collabID);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<List<CollabTable>> GetAllCollabNotesByRadisCache()   //BL
        {
            try
            {
                return await FundooDBContext.CollabDetailTables.ToListAsync();

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
