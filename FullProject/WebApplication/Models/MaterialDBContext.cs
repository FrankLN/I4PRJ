using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using DatabaseInterface;

namespace WebApplication.Models
{
    public class MaterialDBContext : DbContext
    {
        public MaterialDBContext() : base("DefaultConnection")
        {
        }
        public DbSet<MaterialClass> Materials { get; set; } 
    }
}