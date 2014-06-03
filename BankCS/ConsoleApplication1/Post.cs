using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
   public class Post : Observable
    {
		public virtual Guid Id { get; set; }
        public virtual IList<Post> comments { get; set; }
        public virtual string msg { get; set; }
        public virtual Member owner { get; set; }

		 public Post()
        {
            this.msg = "";
            this.owner = null;
            this.comments = new List<Post>();
        }
		
        public Post(string msg,Member owner)
        {
            this.msg = msg;
            this.owner = owner;
            this.comments = new List<Post>();
            
        }
        public virtual void addComment(Post p)
        {
            comments.Add(p);
        }


        public virtual void edit(string change)
        {
            msg = change;
        }

        public virtual string getMsg()
        {
            return msg;
        }

        public virtual IList<Post> getComments()
        {
            return comments;
        }

        public virtual void deleteAllSons()
        {

            foreach (Post p in comments)
            {
                owner.delPost(p);
                p.deleteAllSons();
            }
            owner.delPost(this);
        }

        public virtual void notifyAll()
        {

        }

        public virtual bool isContain(Post p)
        {
            if (this == p) return true;

            foreach (Post cur in comments)
            {
                if (cur.isContain(p)) return true;
            }
            return false;


        }

    }
}
