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
        DbSet<MaterialClass> Materials { get; set; } 
    }
}