<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="post.aspx.cs" Inherits="MvcApplication1.post" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        #Text1
        {
            height: 52px;
            width: 268px;
        }
        #msg
        {
            height: 76px;
            width: 211px;
        }
    </style>
</head>
<body>
    <form id="form1">
    <div>
    
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
    
    </div>
        <asp:Table ID="Table1" runat="server">
        </asp:Table>
    </form>
    <form id="form2" method="post">
        <p><asp:Label ID="Label2" runat="server" Text="Submin new Comment Post"></asp:Label></p>
        <input id="msg" name="msg" type="text" />

        <p>
        <input id="Submit1" type="submit" value="submit" /></p>
    </form>
    
</body>
</html>
