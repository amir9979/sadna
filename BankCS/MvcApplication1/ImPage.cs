using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApplication1
{
    public abstract class ImPage : System.Web.UI.Page
    {
        private static int id = 0;
        public string getSessionId()
        {
            if (Request.Cookies["VisitorID"] == null)
            {
                //Response.Cookies.Set(new HttpCookie("VisitorID", "1"));
                string sessionId = generateID();
                Response.Cookies["VisitorID"].Name = "VisitorID";
                Response.Cookies["VisitorID"].Value = sessionId;
                return sessionId;
            }
            return Request.Cookies["VisitorID"].Value;
        }

        public UserHandler getHandler()
        {
            string forumname;
            string sessionID = getSessionId();
            UserHandler handler = null;
            if ((forumname = getForumName()) == null)
            {
                return null;
            }
            foreach (UserHandler cur in GlobalData.handlers)
            {
                if (cur._id == sessionID && cur._forum == forumname)
                {
                    handler = cur;
                    break;
                }

            }
            if (handler == null)
            {
                handler = new UserHandler(GlobalData.system, sessionID, forumname);
                if (!handler.entry(forumname))
                    return null;
                GlobalData.handlers.Add(handler);
            }
            return handler;
        }
        public string getForumName()
        {
            return Request.QueryString["forumname"];
        }


        private string generateID()
        {
            return Guid.NewGuid().ToString("N");
        }

        protected string guidTostring(Guid g)
        {
            return g.ToString();
        }

        protected Guid stringToGuid(string g)
        {
            return new Guid(g);
        }
    }
}