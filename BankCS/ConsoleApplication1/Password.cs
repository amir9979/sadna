using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Password
    {
        string pass;
        List<String> questions;
        List<String> answers;
        DateTime Expiration;
        List<string> lastpass;

        public Password(string pass, List<String> q, List<String> a, DateTime e)
        {
            this.pass = pass;
            this.questions = new List<string>(q);
            this.answers = new List<string>(a);
            Expiration = e;
            this.lastpass = new List<string>();
            this.lastpass.Add(pass);
        }

        public bool ChangePass(string newpass)
        {
            for (int i=0; i<this.lastpass.Count; i++){
                if (pass.Equals(this.lastpass.ElementAt(i)))
                    return false;
            }
            this.lastpass.Add(newpass);
            this.pass = newpass;
            return true;
        }

        
    }
}
