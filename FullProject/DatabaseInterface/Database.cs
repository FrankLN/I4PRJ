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
            conn = new SqlConnection(@"Data Source=10.29.0.29;Initial Catalog=F14I4SemProj4Gr4;Integrated Security=False;User ID=F14I4SemProj4Gr4;Password=F14I4SemProj4Gr4;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False;");
        }


        public void AddUser(UserClass user)
       // AddUser adds an object in the User tabel on the database.
        {          
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
                   //user.Email = (string) cmd.ExecuteScalar(); //Returns the identity of the new tuple/record
                   cmd.ExecuteScalar();
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

                 // Sammenligner email og password med databasen. Hvis der findes en bruger 
                 // med matchende email og password returneres dennes bruger ID. Ellers returneres -1.

                // String with SQL statement
                string validate = @"SELECT Password FROM Customer WHERE Customer.Email = '" + email + "'";

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
                string userInfo = @"SELECT * FROM [Customer] WHERE Customer.Email = '" + userId + "'";

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
                 string jobInsert = @"INSERT INTO [My3DJob] (OrderId, MaterialFK, Owner, Deadline, MyFile, CreationTime, Hollow, Comment) VALUES ("+job.OrderId+", "+job.Material+", '"+job.Owner+"', '"+job.Deadline+"', '"+job.File+"', '"+job.CreationTime+"', "+job.Hollow+",'"+job.Comment+"')";

                 SqlCommand cmd = new SqlCommand(jobInsert, conn);
                
                     //cmd.Parameters.Add(cmd.CreateParameter()).ParameterName = "@data1";
                     //cmd.Parameters.Add(cmd.CreateParameter()).ParameterName = "@data2";
                     //cmd.Parameters.Add(cmd.CreateParameter()).ParameterName = "@data3";
                     //cmd.Parameters.Add(cmd.CreateParameter()).ParameterName = "@data4";
                     //cmd.Parameters.Add(cmd.CreateParameter()).ParameterName = "@data5";
                     //cmd.Parameters.Add(cmd.CreateParameter()).ParameterName = "@data6";
                     //cmd.Parameters.Add(cmd.CreateParameter()).ParameterName = "@data7";
                     //cmd.Parameters.Add(cmd.CreateParameter()).ParameterName = "@data8";
                     //cmd.Parameters["@Data1"].Value = job.OrderId;
                     //cmd.Parameters["@Data2"].Value = job.MaterialFK;
                     //cmd.Parameters["@Data3"].Value = job.Owner;
                     //cmd.Parameters["@Data4"].Value = job.Deadline;
                     //cmd.Parameters["@Data5"].Value = job.MyFile;
                     //cmd.Parameters["@Data6"].Value = job.CreationTime;
                     //cmd.Parameters["@Data7"].Value = job.Hollow;
                     //cmd.Parameters["@Data8"].Value = job.Comment;

                 //job.OrderId = (int) 
                     cmd.ExecuteNonQuery();
                         //ExecuteScalar(); // use if you need value returned (when using IDENTITY)
                 
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
                     List<JobClass> loc3DJobList = new List<JobClass>();

                     SqlDataReader rdr = cmd.ExecuteReader(); //Returns the identity of the new tuple/record

                     int i = 1;

                     while (rdr.Read())
                     {
                         var loc3DJob = new JobClass();

                         loc3DJob.OrderId = i;
                         //loc3DJob.OrderId = (int) rdr["OrderId"];
                         loc3DJob.Material.MaterialId = (int)rdr["MaterialFK"];
                         loc3DJob.Owner.Email = (string)rdr["Owner"];
                         loc3DJob.Deadline = (string)rdr["Deadline"];
                         loc3DJob.File = (string)rdr["MyFile"];
                         loc3DJob.CreationTime = (string)rdr["CreationTime"];
                         loc3DJob.Hollow = (int) rdr["Hollow"];
                         loc3DJob.Comment = (string)rdr["Comment"];
                         loc3DJobList.Add(loc3DJob);

                         i++;
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
