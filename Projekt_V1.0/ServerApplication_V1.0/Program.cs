using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server;

namespace ServerApplication_V1._0
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                new ServerApp(9000);
            }
        }
    }
}
