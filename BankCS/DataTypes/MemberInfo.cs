using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTypes
{

    [Serializable()]
    public class MemberInfo
    {
        public Guid id;
        public String username;
        public String fullname;
        public String mail;
        public String type;
        public String rank;  //not sure?!?

    }
}
