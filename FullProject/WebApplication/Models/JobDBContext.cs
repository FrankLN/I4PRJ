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
        DbSet<JobClass> Jobs { get; set; } 
    }
}