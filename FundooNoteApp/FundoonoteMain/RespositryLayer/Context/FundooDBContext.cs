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
        public DbSet<UserTable> UserDetailTable { get; set; }
        public DbSet<NoteTable> NoteTable { get; set; }
    }
}
