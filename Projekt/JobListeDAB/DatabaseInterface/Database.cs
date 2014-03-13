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
        
 
       void AddUser(UserClass user)
       // AddUser adds an object in the User tabel on the database.
        {
            //DataContext db = new DataContext(@"C:\Users\Søren Emil\AppData\Local\Microsoft\VisualStudio\SSDT\JobListeDAB\Joblistesystem.mdf");

           try
           {
               // Open the connection
               conn.Open();

               // String with SQL statement
               string userInsert = @"INSERT INTO [User] (Email, Name, PhoneNumber, AdminRights, Password) 
                                        VALUES (@Data1,@Data2,@Data3,@Data4,@Data5)";

               using (SqlCommand cmd = new SqlCommand(userInsert, conn))
               {
                   // Get your parameters ready
                   cmd.Parameters.Add(cmd.CreateParameter()).ParameterName = "@data1";
                   cmd.Parameters.Add(cmd.CreateParameter()).ParameterName = "@data2";
                   cmd.Parameters.Add(cmd.CreateParameter()).ParameterName = "@data3";
                   cmd.Parameters.Add(cmd.CreateParameter()).ParameterName = "@data4";
                   cmd.Parameters.Add(cmd.CreateParameter()).ParameterName = "@data5";
                   cmd.Parameters["@Data1"].Value = user.Email;
                   cmd.Parameters["@Data2"].Value = user.Name;
                   cmd.Parameters["@Data3"].Value = user.PhoneNumber;
                   cmd.Parameters["@Data4"].Value = user.AdminRights;
                   cmd.Parameters["@Data5"].Value = user.Password;

                   //var id 
                   User.HI = (int) cmd.ExecuteScalar(); //Returns the identity of the new tuple/record
               }
           }
           finally 
           {
               // Close the connection
               if (conn != null)
               {
                   conn.Close();
               }
           }
        }

        int ValidateLogInIndo(string email, string password)
        {
            //DataContext db = new DataContext(@"Data Source=(localdb)\Databases;Initial Catalog=Joblistesystem;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False");
            try
            {
                // Open the connection
                conn.Open();

                 /* Sammenligner email og password med databasen. Hvis der findes en bruger 
                * med matchende email og password returneres dennes bruger ID. Ellers returneres -1. */

                // String with SQL statement
                string validate = @"SELECT Password FROM User WHERE User.Email = email GO";

                string passReturn;

                // Validation of password
                using (SqlCommand cmd = new SqlCommand(validate, conn))
                {
                    // Get your parameters ready

                    passReturn = (string) cmd.ExecuteScalar(); //Returns the identity of the new tuple/record
                }
                if (passReturn == password)
                {
                    return 1;
                }

                return 0;

            }
            finally
            {
                // Close the connection
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        UserClass GetUserInfo(int userId)
         // Returns an object of UserClass with ID matching from the database.
         {
             try
             {
                 // Open the connection
                 conn.Open();

                 // String with SQL statement
                 string userInfo = @"SELECT * FROM [User] WHERE User.Email = email";

                 using (SqlCommand cmd = new SqlCommand(userInfo, conn))
                {
                    // Get your parameters ready 

                    /**********************************************************
                     * FØLGENDE SKAL RETTES TIL ******************************
                     **********************************************************/


                    cmd.Parameters.Add(cmd.CreateParameter()).ParameterName = "@data1";

                    cmd.Parameters["@Data1"].Value = this.locHåndværker.HID;

                    rdr = cmd.ExecuteReader(); //Returns the identity of the new tuple/record
                    locHåndværker.Ejes_af = new List<Værktøjskasse>();

                    Værktøjskasse vk = new Værktøjskasse();
                    while (rdr.Read())
                    {
                        Console.WriteLine(rdr[0]);


                        vk.VKID = (int)rdr["VKasseId"];
                        vk.Anskaffet = (DateTime)rdr["Anskaffet"];
                        vk.Fabrikat = (string)rdr["Fabrikat"];
                        vk.Håndværker = (int)rdr["Håndværker"];
                        vk.Model = (string)rdr["Model"];
                        vk.Seriennr = (string)rdr["Serienummer"];
                        locHåndværker.Ejes_af.Add(vk);
                    }
             }
             finally
             {
                 // Close the connection
                 if (conn != null)
                 {
                     conn.Close();
                 }
             }
         }

         void AddJob(JobClass job)
         // Add a Jobclass object in the Job tabel on the database.
         {
             try
             {
                 // Open the connection
                 conn.Open();

                 // String with SQL statement
                 string userInsert = @"";
             }
             finally
             {
                 // Close the connection
                 if (conn != null)
                 {
                     conn.Close();
                 }
             }
         }

         List<JobClass> GetJobList()
         // Return a list of all jobs in the database.
         {
             try
             {
                 // Open the connection
                 conn.Open();

                 // String with SQL statement
                 string userInsert = @"";
             }
             finally
             {
                 // Close the connection
                 if (conn != null)
                 {
                     conn.Close();
                 }
             }
         }
    }
}
