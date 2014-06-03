using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

using AsyncServer.tokenizer;
using AsyncServer.protocol;

namespace AsyncServer
{
    public class AsyncTcpServer<T>
    {
        int _port;
        int _poolSize;

        TokenizerFactory<T> _tokenizerFactory;
        ServerProtocolFactory<T> _protocolFactory;


        private static TcpListener _listener;


        public AsyncTcpServer(int port, int poolSize, TokenizerFactory<T> tokFactory, ServerProtocolFactory<T> protFactory)
        {
            _port = port;
            _poolSize = poolSize;
            _tokenizerFactory = tokFactory;
            _protocolFactory = protFactory;
        }



        public static void StartServer()
        {
            System.Net.IPAddress localIPAddress = System.Net.IPAddress.Parse("10.91.173.201");
            IPEndPoint ipLocal = new IPEndPoint(localIPAddress, 8888);
            _listener = new TcpListener(ipLocal);
            _listener.Start();
            WaitForClientConnect();
        }
        private static void WaitForClientConnect()
        {
            object obj = new object();
            _listener.BeginAcceptTcpClient(new System.AsyncCallback(OnClientConnect), obj);
        }
        private static void OnClientConnect(IAsyncResult asyn)
        {
            try
            {
                TcpClient clientSocket = default(TcpClient);
                clientSocket = _listener.EndAcceptTcpClient(asyn);
                HandleClientRequest<T> clientReq = new HandleClientRequest<T>(clientSocket,null);
                clientReq.StartClient();
            }
            catch (Exception se)
            {
                throw;
            }

            WaitForClientConnect();
        }
    }
}

