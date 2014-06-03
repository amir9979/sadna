using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using client.Network;
using DataTypes;

namespace client
{
    class Program
    {


        static void Main(string[] args)
        {
            ForumConnection con = new ForumConnectionImpl("192.168.2.102", 12345);
            System.Console.WriteLine("connecting...");
            
            con.connect();
            System.Console.WriteLine("connected");
            System.Console.WriteLine("sendMsg...");
            bool ans = con.entry("bla");
            System.Console.WriteLine("GotResponse:"+ans);

            System.Console.WriteLine("sendMsg...");
            bool ans2 = con.entry("blabla");
            System.Console.WriteLine("GotResponse:" + ans2);
            System.Console.WriteLine("finished");
            System.Console.ReadLine();
        }

        static void Main2(string[] args)
        {
            FuncMsgClient msg = new FuncMsgClient();

            FuncMsgClient msg2 = new FuncMsgClient();
            SubForumInfo info = new SubForumInfo();
            info.Name = "forum1";
            SubForumInfo info2 = new SubForumInfo();
            info2.Name = "forum2";
            msg.type = FuncMsgClient.FuncType.Replay;
            msg.args = new List<object>();
            msg.args.Add(info);
            msg2.type = FuncMsgClient.FuncType.ErrorReplay;
            msg2.args = new List<object>();
            msg2.args.Add(info2);


            TcpClient gameServer = new TcpClient();
            //try
            //{
                System.Console.WriteLine("connecting...");
                gameServer.Connect("192.168.2.102", 12345);
                System.Console.WriteLine("connected");
            //}
            //catch (SocketException exp)
            //{
            //    System.Console.WriteLine("error");
            //    System.Console.WriteLine(exp);
            //    System.Console.ReadLine();
             //   return;
            //}
            NetworkStream sourceStream = gameServer.GetStream();
            BinaryFormatter bformatter = new BinaryFormatter();
            bformatter.Serialize(sourceStream, msg);

            bformatter.Serialize(sourceStream, msg2);
            sourceStream.Close();

        }
    }
}
