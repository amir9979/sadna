using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using DataTypes;

namespace AsyncServer.tokenizer
{
    public class MsgFuncTokenizer : MessageTokenizer<FuncMsg>
    {

        List<byte> array;

        public void addBytes(byte[] bytes, int from, int to)
        {
            for (int i = from; i < to; i++)
            {
                array.Add(bytes[i]);
            }
        }

        public bool hasMessage()
        {
            return array.Count >= 4 && array.Count >= curPacketSize();

        }

        public FuncMsg nextMessage()
        {
            if (!hasMessage())
            {
                return null;
            }


            int length = curPacketSize();

            array.RemoveAt(0);
            array.RemoveAt(0);
            array.RemoveAt(0);
            array.RemoveAt(0);

            byte[] ans = new byte[length];
            for (int i = 0; i < length; i++)
            {
                ans[i] = array.ElementAt(0);
                array.RemoveAt(0);
            }
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream(ans);
            bf.Deserialize(ms);
            return (FuncMsg)bf.Deserialize(ms);
        }

        public byte[] getBytesForMessage(FuncMsg msg)
        {
            if (msg == null)
                return null;
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            bf.Serialize(ms, msg);
            return ms.ToArray();
        }

        private int curPacketSize()
        {
            if (array.Count < 4)
                return -1;
            byte[] integer  = { array[0], array[1] , array[2], array[3]};

            return  BitConverter.ToInt32(integer, 0);
        }
    }
}
