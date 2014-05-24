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
    /// <summary>
    /// <c>Database</c>Is the class containing functions that handles the acces to the database
    /// </summary>
    public class Database : IDatabase
    {
        private SqlConnection conn;
        /// <summary>
        /// The <c>Database</c>'s constructer that makes an SqlConnection string "conn".
        /// </summary>
        public Database()
        {
            conn = new SqlConnection(@"Data Source=10.29.0.29;Initial Catalog=F14I4SemProj4Gr4;Integrated Security=False;User ID=F14I4SemProj4Gr4;Password=F14I4SemProj4Gr4;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False;");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        public void AddUser(UserClass user)
       // AddUser adds an object in the User tabel on the database.
        {          
           try
           {
               // Open the connection
               conn.Open();

               // String with SQL statement
               string userInsert = @"INSERT INTO [Customer] (Email, FirstName, LastName, PhoneNumber, AdminRights, Password, ActivationCode) 
                                        VALUES (@Data1,@Data2,@Data3,@Data4,@Data5, @Data6, @Data7)";

               using (SqlCommand cmd = new SqlCommand(userInsert, conn))
               {
                   // Get your parameters ready
                   cmd.Parameters.Add(cmd.CreateParameter()).ParameterName = "@data1";
                   cmd.Parameters.Add(cmd.CreateParameter()).ParameterName = "@data2";
                   cmd.Parameters.Add(cmd.CreateParameter()).ParameterName = "@data3";
                   cmd.Parameters.Add(cmd.CreateParameter()).ParameterName = "@data4";
                   cmd.Parameters.Add(cmd.CreateParameter()).ParameterName = "@data5";
                   cmd.Parameters.Add(cmd.CreateParameter()).ParameterName = "@data6";
                   cmd.Parameters.Add(cmd.CreateParameter()).ParameterName = "@data7";
                   cmd.Parameters["@Data1"].Value = user.Email;
                   cmd.Parameters["@Data2"].Value = user.FirstName;
                   cmd.Parameters["@Data3"].Value = user.LastName;
                   cmd.Parameters["@Data4"].Value = user.PhoneNumber;
                   cmd.Parameters["@Data5"].Value = user.AdminRights;
                   cmd.Parameters["@Data6"].Value = user.Password;
                   cmd.Parameters["@Data7"].Value = user.ActivationCode;


                   
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

        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public int ValidateLogInInfo(string email, string password)
        {
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
 
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
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
                        locUser.Activated = (int) rdr["Activated"];
                        locUser.ActivationCode = (string) rdr["ActivationCode"];
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="job"></param>
        public void AddJob(JobClass job)

         // Add a Jobclass object in the Job tabel on the database.
         {
             try
             {
                 // Open the connection
                 conn.Open();

                 // String with SQL statement
                 // UPDATED
                 string jobInsert = @"INSERT INTO [My3DJob] (MaterialFK, Owner, Deadline, MyFile, CreationTime, Hollow, Comment)
                                    OUTPUT INSERTED.OrderId 
                                    VALUES ('"+job.Material.MaterialId+"', '"+job.Owner.Email+"', '"+job.Deadline+"', '"+job.File+"', '"+job.CreationTime+"', "+job.Hollow+",'"+job.Comment+"')";

                 SqlCommand cmd = new SqlCommand(jobInsert, conn);


                 job.OrderId = (int)((long)cmd.ExecuteScalar()); // use if you need value returned (when using IDENTITY)   
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

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
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

                     while (rdr.Read())
                     {
                         var loc3DJob = new JobClass();

                         loc3DJob.OrderId = (int)((long) rdr["OrderId"]);
                         loc3DJob.Material.MaterialId = (int)rdr["MaterialFK"];
                         loc3DJob.Owner.Email = (string)rdr["Owner"];
                         loc3DJob.Deadline = (string)rdr["Deadline"];
                         loc3DJob.File = (string)rdr["MyFile"];
                         loc3DJob.CreationTime = (string)rdr["CreationTime"];
                         loc3DJob.Hollow = (int) rdr["Hollow"];
                         loc3DJob.Comment = (string)rdr["Comment"];
                         loc3DJob.Status = (int) rdr["Status"];
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

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<MaterialClass> GetMaterials()
        // Returns an object of MaterialClass with ID matching from the database.
        {
            try
            {
                // Open the connection
                conn.Open();

                // String with SQL statement
                string materialInfo = @"SELECT * FROM [Material]";

                using (SqlCommand cmd = new SqlCommand(materialInfo, conn))
                {
                    List<MaterialClass> locMaterialList = new List<MaterialClass>();

                    SqlDataReader rdr = cmd.ExecuteReader(); //Returns the identity of the new tuple/record

                    //int j = 1;

                    while (rdr.Read())
                    {
                        var locMaterial = new MaterialClass();
                        Console.WriteLine(rdr[0]);

                        locMaterial.MaterialId = (int)(rdr["MaterialId"]);
                        //locMaterial.MaterialId = j;
                        locMaterial.MaterialType = (string)rdr["MaterialType"];
                        locMaterialList.Add(locMaterial);

                        //j++;
                    }
                    return locMaterialList;
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        public void ActivateUser(UserClass user)
        {
            try
            {
                // Open the connection
                conn.Open();

                // String with SQL statement
                // UPDATED
                string activateUser = @"UPDATE [Customer] SET Activated='"+user.Activated+"' WHERE Email='"+user.Email+"'";

                SqlCommand cmd = new SqlCommand(activateUser, conn);

                cmd.ExecuteNonQuery();
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
