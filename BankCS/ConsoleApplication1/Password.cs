using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class Password
    {
	   public virtual Guid Id { get; set; }
       public virtual string pass { get; set; }
       public virtual IList<String> questions { get; set; }
       public virtual IList<String> answers { get; set; }
       public virtual DateTime Created { get; set; }
       public virtual IList<String> lastpass { get; set; }


       public Password() : this("",new List<String>(),new List<String>(), new DateTime())
       {
       }
        public Password(string pass, List<String> q, List<String> a, DateTime e)
        {
            this.pass = pass;
            this.questions = new List<string>(q);
            this.answers = new List<string>(a);
            this.Created = DateTime.Now;
            this.lastpass = new List<string>();
            this.lastpass.Add(pass);
        }

        public virtual bool ChangePass(string newpass)
        {
            for (int i=0; i<this.lastpass.Count; i++){
                if (pass.Equals(this.lastpass.ElementAt(i)))
                    return false;
            }
            this.lastpass.Add(newpass);
            this.pass = newpass;
            this.Created = DateTime.Now;
            return true;
        }

        public virtual bool IsValidTime(int month)
        {
            return (DateTime.Now.Month - Created.Month < month && ((DateTime.Now.Year - Created.Year) * 12 + (DateTime.Now.Month - Created.Month)) < month);
        }

        
    }
}
