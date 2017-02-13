using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrinterToner.Models
{
    public class PrinterTonerCompatibility
    {
        public int PrinterTonerCompatibilityID { get; set; }

        public int PrinterID { get; set; }
        public virtual Printer Printer { get; set; }

        public int TonerID { get; set; }
        public virtual Toner Toner { get; set; }
    }
}