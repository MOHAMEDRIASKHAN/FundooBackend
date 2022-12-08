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
    public class CollabBN : ICollabBN
    {
        ICollabRepo collabRepo;

        public CollabBN(ICollabRepo collabRepo)
        {
            this.collabRepo = collabRepo;
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
    }
}
