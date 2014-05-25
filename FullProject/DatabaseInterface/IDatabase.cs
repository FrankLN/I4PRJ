using System.Collections.Generic;


namespace DatabaseInterface
{ 
    /// <summary>
    /// <c>IDatabase</c> is the interface for accessing the database.
    /// </summary>
    public interface IDatabase
    {
        /// <summary>
        /// Method signature
        /// </summary>
        /// <param name="user">The Userclass object</param>
        void AddUser(UserClass user);

        /// <summary>
        /// Method signature
        /// </summary>
        /// <param name="email">The email</param>
        /// <param name="password">The password</param>
        /// <returns></returns>
        int ValidateLogInInfo(string email, string password);

        /// <summary>
        /// Method signature
        /// </summary>
        /// <param name="userId">The ID</param>
        /// <returns>The customer that has the ID</returns>
        UserClass GetUserInfo(string userId);

        /// <summary>
        /// Method signature
        /// </summary>
        /// <param name="job">The Jobclass object</param>
        void AddJob(JobClass job);

        /// <summary>
        /// Method signature
        /// </summary>
        /// <returns>A list of all jobs in the database</returns>
        List<JobClass> GetJobList();

        /// <summary>
        /// Method signature
        /// </summary>
        /// <returns>A list of all materials in the database</returns>
        List<MaterialClass> GetMaterials();

        /// <summary>
        /// Method signature
        /// </summary>
        /// <param name="user">The UserClass object that is to be activated</param>
        void ActivateUser(UserClass user);
    }

}