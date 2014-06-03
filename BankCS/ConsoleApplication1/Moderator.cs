using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class Moderator : MemberState
    {
        public virtual  Guid _Id{ get; set; }

        public virtual SubForum subForum{ get; set; }
        public virtual IList<Complaint> complaint{ get; set; }
		
		public Moderator() : this(null)
        { }
        public Moderator(SubForum s)
        {
            this.subForum = s;
        }
        public virtual bool DeletePost(Member m, Post p)
        {
            return (m.MemberPosts.Contains(p) || this.subForum.IsContain(p));     
        }

        public virtual void addNewcomplaint(Complaint c){
            if (c != null)
                this.complaint.Add(c);
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