using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace PrinterToner.Models
{
    public class SaleToner
    {
        public int SaleTonerID { get; set; }
        [Required(ErrorMessage = "Morate uneti datum prodaje YYYY.MM.DD")]
        public DateTime SaleTonerDate { get; set; }
        public float TonerPrice { get; set; }

        //public int ContractID { get; set; }
        //public virtual Contract Contract { get; set; }

        public int OwnerID { get; set; }
        public virtual Owner Owner { get; set; }

        public int TonerID { get; set; }
        public virtual Toner Toner { get; set; }
    }
}