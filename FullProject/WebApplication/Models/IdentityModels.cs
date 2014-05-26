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

    /// <summary>
    /// <c>Printer3DJob</c> is the class version of the table Printer3DJob.
    /// </summary>
    public class Printer3DJob
    {
        /// <summary>
        /// The property <c>Printer3DJobId</c> is the 
        /// primary key in the database.
        /// </summary>
        public long Printer3DJobId { get; set; }

        /// <summary>
        /// The property <c>Owner</c> is the
        /// foreign key to the ApplicationUser
        /// table in the database.
        /// </summary>
        public string Owner { get; set; }

        /// <summary>
        /// The property <c>Deadline</c>
        /// </summary>
        public string Deadline { get; set; }

        /// <summary>
        /// The property <c>MyFile</c>
        /// </summary>
        public string MyFile { get; set; }

        /// <summary>
        /// The property <c>CreationTime</c>
        /// </summary>
        public string CreationTime { get; set; }

        /// <summary>
        /// The property <c>Hollow</c>
        /// </summary>
        public int Hollow { get; set; }

        /// <summary>
        /// The property <c>Comment</c>
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// The property <c>Status</c>
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// The property <c>Material</c> is the
        /// foreign key to the PrintMaterial
        /// table in the database.
        /// </summary>
        public PrintMaterial Material { get; set; }
    }


    /// <summary>
    /// <c>PrintMaterial</c> is the class version of the table PrintMaterial.
    /// </summary>
    public class PrintMaterial
    {
        /// <summary>
        /// The property <c>PrintMaterialId</c> is the
        /// primary key in the database.
        /// </summary>
        public int PrintMaterialId { get; set; }

        /// <summary>
        /// The property <c>MaterialType</c>
        /// </summary>
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