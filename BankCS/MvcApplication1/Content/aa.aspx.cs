using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataTypes;

namespace MvcApplication1.Content
{
    public partial class aa : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Cookies["VisitorID"] == null)
            {
                //Response.Cookies.Set(new HttpCookie("VisitorID", "1"));

                Response.Cookies["VisitorID"].Name = "VisitorID";
                Response.Cookies["VisitorID"].Value = "1";
            }

            foreach (ForumInfo cur in GlobalData.defaultHandler.WatchAllForums())
            {
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //Button1.
            
        }
    }
}