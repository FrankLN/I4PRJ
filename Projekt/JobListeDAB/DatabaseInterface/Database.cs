using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Configuration;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace DatabaseInterface
{
    public class Database
    {
        private SqlConnection conn;
        Database()
        {
            conn = new SqlConnection(@"Data Source=(localdb)\Projects;Initial Catalog=Joblistesystem;Integrated Security=True;");
        }

       /* void AddUser(User user)
        {
            DataContext db = new DataContext(@"C:\Users\Søren Emil\AppData\Local\Microsoft\VisualStudio\SSDT\JobListeDAB\Joblistesystem.mdf");
           
            // Tilføjer et objekt i Bruger tabellen på databasen.
        }*/

        int ValidateLogInIndo(string email, string password)
        {
            DataContext db = new DataContext(@"Data Source=(localdb)\Databases;Initial Catalog=Joblistesystem;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False");
            conn.Open();
            Table<User> users = db.GetTable<User>();
             /* Sammenligner email og password med databasen. Hvis der findes en bruger 
             * med matchende email og password returneres dennes bruger ID. Ellers returneres -1. */

            var query =
                from U1 in users
                where U1.Email == email
                select U1;

            foreach (var U1 in query)
            {
                if (password == U1.Password)
                {
                    return 1;
                } 
            }
            return 0;

            /*var query_2 =
                from U2 in Bruger
                where U2.Password == password
                select U2;

            if (query_1 == null || query_2 == null)
            {
                return -1;
            }*/
        }

       /* User GetUserInfo(int userId)
        {
            DataContext db = new DataContext(@"C:\Users\Søren Emil\AppData\Local\Microsoft\VisualStudio\SSDT\JobListeDAB\Joblistesystem.mdf");
            
            // Returnerer et objekt af en bruger med matchende ID fra databasen.

        }

        void AddJob(Job job)
        {
            DataContext db = new DataContext(@"C:\Users\Søren Emil\AppData\Local\Microsoft\VisualStudio\SSDT\JobListeDAB\Joblistesystem.mdf");
            
            // Tilføjer et objekt i Job tabellen på databasen.

        }

        List<Job> GetJobList()
        {
            DataContext db = new DataContext(@"C:\Users\Søren Emil\AppData\Local\Microsoft\VisualStudio\SSDT\JobListeDAB\Joblistesystem.mdf");

            // Returnerer en liste over alle jobs i databasen.

        }*/
    }
}
