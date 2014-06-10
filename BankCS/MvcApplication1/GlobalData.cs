using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using ConsoleApplication1;

namespace MvcApplication1
{
    public class GlobalData
    {

        public static ForumSystem system ;//= new ForumSystemImp();

        public static List<UserHandler> handlers = new List<UserHandler>();

        public static UserHandler defaultHandler; 

        public static Guid ToGuid(int value)
        {
            byte[] bytes = new byte[16];
            BitConverter.GetBytes(value).CopyTo(bytes, 0);
            return new Guid(bytes);
        }

        //public static void 
    }
}