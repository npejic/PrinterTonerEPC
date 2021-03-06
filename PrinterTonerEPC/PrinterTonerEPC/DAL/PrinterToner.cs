﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PrinterToner.Models;
using System.Data.Entity;

namespace PrinterTonerEPC.DAL
{
    public class PrinterTonerContext : DbContext
    {
        public PrinterTonerContext() : base("PrinterTonerContext") { }

        public DbSet<Owner> Owners { get; set; }
        public DbSet<Printer> Printers { get; set; }
        public DbSet<Toner> Toners { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<Servis> Servis { get; set; }
        public DbSet<SaleToner> SaleToners { get; set; }
        public DbSet<PrinterTonerCompatibility> PrinterTonerCompatibilitys { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ToDo> ToDoes { get; set; }

        internal void SubmitChanges()
        {
            throw new NotImplementedException();
        }
    }
}