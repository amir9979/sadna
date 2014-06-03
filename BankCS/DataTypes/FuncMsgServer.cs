using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTypes
{

    [Serializable()]
    public class FuncMsgServer
    {
        public enum FuncType
        {
            Replay,
            ErrorReplay,
        };

        public FuncType type;

        public List<Object> args;
    }
}
