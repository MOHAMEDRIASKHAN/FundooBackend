using CommonLayer.Model;
using Microsoft.EntityFrameworkCore;
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
    public class CollabRepo : ICollabRepo
    {
        private readonly FundooDBContext dbcontext;
        private readonly IConfiguration configuration;
        public CollabRepo(FundooDBContext dbcontext, IConfiguration configuration)
        {
            this.dbcontext = dbcontext;
            this.configuration = configuration;
        }

        public CollabTable CreateCollab(long userID, long noteID, string CollabEmailID)
        {
            try
            {

                CollabTable collabTable = new CollabTable();
                collabTable.UserID = userID;
                collabTable.NoteID = noteID;
                collabTable.CollabEmail = CollabEmailID;
                collabTable.Modifiedat = DateTime.Now;
                dbcontext.CollabDetailTables.Add(collabTable);
                dbcontext.SaveChanges();

                return collabTable;

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
                var result = dbcontext.CollabDetailTables.Where(c => c.UserID == userID && c.NoteID == noteID).ToList();
                return result;
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
                var result = dbcontext.CollabDetailTables.FirstOrDefault(c => c.UserID == userID && c.CollabID == collabID);
                if (result != null)
                {
                    dbcontext.CollabDetailTables.Remove(result);
                    dbcontext.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<List<CollabTable>> GetAllCollabNotesByRadisCache()   //RL
        {
            try
            {
                return await dbcontext.CollabDetailTables.ToListAsync();

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
