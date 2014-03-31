using System.Runtime.Remoting.Channels;
using NSubstitute;
using NUnit.Framework;
using Server;

namespace ServerApplication.Test
{
    [TestFixture]
    class ServerUnitTest
    {
        [Test]
        public void Server_Constructor_serverSocketSet()
        {
            Server.Server server = new Server.Server(9000);

            Assert.AreNotEqual(null, server.serverSocket);
        }

        [Test]
        public void Server_Constructor_bFormatterSet()
        {
            Server.Server server = new Server.Server(9000);

            Assert.AreNotEqual(null, server.bFormatter);
        }
    }
}
