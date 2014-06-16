using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using DataTypes;
using ConsoleApplication1;

namespace server
{
    public class ConnectionHandler
    {
        TcpClient _con;
        UserHandler _usrHand;


        public ConnectionHandler(TcpClient con, ForumSystem system)
        {
            _con = con;
            _usrHand = new UserHandler(system);
        }

        public void run()
        {
            try
            {
                BinaryFormatter bformatter = new BinaryFormatter();
                while (true)
                {

                    FuncMsgClient msg = (FuncMsgClient)bformatter.Deserialize(_con.GetStream());

                    FuncMsgServer response = _usrHand.processMsg(msg);
                    bformatter.Serialize(_con.GetStream(), response);
                }
            }
            catch (Exception e)
            {
                return;
            }
        }
    }
}
