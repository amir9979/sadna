using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
   public class SubForum
    {
        public virtual Guid Id { get; set; }
        public virtual string Name{ get; set; }
        public virtual IList<Member> MyModerators{ get; set; }
        public virtual IList<Post> MyThreads{ get; set; }
		public SubForum() : this("")
        {}
        public SubForum(string n)
        {
            this.Name = n;
            MyThreads = new List<Post>();
            MyModerators = new List<Member>();
        }
        public virtual string getName()
        {
            return Name;
        }
        public virtual void AddNewModerator(Member m)
        {
            MyModerators.Add(m);
        }
        public virtual void AddNewThread(Post p)
        {
            MyThreads.Add(p);
        }
        public virtual bool IsContain(Post p)
        {
            bool found = false;
            for (int i = 0; i < MyThreads.Count(); i++)
            {
                if (MyThreads.ElementAt(i).isContain(p))
                    found = true;
            }
            return found;

        }

        public virtual IList<Post> GetMyThreads()
        {
            return MyThreads;
        }

        public virtual Post GetThreadByBody(String body)
        {
            for (int i = 0; i < this.MyThreads.Count; i++)
            {
                if (MyThreads.ElementAt(i).getMsg().Equals(body))
                    return MyThreads.ElementAt(i);
            }
            return null;
        }
        public virtual IList<Member> GetMyModerators()
        {
            return MyModerators;
        }

        public virtual bool removeThread(Post p)
        {
            return MyThreads.Remove(p);
        }

    }
}
