using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using DatabaseInterface;

namespace WebApplication.Models
{
    public class JobDBContext : DbContext
    {
        public JobDBContext() : base("DefaultConnection")
        {
        }
        public DbSet<JobClass> Jobs { get; set; } 
    }
}