using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MvcApplication1
{
    public partial class logout : ImPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            UserHandler handler = getHandler();

            if (handler == null)
            {
                Response.Redirect("~/Default.aspx");
            }

            handler.loggout();
            Response.Redirect("~/forum.aspx?forumname=" + getForumName());
        }
    }
}