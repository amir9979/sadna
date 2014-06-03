using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncServer.tokenizer
{
    class BytesMessage
    {
        private MemoryStream message;

        public BytesMessage(byte[] msg)
        {
            message = new MemoryStream(msg);
        }
        public BytesMessage(MemoryStream msg)
        {
            message = new MemoryStream(msg.ToArray());
        }
        public MemoryStream getMessage()
        {
            return new MemoryStream(message.ToArray());
        }
        public byte[] getBytesMessage()
        {
            return message.ToArray();
        }
    }
}
