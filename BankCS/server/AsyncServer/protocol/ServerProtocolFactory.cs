using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncServer.protocol
{
    public interface ServerProtocolFactory<T>
    {
        Protocol<T> create();
    }
}
