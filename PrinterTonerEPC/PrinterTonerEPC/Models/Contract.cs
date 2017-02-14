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
        [Key]
        public int ContractID { get; set; }
        [Required(ErrorMessage = "Morate uneti naziv ugovora.")]
        [StringLength(100)]
        public string ContractName { get; set; }
        public DateTime ContractDate { get; set; }

        public int OwnerID { get; set; }
        public virtual Owner Owner { get; set; }

        public ContractType ContractIs { get; set; }
        public enum ContractType { Pausal, GratisRenta, Else }

        [Range(0, 36, ErrorMessage = "Trajanje ugovora se unosi u mesecima i mora biti veće od 0")]
        public int ContactDuration { get; set; }
         
        public bool ContractComplete { get; set; }
        public bool ContractActive { get; set; }

        public virtual ICollection<Sale> Sales { get; set; }
    }
}