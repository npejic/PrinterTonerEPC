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
        [Index(IsUnique = true)]
        [StringLength(100)]
        public string PrinterInternalNo { get; set; }
        [Required(ErrorMessage = "Morate uneti naziv proizvođača štampača")]
        [Index("Manufaturer_Model", 1, IsUnique = true)]
        [StringLength(100)]
        public string PrinterManufacturer { get; set; }
        [Required(ErrorMessage = "Morate uneti model štampača")]
        //[Index(IsUnique = true)]
        [Index("Manufaturer_Model", 2, IsUnique = true)]
        [StringLength(100)]
        public string PrinterModel { get; set; }
        [Required(ErrorMessage = "Morate uneti serijski broj štampača")]
        [Index(IsUnique = true)]
        [StringLength(100)]
        public string PrinterSerialNo { get; set; }
        public string PrinterHardwareNo { get; set; }
        //public string PrinterTonerMake { get; set; }
        
        public bool PrinterDecommissioned { get; set; }
        public bool IsEPCprinter { get; set; }

        //public int OwnerID { get; set; }
        //public virtual Owner Owner { get; set; }

        public virtual ICollection<Sale> Sales { get; set; }
        public virtual ICollection<Servis> Servis { get; set; }
    }
}