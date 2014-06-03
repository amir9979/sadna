using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncServer.tokenizer
{
    class MuPacketTokenizer : MessageTokenizer<MuPacket>
    {
        List<byte> array;
        bool toServer;

        public MuPacketTokenizer(bool toServer1)
        {
            array = new List<byte>();
            toServer = toServer1;
        }
        public void addBytes(byte[] bytes, int from, int amount)
        {
            for (int i = from; i < amount; i++)
            {
                array.Add(bytes[i]);
            }
        }

        public bool hasMessage()
        {
            if (array.Count == 0)
            {
                return false;
            }
            int length = curPacketSize();
            if (array.Count() >= length)
            {
                return true;
            }
            else return false;
        }
        private ushort curPacketSize()
        {
            if (array[0] == 0xC1 || array[0] == 0xC3) return array[1]; ;
            if (array[0] == 0xC2 || array[0] == 0xC4) return (ushort)(array[1] * 0x100 + array[2]);
            return 0;// shouldn't occur 
        }

        public MuPacket nextMessage()
        {
            if (!hasMessage())
            {
                return null;
            }
            int length = curPacketSize();
            byte[] ans = new byte[length];
            for (int i = 0; i < length; i++)
            {
                ans[i] = array.ElementAt(0);
                array.RemoveAt(0);
            }
            return new MuPacket(ans, 0, toServer);
        }

        public byte[] getBytesForMessage(MuPacket msg)
        {
            return msg.Source;
        }
    }
}
