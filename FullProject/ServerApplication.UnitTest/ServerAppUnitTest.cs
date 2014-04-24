using System.Runtime.Serialization;
using System.Threading;
using DatabaseInterface;
using MessageTypes;
using MessageTypes.Messages;
using MessageTypes.ReplyMessages;
using NUnit.Framework;
using ServerApplication.UnitTest.TestClasses;
using NSubstitute;

namespace ServerApplication.UnitTest
{
    [TestFixture]
    public class ServerAppUnitTest
    {
        #region ConstructorTest
        [Test]
        public void ServerApp_Constructor_PortIs9000()
        {
            int expect = 9000;

            ServerAppTest serverApp = new ServerAppTest(expect, new Database());

            Assert.AreEqual(expect, serverApp.GetPort());
        }

        [Test]
        public void ServerApp_Constructor_DatabaseIsEqual()
        {
            IDatabase expect = Substitute.For<IDatabase>();

            ServerAppTest serverApp = new ServerAppTest(9000, expect);

            Assert.AreEqual(expect, serverApp.GetDatabase());
        }
        #endregion

        #region VerifyLoginTest

        [Test]
        public void ServerApp_VerifyLogin_GetUserInfoCalledWithExpectedEmail()
        {
            string expectedEmail = "my@email.com";

            IDatabase database = Substitute.For<IDatabase>();
            ILoginMsg loginMessage = Substitute.For<ILoginMsg>();

            loginMessage.Email.ReturnsForAnyArgs(expectedEmail);
            #region NotImportant
            loginMessage.Password.Returns("123456");

            UserClass user = new UserClass();
            database.GetUserInfo(expectedEmail).ReturnsForAnyArgs(user);
            #endregion

            ServerApp serverApp = new ServerApp(9000, database);

            serverApp.VerifyLogin(loginMessage, Substitute.For<IServer>());

            database.Received().GetUserInfo(expectedEmail);
        }

        [Test]
        public void ServerApp_VerifyLogin_GetUserInfoCalledWithNotExpectedEmail()
        {
            string expectedEmail = "my@email.com";
            string notExpectedEmail = "not@my.email";

            IDatabase database = Substitute.For<IDatabase>();
            ILoginMsg loginMessage = Substitute.For<ILoginMsg>();

            loginMessage.Email.ReturnsForAnyArgs(expectedEmail);
            #region NotImportant
            loginMessage.Password.Returns("123456");

            UserClass user = new UserClass();
            database.GetUserInfo(expectedEmail).ReturnsForAnyArgs(user);
            #endregion

            ServerApp serverApp = new ServerApp(9000, database);

            serverApp.VerifyLogin(loginMessage, Substitute.For<IServer>());

            database.DidNotReceive().GetUserInfo(notExpectedEmail);
        }

        [Test]
        public void ServerApp_VerifyLogin_SendToClientCalledWithAnyArguments()
        {
            IDatabase database = Substitute.For<IDatabase>();
            ILoginMsg loginMessage = Substitute.For<ILoginMsg>();
            IServer server = Substitute.For<IServer>();

            #region NotImportant
            loginMessage.Email.ReturnsForAnyArgs("test");
            loginMessage.Password.Returns("123456");

            UserClass user = new UserClass();
            database.GetUserInfo("test").ReturnsForAnyArgs(user);
            #endregion

            ServerApp serverApp = new ServerApp(9000, database);

            serverApp.VerifyLogin(loginMessage, server);
            
            server.ReceivedWithAnyArgs().SendToClient((ISerializable)Substitute.For<ILoginReplyMsg>());
        }


        #endregion

    }
}
