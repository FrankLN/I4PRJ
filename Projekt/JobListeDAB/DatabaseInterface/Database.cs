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
    public class Database : IDatabase
    {
        private SqlConnection conn;
        public Database()
        {
            conn = new SqlConnection(@"Data Source=(localdb)\Projects;Initial Catalog=Joblistesystem;Integrated Security=True;");
        }


        public void AddUser(UserClass user)
       // AddUser adds an object in the User tabel on the database.
        {
            //DataContext db = new DataContext(@"C:\Users\Søren Emil\AppData\Local\Microsoft\VisualStudio\SSDT\JobListeDAB\Joblistesystem.mdf");

           try
           {
               // Open the connection
               conn.Open();

               // String with SQL statement
               string userInsert = @"INSERT INTO [Customer] (Email, FirstName, LastName, PhoneNumber, AdminRights, Password) 
                                        VALUES (@Data1,@Data2,@Data3,@Data4,@Data5, @Data6)";

               using (SqlCommand cmd = new SqlCommand(userInsert, conn))
               {
                   // Get your parameters ready
                   cmd.Parameters.Add(cmd.CreateParameter()).ParameterName = "@data1";
                   cmd.Parameters.Add(cmd.CreateParameter()).ParameterName = "@data2";
                   cmd.Parameters.Add(cmd.CreateParameter()).ParameterName = "@data3";
                   cmd.Parameters.Add(cmd.CreateParameter()).ParameterName = "@data4";
                   cmd.Parameters.Add(cmd.CreateParameter()).ParameterName = "@data5";
                   cmd.Parameters.Add(cmd.CreateParameter()).ParameterName = "@data6";
                   cmd.Parameters["@Data1"].Value = user.Email;
                   cmd.Parameters["@Data2"].Value = user.FirstName;
                   cmd.Parameters["@Data3"].Value = user.LastName;
                   cmd.Parameters["@Data4"].Value = user.PhoneNumber;
                   cmd.Parameters["@Data5"].Value = user.AdminRights;
                   cmd.Parameters["@Data6"].Value = user.Password;

                   //var id 
                   user.Email = (string) cmd.ExecuteScalar(); //Returns the identity of the new tuple/record
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

        public int ValidateLogInInfo(string email, string password)
        {
            //DataContext db = new DataContext(@"Data Source=(localdb)\Databases;Initial Catalog=Joblistesystem;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False");
            try
            {
                // Open the connection
                conn.Open();

                 /* Sammenligner email og password med databasen. Hvis der findes en bruger 
                * med matchende email og password returneres dennes bruger ID. Ellers returneres -1. */

                // String with SQL statement
                string validate = @"SELECT Password FROM Customer WHERE Customer.Email = email";

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

        public UserClass GetUserInfo(string userId)
         // Returns an object of UserClass with ID matching from the database.
         {
            try
            {
                // Open the connection
                conn.Open();

                // String with SQL statement
                string userInfo = @"SELECT * FROM [Customer] WHERE Customer.Email = userId";

                using (SqlCommand cmd = new SqlCommand(userInfo, conn))
                {
                    var locUser = new UserClass();

                    SqlDataReader rdr = cmd.ExecuteReader(); //Returns the identity of the new tuple/record


                    while (rdr.Read())
                    {
                        Console.WriteLine(rdr[0]);


                        locUser.Email = (string) rdr["Email"];
                        locUser.FirstName = (string) rdr["FirstName"];
                        locUser.LastName = (string)rdr["LastName"];
                        locUser.PhoneNumber = (string) rdr["Phonenumber"];
                        locUser.AdminRights = (int) rdr["AdminRights"];
                        locUser.Password = (string) rdr["Password"];
                    }
                    return locUser;
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

         public void AddJob(JobClass job)
         // Add a Jobclass object in the Job tabel on the database.
         {
             try
             {
                 // Open the connection
                 conn.Open();

                 // String with SQL statement
                 string userInsert = @"INSERT INTO [My3DJob] VALUES (@Data1, @Data2, @Data3, @Data4, @Data5, @Data6, @Data7, @Data8)";

                 using (SqlCommand cmd = new SqlCommand(userInsert, conn))
                 {
                     cmd.Parameters.Add(cmd.CreateParameter()).ParameterName = @"Data1";
                     cmd.Parameters.Add(cmd.CreateParameter()).ParameterName = @"Data2";
                     cmd.Parameters.Add(cmd.CreateParameter()).ParameterName = @"Data3";
                     cmd.Parameters.Add(cmd.CreateParameter()).ParameterName = @"Data4";
                     cmd.Parameters.Add(cmd.CreateParameter()).ParameterName = @"Data5";
                     cmd.Parameters.Add(cmd.CreateParameter()).ParameterName = @"Data6";
                     cmd.Parameters.Add(cmd.CreateParameter()).ParameterName = @"Data7";
                     cmd.Parameters.Add(cmd.CreateParameter()).ParameterName = @"Data8";
                     cmd.Parameters[@"Data1"].Value = job.OrderId;
                     cmd.Parameters[@"Data2"].Value = job.Material.MaterialId;
                     cmd.Parameters[@"Data3"].Value = job.Owner.Email;
                     cmd.Parameters[@"Data4"].Value = job.Deadline;
                     cmd.Parameters[@"Data5"].Value = job.File;
                     cmd.Parameters[@"Data6"].Value = job.CreationTime;
                     cmd.Parameters[@"Data7"].Value = job.Hollow;
                     cmd.Parameters[@"Data8"].Value = job.Comment;
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

        public List<JobClass> GetJobList()
         // Return a list of all jobs in the database.
         {
             try
             { 
                 // Open the connection
                 conn.Open();

                 // String with SQL statement
                 string userInsert = @"SELECT * FROM My3DJob";

                 using (SqlCommand cmd = new SqlCommand(userInsert, conn))
                 {
                     List<JobClass> loc3DJobList = null;

                     SqlDataReader rdr = cmd.ExecuteReader(); //Returns the identity of the new tuple/record

                     while (rdr.Read())
                     {
                         var loc3DJob = new JobClass();

                         loc3DJob.OrderId = (int) rdr["OrderId"];
                         loc3DJob.Material = (MaterialClass)rdr["Material"];
                         loc3DJob.Owner = (UserClass)rdr["Owner"];
                         loc3DJob.Deadline = (string)rdr["Deadline"];
                         loc3DJob.File = (string)rdr["File"];
                         loc3DJob.CreationTime = (string)rdr["CreationTime"];
                         loc3DJobList.Add(loc3DJob);
                     }
                     return loc3DJobList;
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
    }
}
