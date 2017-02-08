using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrinterToner.Models
{
    public class Printer : BaseClass
    {
        public int PrinterID { get; set; }
        [Required(ErrorMessage = "Morate uneti naziv proizvođača štampača")]
        public string PrinterManufacturer { get; set; }
        [Required(ErrorMessage = "Morate uneti model štampača")]
        public string PrinterModel { get; set; }
        public string PrinterSerialNo { get; set; }
        public string PrinterHardwareNo { get; set; }
        //public string PrinterTonerMake { get; set; }
        public string PrinterInternalNo { get; set; }
        public bool PrinterDecommissioned { get; set; }
        public bool IsEPCprinter { get; set; }

        //public int OwnerID { get; set; }
        //public virtual Owner Owner { get; set; }

        public virtual ICollection<Sale> Sales { get; set; }
    }
}