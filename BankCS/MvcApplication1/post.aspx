<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="post.aspx.cs" Inherits="MvcApplication1.post" %>

<!DOCTYPE html>
<html>
<head>
	<meta charset="UTF-8">
	<title>Forum System</title>
	<link rel="stylesheet" type="text/css" href="css/style.css">
</head>
<body>
	<div class="page">
		<div id="header">

			<div>
				<a href="/" class="logo"><img src="resources/images/logo.png" alt=""></a>
                <ul>
					
						<asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                        <asp:HyperLink ID="HyperLink1" runat="server">[HyperLink1]</asp:HyperLink>
					
				</ul>

			</div>
		</div>
		<div id="content">
			<div>
				<h3>Comments</h3>
                <p>
					<asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
					<form id="form2" method="post">
						<asp:Label ID="Label3" runat="server" Text="Submin new Comment Post"></asp:Label>
						<br /><input id="msg" name="msg" type="text" />

						
						<br /><input id="Submit1" type="submit" value="submit" />
					</form>
                    <asp:Label ID="Label4" runat="server" Text="Cannot Post Massage" ForeColor="#FF6666" Visible="False"></asp:Label>
                </p>
			</div>
		</div>
		<div id="footer">
			<div>
				<span>Follow us :</span>
				<a href="https://www.facebook.com/ProactGroup/" class="facebook" target="_blank">facebook</a>
				<a href="https://twitter.com/proacteu/" class="twitter" target="_blank">twitter</a>
				<a href="https://plus.google.com/" class="googleplus" target="_blank">googleplus</a>
			</div>
		</div>
	</div>
</body>
</html>