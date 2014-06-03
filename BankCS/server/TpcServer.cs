using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ConsoleApplication1;

namespace server
{
    public class TpcServer
    {
        TcpListener _listener;


        int _port;

        ForumSystem _sys;

        public TpcServer(int port, ForumSystem sys)
        {
            _listener = null;
            _port = port;

            _sys = sys;
        }


        public void start()
        {
            close();
            new Thread(this.run).Start();
        }

        public void close()
        {
            if(_listener!=null)
                _listener.Stop();
            _listener = null;
        }

        private void run()
        {
            IPAddress aa = LocalIPAddress();
            _listener = new TcpListener(aa, _port);
            _listener.Start();

            while (true)
            {
                TcpClient Client = null;
                try
                {
                    Console.WriteLine("listening...");

                    Client = _listener.AcceptTcpClient();

                    Console.WriteLine("got client");
                }
                catch (SocketException exp)
                {
                    Console.WriteLine("Error");
                    return;
                }

                ConnectionHandler con = new ConnectionHandler(Client, _sys);
                Thread conThread = new Thread(con.run);
                conThread.Start();

            }
        }




        public static IPAddress LocalIPAddress()
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
