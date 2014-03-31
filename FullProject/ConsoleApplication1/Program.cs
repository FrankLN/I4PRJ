using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ConsoleApplication1;
using MessageTypes.Messages;
using MessageTypes.ReplyMessages;

namespace ClientApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            Client client = new Client(9000);
            LoginMsg msg = new LoginMsg();

            msg.Email = "test@it.nu";
            msg.Password = "123456";

            client.SendToServer(msg);

            LoginReplyMsg temp = new LoginReplyMsg();
                
            temp = (LoginReplyMsg)client.ReceiveMessage();

            Console.WriteLine("Email is {0}", temp.Email);
            Console.WriteLine("Password is {0}", temp.Password);

            Thread.Sleep(2000);
        }
    }
}
