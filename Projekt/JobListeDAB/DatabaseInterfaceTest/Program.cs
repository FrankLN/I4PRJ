using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseInterface;


namespace DatabaseInterfaceTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var user = new UserClass();
            user.Email = "test@email.dk";
            user.FirstName = "NN";
            user.LastName = "XXX";
            user.PhoneNumber = "20346726";
            user.Password = "password";

            var material = new MaterialClass();
            material.MaterialId = 1;
            material.MaterialType = "Plast";

            var job = new JobClass();
            job.OrderId = 1;
            job.Material = material;
            job.Owner = user;
            job.CreationTime = "17-03-2014";
            job.Deadline = "21-03-2014";
            job.Hollow = 1;
            job.Comment = "Make me!";
            job.MyFile = "hej";

            var db = new Database();

            //db.AddUser(user);
            //db.AddJob(job);
            var user2 = db.GetUserInfo("test@email.dk");
            Console.WriteLine(user2.FirstName);
            var jobliste = db.GetJobList();
            Console.WriteLine(jobliste[0].Comment);
        }
    }
}
