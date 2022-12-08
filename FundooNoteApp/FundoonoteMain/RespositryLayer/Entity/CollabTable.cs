using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RespositryLayer.Entity
{
    public class CollabTable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long CollabID { get; set; }
        public string? CollabEmail { get; set; }
        public DateTime? Modifiedat { get; set; }


        [ForeignKey("UserDetailTables")]
        public long UserID { get; set; }
        [ForeignKey("NoteDetailTables")]
        public long NoteID { get; set; }
        public virtual UserTable? UserDetailTables { get; set; }
        public virtual NoteTable? NoteDetailTables { get; set; }
    }
}
