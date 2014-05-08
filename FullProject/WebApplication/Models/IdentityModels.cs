using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace WebApplication.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string FName { get; set; }
        public string LName { get; set; }
        public string Phone { get; set; }
        public int AdminRights { get; set; }
        public int Activated { get; set; }
        public string ActivationCode { get; set; }
    }

    public class Printer3DJob
    {
        public long Printer3DJobId { get; set; }
        public string Owner { get; set; }
        public string Deadline { get; set; }
        public string MyFile { get; set; }
        public string CreationTime { get; set; }
        public int Hollow { get; set; }
        public string Comment { get; set; }
        public int Status { get; set; }
    }
    public class PrintMaterial
    {
        public int PrintMaterialId { get; set; }
        public string MaterialType { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }

        public System.Data.Entity.DbSet<WebApplication.Models.Printer3DJob> Printer3DJob { get; set; }

        public System.Data.Entity.DbSet<WebApplication.Models.PrintMaterial> PrintMaterials { get; set; }

    }

}