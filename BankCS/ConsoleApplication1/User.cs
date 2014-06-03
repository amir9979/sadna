using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
  public abstract class User
    {
		public virtual Guid Id { get; set; }
       public virtual Forum forum{ get; set; }
		
		public    User()
        {}
      public    User(Forum f)
        {
            this.forum = f;
        }

       public virtual  IList<SubForum> SubForumList(Forum f)
        {
            return f.SubForumList();
            //throw new NotImplementedException();
        }

    }
}
