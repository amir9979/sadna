using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataTypes;

namespace MvcApplication1
{
    public partial class forum : ImPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string forumname;
            string sessionID = getSessionId();
            UserHandler handler=null;
            if ((handler = getHandler()) == null)
            {
                Label1.Text = "error";
                return;
            }
            if (handler.username != null)
            {
                Label1.Text = "you are login as " + handler.username + "   ";
                HyperLink1.Text = "logout";
                HyperLink1.NavigateUrl = "~/logout.aspx?forumname=" + getForumName();
            }
            else
            {
                Label1.Text = "hello guest please :" + handler.username + "   ";
                HyperLink1.Text = "login";
                HyperLink1.NavigateUrl = "~/login.aspx?forumname=" + getForumName();
            }

            IList<SubForumInfo> sf = handler.WatchAllSubForum();
            foreach (SubForumInfo cur in sf)
            {
                TableRow row = new TableRow();
                TableCell cell = new TableCell();
                HyperLink link = new HyperLink();
                link.NavigateUrl = "~/subforum.aspx?forumname=" + getForumName() + "&sbid="+cur.id;
                link.Text = cur.Name;
                cell.Controls.Add(link);
                row.Cells.Add(cell);
                Table1.Rows.Add(row);
            }
        }
    }
}