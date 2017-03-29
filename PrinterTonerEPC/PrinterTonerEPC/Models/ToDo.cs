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
        
        public string Description { get; set; }

        public DateTime? Closed { get; set; }
        public int UserID { get; set; }
        public virtual User User { get; set; }

        //TODO: delete, not going to be used anymore
        public bool IsClosed { get; set; }
    }
}