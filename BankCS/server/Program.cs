using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using ConsoleApplication1;
using DataTypes;

using DataTypes;
using System.Threading;

namespace server
{
    public class Program
    {


        public static void Main()
        {
            IPAddress aa = LocalIPAddress();
            TcpListener _listener = new TcpListener(IPAddress.Any, 12345);
            _listener.Start();


            ForumSystemImpl testSystem = new ForumSystemImpl("amir","1234","amir@gmail.com","amir m","db");
            while (true)
            {
                TcpClient Client = null;
                try
                {
                    Console.WriteLine("listening...");

                    Client = _listener.AcceptTcpClient();

                    Console.WriteLine("got client");
                }
                catch
                {
                    Console.WriteLine("Error");
                    return;
                }

                ConnectionHandler con = new ConnectionHandler(Client, testSystem);
                Thread conThread = new Thread(con.run);
                conThread.Start();

            }
        }

        public static void Run(ForumSystem sys)
        {

            IPAddress aa = LocalIPAddress();
            TcpListener _listener = new TcpListener(IPAddress.Any, 12345);
            _listener.Start();


            //ForumSystemImpl testSystem = new ForumSystemImpl("amir", "1234", "amir@gmail.com", "amir m", "db");
            while (true)
            {
                TcpClient Client = null;
                try
                {
                    Console.WriteLine("listening...");

                    Client = _listener.AcceptTcpClient();

                    Console.WriteLine("got client");
                }
                catch
                {
                    Console.WriteLine("Error");
                    return;
                }

                ConnectionHandler con = new ConnectionHandler(Client, sys);
                Thread conThread = new Thread(con.run);
                conThread.Start();

            }
        }




        static void Main2(string[] args)
        {
            IPAddress aa = LocalIPAddress();
            TcpListener _listener = new TcpListener(aa, 12345);
            _listener.Start();
            TcpClient Client = null;
            try
            {
                Console.WriteLine("listening");

                Client = _listener.AcceptTcpClient();

                Console.WriteLine("got client");
            }
            catch (SocketException exp)
            {
                Console.WriteLine("Error");
                return;
            }

            BinaryFormatter bformatter = new BinaryFormatter();
            System.Threading.Thread.Sleep(5000);
            Console.WriteLine("Reading msg1");
            FuncMsgClient msg = (FuncMsgClient)bformatter.Deserialize(Client.GetStream());
            FuncMsgClient msg2 = (FuncMsgClient)bformatter.Deserialize(Client.GetStream());
            
            System.Console.WriteLine(msg.type);
            System.Console.WriteLine(((SubForumInfo)msg.args[0]).Name);
            Console.WriteLine("Reading msg2");
            
            
            System.Console.WriteLine(msg2.type);
            System.Console.WriteLine(((SubForumInfo)msg2.args[0]).Name);

            Client.Close();
            System.Console.ReadLine();

        }


        private static IPAddress LocalIPAddress()
        {
            if (!System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
            {
                return null;
            }

            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());

            return host
                .AddressList
                .FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork);
        }
    }
}
