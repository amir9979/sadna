using DataTypes;
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
       private UserInfo user;
       

       public static UserHandler CreatUserHandler(ForumSystemImpl s, String forumname)  // represent the entry use case
       {
           
           UserInfo g=null;
            g = s.entry(forumname);
            if (g != null)
            {
                return new UserHandler(s,g);
            }
           return null;
          
       }

       private UserHandler(ForumSystemImpl s, UserInfo u)
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
