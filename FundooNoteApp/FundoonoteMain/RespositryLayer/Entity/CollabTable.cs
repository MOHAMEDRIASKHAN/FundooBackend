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


        [ForeignKey("UserTables")]
        public long UserId { get; set; }
        [ForeignKey("NoteDetailTable")]
        public long NoteID { get; set; }
        public virtual UserTable? UserTables { get; set; }
        public virtual NoteTable? NoteDetailTable { get; set; }
    }
}
