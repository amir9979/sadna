using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
   public class SuperManager 

    {
        
     public   String username;
     public String password;
     public String mail;
     public String fullname;

        public SuperManager(String username, String password, String mail, String fullname)
        {
            this.username = username;
            this.password = password;
            this.mail = mail;
            this.fullname = fullname;
            
        }
    }
}
