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
        
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public bool IsAdmin { get; set; }
    }
}