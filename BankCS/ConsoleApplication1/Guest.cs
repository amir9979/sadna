using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
   public class Guest : User
    {
       public Guest()
       { }
        public Guest(Forum f) : base(f)
        {
           
        }

        public virtual Member loggin(Forum f, String username, String pass)
        {
            return f.login(username, pass);
        }
    }
}
