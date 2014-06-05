using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTypes
{

    [Serializable()]
    public class ComplaintInfo
    {
        Guid id;
        public MemberInfo TheComplainer;
        public MemberInfo member;
        public string complaint;

    }
}
