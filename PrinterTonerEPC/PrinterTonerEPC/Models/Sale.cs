using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrinterToner.Models
{
    public class Sale : BaseClass
    {
        public int SaleID { get; set; }
        [Required(ErrorMessage = "Morate uneti datum prodaje YYYY.MM.DD")]
        public DateTime SaleDate { get; set; }
        public float Price { get; set; }
        //TODO: Tip ugovora
        public SaleType a { get; set; }
        public enum SaleType { Pausal, GratisRenta, Else }

        //[Range(1, 36, ErrorMessage = "Trajanje ugovora se unosi u mesecima i mora biti veće od 0")]
        //IZBACITI
        public int SaleDuration { get; set; }

        public int ContractID { get; set; }
        public virtual Contract Contract { get; set; }

        public int PrinterID { get; set; }
        public virtual Printer Printer { get; set; }

        public int TonerID { get; set; }
        public virtual Toner Toner { get; set; }

    }
}