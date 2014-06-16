using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTypes
{
    [Serializable()]
   public class PolicyInfo
    {
        public Guid id;
        public int  maxmoth;
        public int  minword;
        public List<string> ileg;
    }
}
