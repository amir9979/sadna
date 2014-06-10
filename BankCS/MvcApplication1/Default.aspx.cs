using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataTypes;

namespace MvcApplication1
{
    public partial class Default : ImPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            foreach (ForumInfo cur in GlobalData.defaultHandler.WatchAllForums())
            {
                TableRow row = new TableRow();
                TableCell cell = new TableCell();
                HyperLink link = new HyperLink();
                link.NavigateUrl = "~/forum.aspx?forumname=" + cur.name;
                link.Text = cur.name;
                cell.Controls.Add(link);
                row.Cells.Add(cell);
                Table1.Rows.Add(row);
            }
        }
    }
}