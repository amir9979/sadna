using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Normal : MemberState
    {
        public virtual  Guid _Id{ get; set; }
        public virtual bool DeletePost(Member m, Post p)
        {
            return m.MemberPosts.Remove(p);
        }
        public virtual Guid Id
        {
            get
            {
                return _Id;
            }

            set
            {
                _Id = value;
            }
        }

    }
}
