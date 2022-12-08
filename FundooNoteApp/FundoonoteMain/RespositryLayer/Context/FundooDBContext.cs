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
        public DbSet<UserTable> UserDetailTables { get; set; }
        public DbSet<NoteTable> NoteDetailTables { get; set; }

        public DbSet<LabelTable> LabelDetailTables{ get; set; }
        public DbSet<CollabTable> CollabDetailTables { get; set; }
    }
}
