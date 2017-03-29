using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrinterToner.Models
{
    public class Servis : BaseClass
    {
        public int ServisID { get; set; }
        public DateTime ServisDate { get; set; }
        public float ServisPrice { get; set; }

        public int PrinterID { get; set; }
        public virtual Printer Printer { get; set; }

        public string Napomena { get; set; }
    }
}