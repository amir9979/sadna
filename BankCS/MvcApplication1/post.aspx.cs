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
    public partial class post : ImPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string forumname;
            string postid = getSubForumId();
            string sessionID = getSessionId();
            UserHandler handler = getHandler();
            string msg = getPostMsg();
            if (handler == null || postid==null)
            {
                Label1.Text = "error";
                return;
            }
            if (msg != null && !handler.PublishCommentPost(msg, new PostInfo { id = stringToGuid(postid) }))
            {
                Label1.Text = "cannot post msg";
                return;
            }


            IList<PostInfo> posts = handler.WatchAllComments(new PostInfo { id = stringToGuid(postid) });
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
            ICollection ddd = Request.Form;
            return Request.Form["msg"];
        }



        public string getSubForumId()
        {
            return Request.QueryString["pid"];
        }

    }
}