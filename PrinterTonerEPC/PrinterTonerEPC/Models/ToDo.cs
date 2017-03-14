using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PrinterToner.Models;

namespace PrinterToner.Models
{
    public class ToDo
    {
        public int ToDoID { get; set; }
        
        public DateTime Opened { get; set; }
        public int UserOpenedID { get; set; }
        public virtual User UserOpened { get; set; }

        public string Description { get; set; }

        public DateTime Closed { get; set; }
        public int UserClosedID { get; set; }
        public virtual User UserClosed { get; set; }
    }
}