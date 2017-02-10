using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PrinterToner.Models;

namespace PrinterToner.Models
{
    public class Owner : BaseClass
    {
        public int OwnerID { get; set; }
        [Required(ErrorMessage = "Morate uneti naziv firme.")]
        public string OwnerName { get; set; }
        [Required(ErrorMessage = "Morate uneti telefon firme.")]
        public string OwnerTelephone { get; set; }
        [Required(ErrorMessage = "Morate uneti adresu firme.")]
        public string OwnerAddress { get; set; }
        public string OwnerContact { get; set; }
        public string OwnerContactTelephone { get; set; }
        [Required(ErrorMessage = "Morate uneti PIB firme.")]
        public string OwnerPIB { get; set; }
        [Required(ErrorMessage = "Morate uneti matični broj firme.")]
        public string OwnerMatBroj { get; set; }
        public bool OwnerIsInPDV { get; set; }

        //public virtual ICollection<Printer> Printers { get; set; }
        public virtual ICollection<Contract> Contracts { get; set; }
        public virtual ICollection<Servis> Servis { get; set; }
    }
}