using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PrinterToner.Models;

namespace PrinterToner.Models
{
    public class ToDo : BaseClass
    {
        public int ToDoID { get; set; }
        
        //public DateTime? Opened { get; set; }
     
        public string Description { get; set; }

        public DateTime? Closed { get; set; }
        public int UserID { get; set; }
        public virtual User User { get; set; }

        public bool IsClosed { get; set; }
    }
}