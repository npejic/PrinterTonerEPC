using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PrinterToner.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrinterToner.Models
{
    public class ToDo : BaseClass
    {
        public int ToDoID { get; set; }

        [Required(ErrorMessage = "Morate uneti opis posla")]
        public string Description { get; set; }

        public DateTime? Closed { get; set; }
        
        public int UserID { get; set; }
        public virtual User User { get; set; }

        public bool IsReady { get; set; }
        public string Remark { get; set; }
    }
}