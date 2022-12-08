﻿using System;
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

        [ForeignKey("UserTables")]
        public long UserID { get; set; }

        [ForeignKey("NoteDetailTable")]
        public long NoteID { get; set; }
        
        public virtual UserTable? UserTables { get; set; }
        public virtual NoteTable? NoteDetailTable { get; set; }
    }
}
