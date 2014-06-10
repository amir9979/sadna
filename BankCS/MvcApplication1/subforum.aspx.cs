using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataTypes;

namespace MvcApplication1
{
    public partial class subforum : ImPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string forumname;
            string subForumId = getSubForumId();
            string sessionID = getSessionId();
            UserHandler handler = getHandler();
            string msg = getPostMsg();

            if (handler == null || subForumId == null)
            {
                Label1.Text = "error";
                return;
            }
            if (msg != null && !handler.PublishNewThread(msg, new SubForumInfo { id = stringToGuid(subForumId) }))
            {
                Label1.Text = "cannot post msg";
                return;
            }

            IList<PostInfo> posts = handler.WatchAllThreads(new SubForumInfo { id = stringToGuid(subForumId) });
            foreach (PostInfo cur in posts)
            {
                TableRow row = new TableRow();
                TableCell cell = new TableCell();
                HyperLink link = new HyperLink();
                link.NavigateUrl = "~/post.aspx?forumname=" + getForumName() + "&pid=" + cur.id;
                link.Text = cur.msg;
                cell.Controls.Add(link);
                row.Cells.Add(cell);
                Table1.Rows.Add(row);
            }
        }


        public string getPostMsg()
        {
            return Request.Form["msg"];
        }

        public string getSubForumId()
        {
            return Request.QueryString["sbid"];

        }
    }
}