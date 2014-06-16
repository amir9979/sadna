using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTypes
{
     [Serializable()]
    public class UserInfo
    {
        public Guid id;
        public bool isGuest;
        public string forumName;

    }
}
