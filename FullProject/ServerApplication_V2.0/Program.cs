using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseInterface;
using ServerApplication;

namespace ServerApplication
{
    /// <summary>
    /// <c>Program</c> is the startup class, which starts up the serverApp.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            ServerApp serverApp = new ServerApp(9000, new Database());

            serverApp.RunServerApp();
        }
    }
}
