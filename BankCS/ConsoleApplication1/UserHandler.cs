using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
   public class UserHandler

    {
       private ForumSystemImpl system;
       private User user;
       

       public static UserHandler CreatUserHandler(ForumSystemImpl s, String forumname)  // represent the entry use case
       {
           
           User g=null;
            g = s.entry(forumname);
            if (g != null)
            {
                return new UserHandler(s,g);
            }
           return null;
          
       }

       private UserHandler(ForumSystemImpl s, User u)
       {
           this.system=s;
           this.user=u;
       }

       /*
       private BuildForum (string name, string adminname, string adminpass, string adminmail,string fullname){
           if (this.user.)
       }

    */

    }
}
