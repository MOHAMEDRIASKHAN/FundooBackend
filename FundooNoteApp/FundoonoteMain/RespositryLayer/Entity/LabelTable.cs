using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RespositryLayer.Entity
{
    public class LabelTable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public long LabelID { get; set; }
        public string? LabelName { get; set; }

        [ForeignKey("UserDetailTables")]
        public long UserID { get; set; }

        [ForeignKey("NoteDetailTables")]
        public long NoteID { get; set; }
        
        public virtual UserTable? UserDetailTables { get; set; }
        public virtual NoteTable? NoteDetailTables { get; set; }
    }
}
