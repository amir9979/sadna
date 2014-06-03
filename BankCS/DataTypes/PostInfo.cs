using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTypes
{

    [Serializable()]
   public class PostInfo
    {
      public int id;
      public String msg;
      public MemberInfo owner;

    }
}
