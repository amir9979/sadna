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
                Response.Redirect("~/Default.aspx");
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



            IList<PostInfo> posts = handler.WatchAllComments(new PostInfo { id = stringToGuid(postid) });
            foreach (PostInfo cur in posts)
            {

                HyperLink link = new HyperLink();
                link.NavigateUrl = "~/post.aspx?forumname=" + getForumName() + "&pid=" + cur.id;
                link.Text = cur.msg;
                Label label = new Label();
                label.Text = ":" + cur.owner.username;
                PlaceHolder1.Controls.Add(link);
                PlaceHolder1.Controls.Add(label);
                PlaceHolder1.Controls.Add(new LiteralControl("<br />"));


            }
            if (msg != null && !handler.PublishCommentPost(msg, new PostInfo { id = stringToGuid(postid) }))
            {
                Label4.Visible = true;
                return;
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