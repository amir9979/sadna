<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="MvcApplication1.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" method="post">
    <div>
        login:
        <br />
        username: <input id="Text1" name="usr" type="text" />
        <br />
        password: <input id="Password1" name="pwd" type="password" />
        <br />
        <input id="Submit1" type="submit" value="submit" />
        <br />
        <asp:Label ID="Label1" runat="server" Text="Login failed try again" ForeColor="#FF6666" Visible="False"></asp:Label>
    </div>
    </form>
</body>
</html>
