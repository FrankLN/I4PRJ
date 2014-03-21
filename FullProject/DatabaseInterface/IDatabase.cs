using System.Collections.Generic;

namespace DatabaseInterface
{
    public interface IDatabase
    {
        void AddUser(UserClass user);

        //int ValidateLogInInfo(string email, string password);

        UserClass GetUserInfo(string userId);

        void AddJob(JobClass job);

        List<JobClass> GetJobList();
    }
}