using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class Password
    {
       public string pass;
       public List<String> questions;
       public List<String> answers;
       public DateTime Created;
       public List<String> lastpass;
        

        public Password(string pass, List<String> q, List<String> a, DateTime e)
        {
            this.pass = pass;
            this.questions = new List<string>(q);
            this.answers = new List<string>(a);
            this.Created = e;
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
