using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerApplication.UnitTest.TestClasses
{
    public class ServerAppTest : ServerApp
    {
        public ServerAppTest(int port) : base(port)
        {
        }

        public int GetPort()
        {
            return _port;
        }
    }
}
