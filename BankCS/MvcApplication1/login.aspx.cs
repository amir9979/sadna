using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MvcApplication1
{
    public partial class login : ImPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string usr = getLoginUsername();
            string pwd = getLoginPassword();
            UserHandler handler = getHandler();

            if (usr == null || pwd == null)
                return;
            if (handler == null)
            {
                Response.Redirect("~/Default.aspx");
            }


            

            if (!handler.login(usr, pwd))
            {
                Label1.Visible = true;
                return;
            }
            Response.Redirect("~/forum.aspx?forumname=" + getForumName());
        }

        public string getLoginUsername()
        {
            return Request.Form["usr"];
        }

        public string getLoginPassword()
        {
            return Request.Form["pwd"];
        }


    }
}