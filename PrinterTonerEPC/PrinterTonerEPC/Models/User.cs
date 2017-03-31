using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrinterToner.Models
{
    public class User
    {
        public int UserID { get; set; }

        [Required(ErrorMessage = "Morate uneti model tonera")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Morate uneti model tonera")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Morate uneti model tonera")]  
        public string Name { get; set; }
        [Required(ErrorMessage = "Morate uneti model tonera")]
        public string Nick { get; set; }
        [Required(ErrorMessage = "Morate uneti model tonera")]
        public string Telephone { get; set; }
        
        public bool IsAdmin { get; set; }
        public string Remark { get; set; }

        public virtual ICollection<ToDo> ToDoes { get; set; }
    }
}