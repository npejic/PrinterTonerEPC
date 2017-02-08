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

        public LocationTypeOfPrinter LocationOfPrinterIs { get; set; }
        public enum LocationTypeOfPrinter { U_firmi, U_magacinu }

        public int ContractID { get; set; }
        public virtual Contract Contract { get; set; }

        public int PrinterID { get; set; }
        public virtual Printer Printer { get; set; }

        public int TonerID { get; set; }
        public virtual Toner Toner { get; set; }

    }
}