using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using client.Network;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using server;

namespace ClientServerTest
{

    //[TestClass]
    public class StressTests
    {
        static TpcServer _server;

        static List<ForumConnectionImpl> _clients;

        [ClassInitialize()]

        public static void setup(TestContext ss)
        {
            _server = new TpcServer(12343, new ForumSystemTest());
            _server.start();
            Thread.Sleep(1000);
            _clients = new List<ForumConnectionImpl>();
            for (int i = 0; i < 100; i++)
            {
                ForumConnectionImpl temp = new ForumConnectionImpl(TpcServer.LocalIPAddress().ToString(), 12343);
                temp.connect();
                _clients.Add(temp);
            }


        }


        [ClassCleanup()]
        public static void cleanup()
        {
            
            for (int i = 0; i < 100; i++)
            {
                _clients[i].disconnect();
            }

            _server.close();



        }

        [TestMethod]
        public void TestStress1()  //need to return true only to value "test1"
        {
            

            for (int i = 0; i < 100; i++)
            {
                _clients[i].entry("test1");
            }

        }


    }
}
