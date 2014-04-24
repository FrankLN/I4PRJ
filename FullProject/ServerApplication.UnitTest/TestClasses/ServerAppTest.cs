using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseInterface;

namespace ServerApplication.UnitTest.TestClasses
{
    public class ServerAppTest : ServerApp
    {
        public ServerAppTest(int port, IDatabase database) : base(port, database)
        {
        }

        public int GetPort()
        {
            return _port;
        }

        public IDatabase GetDatabase()
        {
            return _database;
        }
    }
}
