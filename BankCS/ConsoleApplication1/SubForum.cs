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
        public virtual IList<Post> MyThreads { get; set; }

        public virtual ICollection<String> UsedWords { get; set; }
     
		public SubForum() : this("")
        {}
        public SubForum(string n)
        {
            this.Name = n;
            MyThreads = new List<Post>();
            MyModerators = new List<Member>();
            UsedWords = new HashSet<String>();
        }
        public virtual string getName()
        {
            return Name;
        }
        public virtual void AddNewModerator(Member m)
        {
            MyModerators.Add(m);
        }

        public virtual void AddWords(string msg)
        {
            string[] ssize = msg.Split(null);
            for (int i = 0; i < ssize.Length; i++)
            {
                string s = ssize[i];
                String ans = s.ToString();
                UsedWords.Add(ans);

            }
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
