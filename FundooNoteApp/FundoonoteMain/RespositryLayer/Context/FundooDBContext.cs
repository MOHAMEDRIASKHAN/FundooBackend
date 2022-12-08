using Microsoft.EntityFrameworkCore;
using RespositryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RespositryLayer.Context
{
    public class FundooDBContext :DbContext
    {
        public FundooDBContext(DbContextOptions options):base(options)
        {

        }
        public DbSet<UserTable> UserTables { get; set; }
        public DbSet<NoteTable> NoteDetailTable { get; set; }

        public DbSet<LabelTable> LabelTables{ get; set; }
        public DbSet<CollabTable> CollabDetailTable { get; set; }
    }
}
