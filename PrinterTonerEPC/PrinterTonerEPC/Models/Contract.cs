using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrinterToner.Models
{
    public class Contract : BaseClass
    {
        //[Key]
        public int ContractID { get; set; }
        public string ContractName { get; set; }
        public int ContractDuration { get; set; }

        public int OwnerID { get; set; }
        public virtual Owner Owner { get; set; }

        public bool ContractComplete { get; set; }

        public virtual ICollection<Sale> Sales { get; set; }
    }
}